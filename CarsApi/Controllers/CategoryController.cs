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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            return Ok(await _categoryServices.GetCategories());
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddCategory(string name)
        {
            if(name!=null)
            {
                var result = await _categoryServices.CreateCategory(name);
                if (result)
                    return Ok("Category Created Succesfully");
                else
                return BadRequest("Error On creating category");
            }

            return BadRequest("name can't be null");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryServices.GetCategory(id);
            if(category != null)
                return Ok(category);
            
            return NotFound("Can't Find This Category");
        }
    }
}