using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class TournamentCreateData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int GameId { get; set; }

        public int Size { get; set; }

        public ICollection<int> TeamIds { get; set; }
    }
}
