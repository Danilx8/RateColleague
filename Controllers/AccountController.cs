using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using NuGet.Common;
using RateColleague.Data;
using RateColleague.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RateColleague.Controllers
{
    [ApiController]
    public class AccountController(ApplicationDbContext _db, 
        UserManager<Employee> _userManager) : ControllerBase
    {
        private readonly ApplicationDbContext db = _db;
        private readonly UserManager<Employee> userManager = _userManager;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            var result = await userManager.CreateAsync(new Employee
            {
                UserName = dto.Name,
                Email = dto.Email,
            }, dto.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(Credentials credentials)
        {
            var user = db.Users.Where(u => u.Email == credentials.Email).FirstOrDefault();
            if (user == null || !await userManager.CheckPasswordAsync(user, credentials.Password))
            {
                return BadRequest("Пользователь не найден");
            }

            var claimsIdentity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            ], "Cookies");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
