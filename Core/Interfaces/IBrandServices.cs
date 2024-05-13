using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBrandServices
    {
        Task<IReadOnlyList<BrandDto>> GetBrands();
        Task<bool> AddBrand(string name , string country);
        Task<BrandDto?> GetBrandById(int id);
    }
}