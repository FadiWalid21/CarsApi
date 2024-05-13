using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Core.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "User name can't be null")]
        public string PersonName { get; set; } = String.Empty;
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Email can't be null")]
        [EmailAddress(ErrorMessage ="Eamil should be in vaild format")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password can't be null")]
        public string Password { get; set; }
    }
}