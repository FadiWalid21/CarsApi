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
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _brandServices;
        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetBrands()
        {
            return Ok(await _brandServices.GetBrands());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetBrandById(int id)
        {
            var brand = await _brandServices.GetBrandById(id);
            if(brand != null)
                return Ok(brand);
            
            return NotFound("Sorry Can't Find This Brand");
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddBrand(string name , string country)
        {
            if(name !=null && country!= null)
            {
                var result = await _brandServices.AddBrand(name,country);
                if (result)
                    return Ok("Brand Created Successfully");
            }

            return BadRequest("Name and Country Shouldn't Be Null ");
        }
    }
}