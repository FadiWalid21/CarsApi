using Core.Dtos;

namespace Core.Interfaces;

public interface IFavService
{
    Task<bool> AddToFavourate(string userEmail,int carId);

}