using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Description;
using WebApplication2.Data;
using WebApplication2.Infrastructure;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();

        [HttpPost]
        public ActionResult Post([FromBody] UserSignUpData userData)
        {
            var SameEmailOrUserName = context.Users.FirstOrDefault(u => u.Email == userData.Email || u.UserName == userData.UserName);

            if (SameEmailOrUserName != null)
            {
                return ValidationProblem("Username or email already in use");
            }

            var role = userData.SuperSecretOverride == "Han shot first" ? "Admin" : "User";

            var user = new User
            {
                UserName = userData.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(userData.Password),
                Email = userData.Email,
                Role = role,
                Bio = "I am " + userData.UserName
            };
  
            context.Add(user);
            context.SaveChanges();
            return Ok(user);
        }



        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public ActionResult Get()
        {
            var users = context.Users.Include(u => u.Team).ToList();

            return Ok(users);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Ok();
            }

            context.Remove(user);
            context.SaveChanges();

            return Ok();
        }

        [Route("me")]
        [Authorize(Roles = "User,Admin")]
        [HttpDelete]
        public ActionResult DeleteMyself()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            context.Remove(user);
            context.SaveChanges();

            return Ok("User deleted");
        }

        
        [Authorize(Roles = "User,Admin")]
        [HttpPut]
        public ActionResult Put([FromBody] UserUpdateData userData)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            if (userData.Bio != null)
            {
                user.Bio = userData.Bio;
            }

            if (userData.Password != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userData.Password);
            }

            if (userData.TeamId != 0)
            {
                var team = context.Teams.FirstOrDefault(x => x.Id == userData.TeamId);

                if (team == null)
                {
                    return NotFound("Team does not exist");
                }

                user.TeamId = team.Id;
                team.Members.Add(user);
                context.Update(team);
            }

            context.Update(user);
            context.SaveChanges();

            return Ok(user);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult UpdateSomeone([FromBody] UserUpdateData userData, int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            if (userData.Bio != null)
            {
                user.Bio = userData.Bio;
            }

            if (userData.Password != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userData.Password);
            }

            if (userData.TeamId != 0)
            {
                var team = context.Teams.FirstOrDefault(x => x.Id == userData.TeamId);

                if (team == null)
                {
                    return NotFound("Team does not exist");
                }

                user.TeamId = team.Id;
                team.Members.Add(user);
                context.Update(team);
            }

            context.Update(user);
            context.SaveChanges();

            return Ok(user);
        }

        [Route("me")]
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public ActionResult Status()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.Include(u => u.Team).ThenInclude(t => t.Members).FirstOrDefault(x => x.UserName == userName);

            return Ok(user);
        }
        
    }
}
