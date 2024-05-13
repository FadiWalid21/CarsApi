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
    public class ModelController : ControllerBase
    {
        private readonly IModelServices _modelServices;
        private readonly IBrandServices _brandServices;
        public ModelController(IModelServices modelServices,IBrandServices brandServices)
        {
            _brandServices = brandServices;
            _modelServices = modelServices;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Model>>> GetModels()
        {
            return Ok(await _modelServices.GetModels());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelToReturn>> GetModelById(int id)
        {
            var model = await _modelServices.GetModelById(id);
            if(model != null)
                return Ok(model);
            
            return NotFound("Model Not Found");
        }
        
        [HttpPost]
        public async Task<ActionResult<bool>> AddModel(string name,int brandId,string imageUrl)
        {
            var brand = await _brandServices.GetBrandById(brandId);
            if(brand == null)
                return NotFound("Brand Not Found");
            
            brandId = brand.Id;
            var result = await _modelServices.CreateModel(name,brandId,imageUrl);

            if(result)
                return Ok("Model Added Successfully");
            
            return BadRequest("Can't Add This Model");
        }
    }
}