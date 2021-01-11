using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ICollection<Team> Teams { get; set; }

        [Required]
        public Game Game { get; set; }

        public int Winner { get; set; }

        public int Score1 { get; set; }
        public int Score2 { get; set; }

        public string Link { get; set; }

        public Tournament Tournament { get; set; }

    }
}
