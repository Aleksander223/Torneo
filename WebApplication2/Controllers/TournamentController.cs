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
    [Route("tournaments")]
    public class TournamentController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] TournamentCreateData tournamentData)
        {
            var game = context.Games.FirstOrDefault(x => x.Id == tournamentData.GameId);
            var tournament = new Tournament
            {
                Name = tournamentData.Name,
                Description = tournamentData.Description,
                Game = game
            };

            context.Add(tournament);
            context.SaveChanges();

            if (tournamentData.Size != 2 && tournamentData.Size != 4 && tournamentData.Size != 8 && tournamentData.TeamIds.Count != tournamentData.Size)
            {
                return ValidationProblem();
            }

            foreach (var teamId in tournamentData.TeamIds)
            {
                var team = context.Teams.FirstOrDefault(t => t.Id == teamId);

                context.TeamTournaments.Add(new TeamTournament { TeamId = team.Id, TournamentId = tournament.Id });
            }

            var teams = tournamentData.TeamIds.ToList();
            int matchCounter = 0;

            // create matches
            for (int i = 0; i < teams.Count; i+=2)
            {
                var team1 = context.Teams.FirstOrDefault(t => t.Id == teams[i]);
                var team2 = context.Teams.FirstOrDefault(t => t.Id == teams[i+1]);

                var match = new Match
                {
                    Game = game,
                    TournamentId = tournament.Id,
                    TournamentOrder=matchCounter
                };

                matchCounter++;

                context.Add(match);
                context.SaveChanges();

                context.TeamMatches.Add(new TeamMatch { MatchId = match.Id, TeamId = team1.Id });
                context.TeamMatches.Add(new TeamMatch { MatchId = match.Id, TeamId = team2.Id });

                context.SaveChanges();
            }

            if (tournamentData.Size == 4)
            {
                var match = new Match
                {
                    Game = game,
                    TournamentId = tournament.Id,
                    TournamentOrder=matchCounter
                };

                matchCounter++;

                context.Add(match);
                context.SaveChanges();
            }


            if (tournamentData.Size == 8)
            {
                var match1 = new Match
                {
                    Game = game,
                    TournamentId = tournament.Id,
                    TournamentOrder = matchCounter
                };

                matchCounter++;

                var match2 = new Match
                {
                    Game = game,
                    TournamentId = tournament.Id,
                    TournamentOrder = matchCounter
                };

                matchCounter++;

                var match3 = new Match
                {
                    Game = game,
                    TournamentId = tournament.Id,
                    TournamentOrder = matchCounter
                };

                matchCounter++;

                context.Add(match1);
                context.Add(match2);
                context.Add(match3);
                context.SaveChanges();
            }

            return Ok(tournament);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var tournaments = context.Tournaments.Include(t => t.Game).Include(t => t.TeamTournaments).ThenInclude(t => t.Team).Include(t => t.Matches).ThenInclude(t => t.TeamMatches).ThenInclude(t => t.Team).ToList();
            return Ok(tournaments);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var tournament = context.Tournaments.Include(t => t.Game).Include(t => t.TeamTournaments).ThenInclude(t => t.Team).Include(t => t.Matches).ThenInclude(t => t.TeamMatches).ThenInclude(t => t.Team).FirstOrDefault(t => t.Id == id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var tournament = context.Tournaments.Include(t => t.Matches).FirstOrDefault(m => m.Id == id);

            if (tournament == null)
            {
                return NotFound("Tournament does not exist");
            }

            var teamTournaments = context.TeamTournaments.Where(x => x.TournamentId == id);

            foreach (var tt in teamTournaments)
            {
                context.Remove(tt);
            }

            foreach(var match in tournament.Matches)
            {
                context.Remove(match);
            }

            context.Remove(tournament);

            context.SaveChanges();

            return Ok(tournament);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Put(int id, [FromBody] TournamentUpdateData tournamentData)
        {
            var tournament = context.Tournaments.FirstOrDefault(m => m.Id == id);

            if (tournament == null)
            {
                return NotFound("Tournament does not exist");
            }

            if (tournamentData.Description != null && tournamentData.Description != "")
            {
                tournament.Description = tournamentData.Description;
            }

            if (tournamentData.Name != null && tournamentData.Name != "")
            {
                tournament.Name = tournamentData.Name;
            }

            context.SaveChanges();

            return Ok(tournament);
        }
    }
}
