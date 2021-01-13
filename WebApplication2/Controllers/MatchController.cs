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
    [Route("matches")]
    public class MatchController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] MatchCreateData matchData)
        {
            var game = context.Games.FirstOrDefault(g => g.Id == matchData.GameId);
            var team1 = context.Teams.FirstOrDefault(t => t.Id == matchData.Team1Id);
            var team2 = context.Teams.FirstOrDefault(t => t.Id == matchData.Team2Id);

            var match = new Match
            {
                Game = game
            };



            context.Add(match);
            context.SaveChanges();

            var teamMatch1 = new TeamMatch
            {
                TeamId = team1.Id,
                MatchId = match.Id
            };

            var teamMatch2 = new TeamMatch
            {
                TeamId = team2.Id,
                MatchId = match.Id
            };

            context.TeamMatches.Add(teamMatch1);
            context.TeamMatches.Add(teamMatch2);

            context.SaveChanges();

            return Ok(match);
        }
        

        [HttpGet]
        public ActionResult Get()
        {
            var matches = context.Matches.ToList();

            return Ok(matches);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult GetMatch(int id)
        {
            var match = context.Matches.FirstOrDefault(x => x.Id == id);

            if (match == null)
            {
                return NotFound("Match does not exist");
            }

            return Ok(match);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Put(int id, [FromBody] MatchUpdateData matchData)
        {
            var match = context.Matches.FirstOrDefault(m => m.Id == id);

            if (match == null)
            {
                return NotFound("Match does not exist");
            }

            if (matchData.GameId != 0)
            {
                var game = context.Games.FirstOrDefault(g => g.Id == matchData.GameId);

                if (game != null)
                {
                    match.Game = game;
                }
            }

            if (matchData.Link != null && matchData.Link != "")
            {
                match.Link = matchData.Link;
            }

            if (matchData.Score1 != null)
            {
                match.Score1 = (int)matchData.Score1;
            }

            if (matchData.Score2 != null)
            {
                match.Score2 = (int)matchData.Score2;
            }

            if (matchData.WinnerId != 0)
            {
                var team = context.Teams.FirstOrDefault(t => t.Id == matchData.WinnerId);

                if (team != null)
                {
                    match.Winner = team;
                }
            }

            context.SaveChanges();

            return Ok(match);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete (int id)
        {
            var match = context.Matches.FirstOrDefault(m => m.Id == id);

            if (match == null)
            {
                return NotFound("Match does not exist");
            }

            context.Remove(match);  

            context.SaveChanges();

            return Ok(match);
        }
    }
}
