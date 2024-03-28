﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectEF.DAL.Models
{
    public class Team
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int GameLoss { get; set; }
        public int GameWin { get; set; }
        public int GameTie { get; set; }
        public int ScoredGoals { get; set; }
        public int MissedHeads { get; set; }
        public List<Player> Players { get; set; }
    }
}
