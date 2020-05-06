using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeManager.ApplicationCore.Models;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;

        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (login == null)
            {
                return Ok(new LoginResult
                {
                    Successful = false,
                    Error = "Username and password must be provided.",
                });
            }

            var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return Ok(new LoginResult
                {
                    Successful = false,
                    Error = "Username and / or password are invalid.",
                });
            }

            var user = await signInManager.UserManager.FindByNameAsync(login.UserName).ConfigureAwait(false);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(configuration["JwtExpiryInDays"], CultureInfo.InvariantCulture));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: credentials);

            return Ok(new LoginResult
            {
                Successful = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
    }
}
