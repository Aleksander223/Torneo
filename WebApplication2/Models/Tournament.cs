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
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public User Admin { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<Match> Matches { get; set; }
        
        [Required]
        public Game Game { get; set; }
    }
}
