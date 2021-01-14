using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        public ICollection<TeamTournament> TeamTournaments { get; set; }

        public ICollection<Match> Matches { get; set; }
        
        [Required]
        public Game Game { get; set; }
    }
}
