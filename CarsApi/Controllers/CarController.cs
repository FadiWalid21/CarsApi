using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarServices _carServices;
        private readonly IFavService _favService;

        public CarController(ICarServices carServices , IFavService favService)
        {
            _carServices = carServices;
            _favService = favService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CarToReturnDto>>> GetCars()
        {
            return Ok(await _carServices.GetCars());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<CarToReturnDto>>> Search(string name)
        {
            var cars = await _carServices.SearchCars(name);
            if(cars != null)
                return Ok(cars);
            
            return NotFound("Sorry Car Not Found");
        }

        [Authorize]
        [HttpGet("favorites")]
        public async Task<ActionResult<IReadOnlyList<CarToReturnDto>>> GetFavorites()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail)) return Unauthorized();
            
           var cars =  await _carServices.GetFavourites(userEmail);
           if (cars != null)
               return Ok(cars);

           return NotFound("Can't Find Any Favorite Car");
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddCar(CarDto carDto)
        {
            return Ok(await _carServices.AddCar(carDto));
        }
        
        [Authorize]
        [HttpPost("add-favorite")]
        public async Task<ActionResult<bool>> AddToFavorite(int carId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail)) return Unauthorized();

            var result =  await _favService.AddToFavourate(userEmail, carId);
            if (result) return Ok("Car Added To Favorites Successfully");

            return BadRequest("Can't Add To Favorite");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarToReturnDto>> GetCarById(int id)
        {
            var car = await _carServices.GetCarById(id);
            if(car != null)
                return Ok(car);
            
            return NotFound("Car Not Found");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCar(int id)
        {
            var result = await _carServices.DeleteCar(id);
            if(result)
                return Ok("Car Deleted Successfully");

            return BadRequest("Can't Delete The Car");
        }

    }
}