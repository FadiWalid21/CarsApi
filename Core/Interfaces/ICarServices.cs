using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICarServices
    {
        Task<IReadOnlyList<CarToReturnDto>> GetCars();
        Task<CarToReturnDto?> GetCarById(int id);
        Task<IReadOnlyList<CarToReturnDto>?> SearchCars(string name);
        Task<IReadOnlyList<CarToReturnDto>?> GetFavourites(string userEmail);
        Task<bool> AddCar(CarDto carDto);
        Task<bool> DeleteCar(int id);
    }
}