using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Infrastructure;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {

        private readonly AppDbContext context = new AppDbContext();

        [HttpPost]
        public ActionResult Post([FromBody] UserLoginData userData)
        {
            var user = context.Users.SingleOrDefault(a => a.Email == userData.Email);

            if (user == null)
            {
                return ValidationProblem("Invalid email or password");
            }

            if (!BCrypt.Net.BCrypt.Verify(userData.Password, user.Password))
            {
                return ValidationProblem("Invalid email or password");
            }


            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { identity });
            HttpContext.SignInAsync(userPrincipal);

            return Ok();
        }
    }
}
