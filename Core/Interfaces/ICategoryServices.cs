using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryServices
    {
        Task<IReadOnlyList<CategoryDto>> GetCategories();
        Task<CategoryDto?> GetCategory(int id);
        Task<bool> CreateCategory(string name );
    }
}