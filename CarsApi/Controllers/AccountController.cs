using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterDto registerDto)
        {
            if (registerDto != null)
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    PersonName = registerDto.PersonName
                };

                // Check if the user exist
                var exist = await _userManager.FindByEmailAsync(registerDto.Email);
                if (exist != null)
                    return BadRequest("The Email Already Exist , Try Another One");

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok(user);
                }
                else
                    return Problem("Error While Creating The User");
            }
            return BadRequest("Sorry, Register Faild, Make Sure To Enter The Requierd Data");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                return user;
            }
            else
            {
                return BadRequest("Make Sure Password And Email Are Correct");
            }
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<ApplicationUser>> CurrentUser()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // User not authenticated
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(); // User not found
            }

            return Ok(user);
        }
    }
}