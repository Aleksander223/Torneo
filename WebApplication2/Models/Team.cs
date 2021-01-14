using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public ICollection<User> Members { get; set; }

        public ICollection<TeamMatch> TeamMatches { get; set; }

        public ICollection<TeamTournament> TeamTournaments { get; set; }
    }

    public class TeamMatch
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int MatchId { get; set; }

        public Match Match { get; set; }
    }

    public class TeamTournament
    {
        public int  TeamId { get; set; }
        public Team Team { get; set; }

        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
