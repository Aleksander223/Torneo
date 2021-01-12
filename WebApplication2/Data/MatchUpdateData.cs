using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class MatchUpdateData
    {
        public int GameId { get; set; }
        public int? Score1 { get; set; }
        public int? Score2 { get; set; }

        public int WinnerId { get; set; }

        public string Link { get; set; }
    }
}
