using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class CategoryService : ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCategory(string name)
        {

            var categories = await GetCategories();
            var exist = categories.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

            if (exist != null)
                return false;

            Category newCategory = new Category
            {
                Name = name,
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetCategories()
{
    var categories = await _context.Categories
        .Include(c => c.Cars)
        .ThenInclude(car => car.Brand)
        .ThenInclude(m => m.Models)
        .ToListAsync();

    var data = categories.Select(category => new CategoryDto
    {
        Cars = category.Cars.Select(car => new CarToReturnDto
        {
            CarType = car.CarType?.Name, // Use null-conditional operator
            Colors = car.Colors,
            Country = car.Brand?.Country, // Use null-conditional operator
            Name = $"{car.Brand?.Name} {car.Model?.Name}", // Use null-conditional operator
            Cylinder = car.Cylinder,
            Doors = car.Doors,
            GearBox = car.GearBox,
            Id = car.Id,
            ImagesUrls = car.ImagesUrls,
            MaxPrice = car.MaxPrice,
            MinPrice = car.MinPrice,
            Model = car.Model?.Name, // Use null-conditional operator
            Motor = car.Motor,
            PowerHorse = car.PowerHorse,
            Tank = car.Tank,
            Year = car.Year
        }).ToList(),
        Name = category.Name,
        Id = category.Id
    }).ToList();

    return data;
}


        public async Task<CategoryDto?> GetCategory(int id)
        {
            var category = await _context.Categories
                                        .Include(c => c.Cars)
                                        .ThenInclude(car => car.Brand)
                                        .ThenInclude(m => m.Models)
                                        .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                return new CategoryDto{
                    Cars = category.Cars.Select(car => new CarToReturnDto
                {
                    CarType = car.CarType.Name,
                    Colors = car.Colors,
                    Country = car.Brand.Country,
                    Name = $"{car.Brand.Name} {car.Model.Name}",
                    Cylinder = car.Cylinder,
                    Doors = car.Doors,
                    GearBox = car.GearBox,
                    Id = car.Id,
                    ImagesUrls = car.ImagesUrls,
                    MaxPrice = car.MaxPrice,
                    MinPrice = car.MinPrice,
                    Model = car.Model.Name,
                    Motor = car.Motor,
                    PowerHorse = car.PowerHorse,
                    Tank = car.Tank,
                    Year = car.Year
                }).ToList(),
                Name = category.Name,
                Id = category.Id
                };
            }

            return null;
        }

    }
}