using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email Should Be In Right Format")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}