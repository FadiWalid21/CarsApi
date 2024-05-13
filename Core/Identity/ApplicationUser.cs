using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string PersonName { get; set; }
        public string? ImageUrl { get; set; }
    }
}