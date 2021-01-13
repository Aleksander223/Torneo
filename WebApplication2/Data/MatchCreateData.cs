using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class MatchCreateData
    {
        public int GameId { get; set; }

        public string Link { get; set; }

        public int Team1Id { get; set; }
        public int Team2Id { get; set; }

        public int TournamentId { get; set; }
    }
}
