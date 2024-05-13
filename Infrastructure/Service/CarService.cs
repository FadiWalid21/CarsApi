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
    public class CarService : ICarServices
    {
        private readonly ApplicationDbContext _context;
        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCar(CarDto carDto)
        {
            var model = await _context.Models.FindAsync(carDto.ModelId);

            if (carDto != null && model != null)
            {
                Car newCar = new Car
                {
                    Doors = carDto.Doors,
                    GearBox = carDto.GearBox,
                    Cylinder = carDto.Cylinder,
                    ImagesUrls = carDto.ImagesUrls,
                    Colors = carDto.Colors,
                    MinPrice = carDto.MinPrice,
                    MaxPrice = carDto.MaxPrice,
                    PowerHorse = carDto.PowerHorse,
                    Tank = carDto.Tank,
                    Year = carDto.Year,
                    CategoryId = carDto.CategoryId,
                    BrandId = carDto.BrandId,
                    ModelId = carDto.ModelId,
                    Motor = carDto.Motor
                };

                await _context.Cars.AddAsync(newCar);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCar(int id)
        {
            var carToDelete = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (carToDelete != null)
            {
                _context.Cars.Remove(carToDelete);
                return true;
            }

            return false;
        }

        public async Task<CarToReturnDto?> GetCarById(int id)
        {
            var car = await _context.Cars
            .Include(c => c.CarType)
            .Include(c => c.Brand)
            .Include(c => c.Model)
            .FirstOrDefaultAsync(c => c.Id == id);
            if (car != null)
            {
                return new CarToReturnDto
                {
                    Id = car.Id,
                    Name = $"{car.Brand.Name} {car.Model.Name}",
                    CarType = car.CarType.Name,
                    Colors = car.Colors,
                    ImagesUrls = car.ImagesUrls,
                    Country = car.Brand.Country,
                    Doors = car.Doors,
                    Cylinder = car.Cylinder,
                    GearBox = car.GearBox,
                    MaxPrice = car.MaxPrice,
                    MinPrice = car.MinPrice,
                    Model = car.Model.Name,
                    Motor = car.Motor,
                    PowerHorse = car.PowerHorse,
                    Tank = car.Tank,
                    Year = DateTime.Parse(car.Year.ToString("yyyy/MM/dd")),
                };
            }
            return null;
        }


        public async Task<IReadOnlyList<CarToReturnDto>> GetCars()
        {
            var cars = await _context.Cars
                .AsNoTracking()
                .Include(c => c.CarType)
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .ToListAsync();

            return cars.Select(car => new CarToReturnDto
            {
                Id = car.Id,
                Name = $"{car.Brand.Name} {car.Model.Name}",
                CarType = car.CarType.Name,
                Colors = car.Colors,
                ImagesUrls = car.ImagesUrls,
                Country = car.Brand.Country,
                Doors = car.Doors,
                Cylinder = car.Cylinder,
                GearBox = car.GearBox,
                MaxPrice = car.MaxPrice,
                MinPrice = car.MinPrice,
                Model = car.Model.Name,
                Motor = car.Motor,
                PowerHorse = car.PowerHorse,
                Tank = car.Tank,
                Year = DateTime.Parse(car.Year.ToString("yyyy/MM/dd"))
            }).ToList();
        }

        public async Task<IReadOnlyList<CarToReturnDto>?> SearchCars(string name)
        {
            var cars = _context.Cars
                .Where(car => (car.Brand.Name + car.Model.Name).ToLower().Contains(name.ToLower()))
                .Include(c => c.CarType)
                .Include(c => c.Brand)
                .Include(c => c.Model);



            if (cars != null)
                return cars.Select(car => new CarToReturnDto
                {
                    Id = car.Id,
                    Name = $"{car.Brand.Name} {car.Model.Name}",
                    CarType = car.CarType.Name,
                    Colors = car.Colors,
                    ImagesUrls = car.ImagesUrls,
                    Country = car.Brand.Country,
                    Doors = car.Doors,
                    Cylinder = car.Cylinder,
                    GearBox = car.GearBox,
                    MaxPrice = car.MaxPrice,
                    MinPrice = car.MinPrice,
                    Model = car.Model.Name,
                    Motor = car.Motor,
                    PowerHorse = car.PowerHorse,
                    Tank = car.Tank,
                    Year = DateTime.Parse(car.Year.ToString("yyyy/MM/dd")),
                }).ToList();

            return null;
        }
    }
}