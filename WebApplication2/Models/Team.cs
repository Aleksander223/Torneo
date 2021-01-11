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
        public string Name { get; set; }

        public ICollection<User> Members { get; set; }

        public ICollection<Match> Matches { get; set; }

        public ICollection<Tournament> Tournaments { get; set; }

        public Team()
        {
            Members = new List<User>();
            Matches = new List<Match>();
            Tournaments = new List<Tournament>();
        }
    }
}
