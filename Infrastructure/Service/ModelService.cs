using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Service
{
    public class ModelService : IModelServices
    {
        private readonly ApplicationDbContext _context;
        public ModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateModel(string name, int brandId, string imageUrl)
        {
            // Check if the model already exists (case-insensitive comparison)
            var models = await GetModels();
            var existingModel = models.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

            if (existingModel != null)
            {
                // Model with the same name already exists, return false
                return false;
            }

            // Create a new Model instance
            Model newModel = new Model
            {
                Name = name,
                ImageUrl = imageUrl,
                BrandId = brandId,
            };

            // Add the new Model to the DbContext and save changes
            await _context.Models.AddAsync(newModel);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ModelToReturn?> GetModelById(int id)
        {
            var model = await _context.Models
            .Include(c=>c.Cars)
            .ThenInclude(car=>car.CarType)
            .Include(ca=>ca.Brand)
            .FirstOrDefaultAsync(m=> m.Id==id);

            if(model != null)
                return new ModelToReturn {
                    Id = model.Id,
                    Name = model.Name,
                    ImageUrl = model.ImageUrl,
                    Cars = model.Cars.Select(car=> new CarToReturnDto{
                        Id = car.Id,
                        Colors = car.Colors,
                        ImagesUrls = car.ImagesUrls,
                        Country = car.Brand.Country,
                        Cylinder = car.Cylinder,
                        CarType = car.CarType.Name,
                        Doors =car.Doors,
                        GearBox = car.GearBox,
                        MaxPrice = car.MaxPrice,
                        MinPrice = car.MinPrice,
                        Model= car.Model.Name,
                        Motor = car.Motor,
                        Name = $"{car.Brand.Name} {car.Model.Name}",
                        PowerHorse = car.PowerHorse,
                        Tank = car.Tank,
                        Year = car.Year
                    }).ToList(),
                };
            
            return null;
        }

        public async Task<IReadOnlyList<Model>> GetModels()
        {
            return await _context.Models
            .Include(m => m.Brand)
            .ToListAsync();
        }
    }
}