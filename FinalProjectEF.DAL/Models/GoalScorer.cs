using System.Text.RegularExpressions;

namespace FinalProjectEF.DAL.Models
{
    public class GoalScorer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int GoalsScored { get; set; }
        public Player Player { get; set; }
        public Match Match { get; set; }
    }
}
