using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IModelServices
    {
        Task<IReadOnlyList<Model>> GetModels();
        Task<ModelToReturn?> GetModelById(int id);
        Task<bool> CreateModel(string name,int brandId,string imageUrl);
    }
}