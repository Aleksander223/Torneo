using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("games")]
    public class GameController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] GameData gameData)
        {
            var game = new Game
            {
                Name = gameData.Name,
                Description = gameData.Description
            };

            context.Add(game);
            context.SaveChanges();

            return Ok(game);
        }

        [HttpGet]
        public ActionResult Get()
        {

            var games = context.Games.ToList();
            return Ok(games);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult GetGame(int id)
        {
            var game = context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound("Game does not exist");
            }

            return Ok(game);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Put(int id, [FromBody] GameData gameData)
        {
            var game = context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound("Game does not exist!");
            }

            if (gameData.Name != null)
            {
                game.Name = gameData.Name;
            }

            if (gameData.Description != null)
            {
                game.Description = gameData.Description;
            }
            
            context.SaveChanges();

            return Ok(game);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var game = context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return Ok("Game deleted");
            }

            context.Remove(game);

            context.SaveChanges();

            return Ok("Game deleted");
        }
    }
}
