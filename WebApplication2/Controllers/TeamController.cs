using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Infrastructure;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    [ApiController]
    [Route("teams")]
    public class TeamController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] TeamCreateData teamData)
        {

            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            var team = new Team
            {
                Name = teamData.Name
            };

            context.Add(team);
            context.SaveChanges();

            user.TeamId = team.Id;

            context.SaveChanges();
            return Ok(team);
        }

        [Route("join/{id}")]
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public ActionResult JoinTeam(int id)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            var team = context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                return NotFound("Team does not exist");
            }

            user.TeamId = team.Id;
            context.SaveChanges();

            return Ok(team);
        }

        [Route("leave")]
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public ActionResult LeaveTeam(int id)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            user.TeamId = null;
            context.SaveChanges();

            return Ok(user);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var teams = context.Teams.Include(t => t.Members).ToList();

            return Ok(teams);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult GetTeam(int id)
        {
            var team = context.Teams.Include(t => t.Members).FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut]
        public ActionResult Put([FromBody] TeamUpdateData teamData)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            var team = context.Teams.FirstOrDefault(x => x.Id == user.TeamId);
             
            if (team == null)
            {
                return NotFound();
            }

            if (teamData.Name != null)
            {
                team.Name = teamData.Name;
            }

            context.SaveChanges();

            return Ok(team);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult UpdateTeam([FromBody] TeamUpdateData teamData, int id)
        {
            var team = context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            if (teamData.Name != null)
            {
                team.Name = teamData.Name;
            }

            context.SaveChanges();

            return Ok(team);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteTeam(int id)
        {
            var team = context.Teams.FirstOrDefault(x => x.Id == id);
            var users = context.Users.Where(x => x.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            context.Teams.Remove(team);

            foreach (var user in users)
            {
                user.TeamId = null;
            }

            context.SaveChanges();

            return Ok("Team deleted");
        }


    }
}
