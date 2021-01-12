using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Infrastructure;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("tournaments")]
    public class TournamentController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();


        [Authorize(Roles = "Admin")]
        [HttpPost]
        ActionResult Post([FromBody] TournamentCreateData tournamentData)
        {
            return Ok();
        }

        [HttpGet]
        ActionResult Get()
        {
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        ActionResult GetById(int id)
        {
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        ActionResult Delete(int id)
        {
            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        ActionResult Put(int id, [FromBody] TournamentCreateData tournamentData)
        {
            return Ok();
        }
    }
}
