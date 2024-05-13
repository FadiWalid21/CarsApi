using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service
{
    public class BrandService : IBrandServices
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBrand(string name, string country)
        {
            var brands = await GetBrands();

            var exist = brands.FirstOrDefault(b => String.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));

            if (exist != null)
                return false;

            var Brand = new Brand
            {
                Name = name,
                Country = country
            };

            await _context.Brands.AddAsync(Brand);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<BrandDto?> GetBrandById(int id)
        {
            var brand = await _context.Brands
            .Include(b => b.Cars)
            .ThenInclude(car => car.CarType)
            .Include(b => b.Models)
            .FirstOrDefaultAsync(b => b.Id == id);
            if (brand != null)
                return new BrandDto
                {
                    Cars = brand.Cars.Select(car => new CarToReturnDto
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
                    Models = brand.Models.Select(m => new ModelDto
                    {
                        Id = m.Id,
                        ImageUrl = m.ImageUrl,
                        Name = m.Name
                    }).ToList(),
                    Id = brand.Id,
                    Name = brand.Name
                };

            return null;
        }

        public async Task<IReadOnlyList<BrandDto>> GetBrands()
        {
            var brands = await _context.Brands
            .Include(b => b.Cars)
            .ThenInclude(car => car.CarType)
            .Include(b => b.Models)
            .ToListAsync();

            var data = brands.Select(b => new BrandDto
            {
                Cars = b.Cars.Select(car => new CarToReturnDto
                {
                    CarType = car.CarType?.Name,
                    Colors = car.Colors,
                    Country = car.Brand?.Country,
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
                Models = b.Models.Select(m => new ModelDto
                {
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Name = m.Name
                }).ToList(),
                Id = b.Id,
                Name = b.Name
            }).ToList();

            return data;
        }
    }
}