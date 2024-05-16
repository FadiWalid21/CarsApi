using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string PersonName { get; set; }
        public string? ImageUrl { get; set; }
        public List<UserFavProducts> FavouriteCars { get; set; } = new List<UserFavProducts>();
    }
}