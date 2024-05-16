using Core.Dtos;
using Core.Entities;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class FavService : IFavService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public FavService(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> AddToFavourate(string userEmail, int carId)
    {
        // Retrieve user by email
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            return false;
        }

        // Retrieve car by ID
        var car = await _context.Cars.FindAsync(carId);
        if (car == null)
        {
            return false;
        }

        // Check if the car is already a favorite
        var existingFavorite = await _context.UserFavProducts
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.ProductId == carId);

        if (existingFavorite != null)
        {
            return false; // Already a favorite
        }

        // Add to favorites
        var userFavProduct = new UserFavProducts
        {
            UserId = user.Id,
            ProductId = carId
        };

        _context.UserFavProducts.Add(userFavProduct);
        await _context.SaveChangesAsync();

        return true;
    }
    
}