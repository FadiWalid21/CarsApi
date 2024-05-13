using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarServices _carServices;
        public CarController(ICarServices carServices)
        {
            _carServices = carServices;
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

        [HttpPost]
        public async Task<ActionResult<bool>> AddCar(CarDto carDto)
        {
            return Ok(await _carServices.AddCar(carDto));
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