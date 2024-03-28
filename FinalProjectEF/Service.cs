using FinalProjectEF.DAL;
using FinalProjectEF.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectEF
{
    public class Service
    {

        private readonly Repository<Team> _teamRepository;
        private readonly Repository<Player> _playerRepository;
        private readonly Repository<Match> _matchRepository;
        private readonly Repository<GoalScorer> _scorerRepository;
        private readonly Repository<object> _repository;

        public Service()
        {
            _teamRepository = new Repository<Team>();
            _playerRepository = new Repository<Player>();
            _matchRepository = new Repository<Match>();
            _scorerRepository = new Repository<GoalScorer>();
        }
        public void DisplayTeamWithMostWins()
        {

            var teamWithMostWins = _teamRepository.GetAll().OrderByDescending(t => t.GameWin).FirstOrDefault();
            if (teamWithMostWins != null)
            {
                MessageBox.Show($"Team with the most wins: {teamWithMostWins.Name} \nWins:{teamWithMostWins.GameWin}");
            }
            else
            {
                MessageBox.Show("No team information available.");
            }

        }

        public List<Team> GetAllTeams()
        {
            return _teamRepository.GetAll<Team>();
        }

        public void DisplayTeamWithMostLosses()
        {

            var teamWithMostLosses = _teamRepository.GetAll().OrderByDescending(t => t.GameLoss).FirstOrDefault();
            if (teamWithMostLosses != null)
            {
                MessageBox.Show($"Team with the most losses: {teamWithMostLosses.Name} \nLose's:{teamWithMostLosses.GameLoss}");
            }
            else
            {
                MessageBox.Show("No team information available.");
            }

        }

        public void DisplayTeamWithMostDraws()
        {

            var teamWithMostDraws = _teamRepository.GetAll().OrderByDescending(t => t.GameTie).FirstOrDefault();
            if (teamWithMostDraws != null)
            {
                MessageBox.Show($"Team with the most draws: {teamWithMostDraws.Name} \nDraws: {teamWithMostDraws.GameTie}");
            }
            else
            {
                MessageBox.Show("No team information available.");
            }

        }


        public void SearchTeamByName(TextBox textBox)
        {
            string Name = textBox.Text;
            var team = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == Name.ToUpper());
            if (team != null)
            {
                MessageBox.Show($"Name:{team.Name} \nCity:{team.City} \nWins:{team.GameWin} \nLoss:{team.GameLoss} \nTie's:{team.GameTie} \nScored goals:{team.ScoredGoals} \nMissed Goals:{team.MissedHeads}");

            }
            else
            {
                MessageBox.Show($"Team with name '{Name}' not found.");
            }

        }

        public void SearchTeamsByCity(TextBox textBox)
        {

            string city = textBox.Text;
            var teams = _teamRepository.GetAll().Where(t => t.City.ToUpper() == city.ToUpper());
            if (teams.Any())
            {
                MessageBox.Show($"Team's in City '{city}':");
                foreach (var team in teams)
                {
                    MessageBox.Show($"Information abt team in '{city}' \nName:{team.Name} \nCity:{team.City} \nWins:{team.GameWin} \nLoss:{team.GameLoss} \nTie's:{team.GameTie} \nScored goals:{team.ScoredGoals} \nMissed Goals:{team.MissedHeads}");

                }
            }
            else
            {
                MessageBox.Show($"Team with city name '{city}' not found.");
            }

        }

        public void SearchTeamByNameAndCity(TextBox textBox, TextBox textBox1)
        {

            string city = textBox.Text;
            string Name = textBox1.Text;
            var team = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == Name.ToUpper() && t.City.ToUpper() == city.ToUpper());
            if (team != null)
            {
                MessageBox.Show($"Information abt team '{Name}' in '{city}' \nName:{team.Name} \nCity:{team.City} \nWins:{team.GameWin} \nLoss:{team.GameLoss} \nTie's:{team.GameTie} \nScored goals:{team.ScoredGoals} \nMissed Goals:{team.MissedHeads}");

            }
            else
            {
                MessageBox.Show($"Team with name '{Name}' in city '{city}' not found.");
            }

        }


        public void ShowDataTeam()
        {
            var teams = _teamRepository.GetAll().ToList();
            foreach (var football in teams)
            {
                MessageBox.Show($"Name:{football.Name} \nCity:{football.City} \nWins:{football.GameWin} \nLoss:{football.GameLoss} \nTie's:{football.GameTie} \nScored goals:{football.ScoredGoals} \nMissed Goals:{football.MissedHeads}");

            }

        }

        public void ShowGoalDifference()
        {

            var teams = _teamRepository.GetAll()
                .ToList();

            foreach (var team in teams)
            {
                int goalsScored = 0;
                int goalsConceded = 0;

                var matches = _matchRepository.GetAll()
                    .Where(m => m.Team1Id == team.id || m.Team2Id == team.id)
                    .ToList();


                foreach (var match in matches)
                {
                    if (match.Team1 == team)
                    {
                        goalsScored += match.GoalsTeam1;
                        goalsConceded += match.GoalsTeam2;
                    }
                    else
                    {
                        goalsScored += match.GoalsTeam2;
                        goalsConceded += match.GoalsTeam1;
                    }
                }

                int goalDifference = goalsScored - goalsConceded;

                MessageBox.Show($"Team: {team.Name} \nGoals Scored: {goalsScored} \nGoals Conceded: {goalsConceded} \nGoal Difference: {goalDifference}");
            }

        }

        public void PopulateMatchesTable(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7, TextBox textBox8, DateTimePicker dateTimePicker)
        {
            Match match = new Match();

            string team1Name = textBox1.Text;
            var team1 = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == team1Name.ToUpper());
            if (team1 == null)
            {
                MessageBox.Show($"Team '{team1Name}' not found.");
                return;
            }
            match.Team1Id = team1.id;

            string team2Name = textBox2.Text;
            var team2 = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == team2Name.ToUpper());
            if (team2 == null)
            {
                MessageBox.Show($"Team '{team2Name}' not found.");
                return;
            }
            match.Team2Id = team2.id;

            int goalsTeam1;
            while (!int.TryParse(textBox3.Text, out goalsTeam1) || goalsTeam1 < 0)
            {
                MessageBox.Show("Please enter a valid non-negative number.");
            }
            match.GoalsTeam1 = goalsTeam1;

            int goalsTeam2;
            while (!int.TryParse(textBox4.Text, out goalsTeam2) || goalsTeam2 < 0)
            {
                MessageBox.Show("Please enter a valid non-negative number.");
            }
            match.GoalsTeam2 = goalsTeam2;

            DateTime matchDate;
            while (!DateTime.TryParse(dateTimePicker.Text, out matchDate))
            {
                MessageBox.Show("Please enter a valid date in the format yyyy-MM-dd.");
            }
            match.MatchDate = matchDate;
            _matchRepository.Add(match);

            AddGoalScorersToMatch(match.Id, textBox5, textBox6, textBox7, textBox8); ;

            MessageBox.Show("Matches added successfully.");
        }

        public void AddGoalScorersToMatch(int matchId, TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
        {
            var matchToUpdate = _matchRepository.GetAll()
                .AsQueryable()
                .Include(m => m.GoalScorers).FirstOrDefault(m => m.Id == matchId);
            if (matchToUpdate != null)
            {

                int playerId1 = int.Parse(textBox1.Text);
                int playerId2 = int.Parse(textBox2.Text);


                int goalsScored1 = int.Parse(textBox3.Text);
                int goalsScored2 = int.Parse(textBox4.Text);

                var player1 = _playerRepository.GetAll().AsQueryable().FirstOrDefault(p => p.Id == playerId1);
                var player2 = _playerRepository.GetAll().AsQueryable().FirstOrDefault(p => p.Id == playerId2);
                if (player1 != null)
                {
                    matchToUpdate.GoalScorers.Add(new GoalScorer { Player = player1, GoalsScored = goalsScored1 });
                }
                else
                {
                    MessageBox.Show($"Player with ID '{playerId1}' not found.");
                }
                if (player2 != null)
                {
                    matchToUpdate.GoalScorers.Add(new GoalScorer { Player = player2, GoalsScored = goalsScored2 });
                }
                else
                {
                    MessageBox.Show($"Player with ID '{playerId2}' not found.");
                }


                _matchRepository.Update(matchId, matchToUpdate);
            }
            else
            {
                MessageBox.Show($"Match with id '{matchId}' not found.");
            }

        }//s




        public void DisplayPlayersByTeamName(TextBox textBox)
        {
            string Name = textBox.Text;


            var team = GetTeamByName(Name);
            if (team == null)
            {
                MessageBox.Show($"Team with the name '{Name}' does not exist.");
                return;
            }

            if (team.Players == null || !team.Players.Any())
            {
                MessageBox.Show($"There are no players for team '{Name}'.");
                return;
            }

            MessageBox.Show($"Players for team '{Name}':");
            foreach (var player in team.Players)
            {
                MessageBox.Show($"Name: {player.FullName} \nJersey Number: {player.JerseyNumber} \nPosition: {player.Position}");
            }

        }




        public Team MapDataTeam(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7)
        {
            Team football = new Team();

            string Name = textBox1.Text;

            if (GetTeamByName(Name) != null)
            {
                MessageBox.Show($"Team with the name '{Name}' already exists in the application.");
                return null;
            }

            football.Name = Name;

            football.City = textBox2.Text;

            int.TryParse(textBox3.Text, out int buffer);
            football.GameWin = buffer;

            int.TryParse(textBox4.Text, out int buffer1);
            football.GameLoss = buffer1;

            int.TryParse(textBox5.Text, out int buffer2);
            football.GameTie = buffer2;

            int.TryParse(textBox6.Text, out int buffer3);
            football.ScoredGoals = buffer3;

            int.TryParse(textBox7.Text, out int buffer4);
            football.MissedHeads = buffer4;

            return football;
        }


        public void InsertDataTeam(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7)
        {
            Team team = MapDataTeam(textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox7);
            if (team != null)
            {
                _teamRepository.Add(team);
                MessageBox.Show("Team added successfully");
            }
        }

        public Player MapDataPlayer(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5)
        {
            Player player = new Player();

            string fullName = textBox1.Text;

            if (PlayerExistsInAnyTeam(fullName))
            {
                MessageBox.Show("Player exist's in team");
                return null;
            }


            player.FullName = fullName;

            player.Country = textBox2.Text;

            int.TryParse(textBox3.Text, out int jerseyNumber);
            player.JerseyNumber = jerseyNumber;

            player.Position = textBox4.Text;

            string Name = textBox5.Text;

            if (GetTeamByName(Name) == null)
            {
                MessageBox.Show($"Team '{Name}' not found. Player cannot be added without a valid team.");
                return null;
            }

            if (PlayerExistsInTeam(fullName, GetTeamByName(Name))) return null;

            player.TeamId = GetTeamByName(Name).id;


            return player;
        }

        private bool PlayerExistsInAnyTeam(string fullName)
        {
            return _playerRepository.GetAll().Any(p => p.FullName.ToUpper() == fullName.ToUpper());
        }

        public Team GetTeamByName(string Name)
        {
            var player = _teamRepository.GetAll().AsQueryable()
                .Include(t => t.Players)
                            .FirstOrDefault(t => t.Name.ToUpper() == Name.ToUpper());

            if (player != null)
            {
                return player;
            }
            else
            {
                return null;
            }
        }


        public Player GetById(int playerId)
        {
            return _playerRepository.GetAll().FirstOrDefault(p => p.Id == playerId);
        }


        private bool PlayerExistsInTeam(string fullName, Team team)
        {
            return _playerRepository.GetAll().Any(p => p.FullName.ToUpper() == fullName.ToUpper() && p.Team.id == team.id);
        }


        public void InsertDataPlayer(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5)
        {
            Player player = MapDataPlayer(textBox1, textBox2, textBox3, textBox4, textBox5);
            if (player != null)
            {
                _playerRepository.Add(player);
                MessageBox.Show("Player added successfully");
            }

        }

        //1
        public void UpdateTeamData()
        {
            MessageBox.Show("Football Team Name:");
            string Name = Console.ReadLine();

            var team = GetTeamByName(Name);

            if (team != null)
            {
                MessageBox.Show($"Team '{Name}' found. Select the parameter to change:");

                MessageBox.Show("1. Team Name");
                MessageBox.Show("2. Team City");
                MessageBox.Show("3. Number of Wins");
                MessageBox.Show("4. Number of Losses");
                MessageBox.Show("5. Number of Draws");
                MessageBox.Show("6. Goals Scored");
                MessageBox.Show("7. Goals Conceded");

                Console.Write("Select an option: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            MessageBox.Show("Football Team Name:");
                            team.Name = Console.ReadLine();
                            break;
                        case 2:
                            MessageBox.Show("Football Team City:");
                            team.City = Console.ReadLine();
                            break;
                        case 3:
                            MessageBox.Show("Football Team Wins:");
                            int.TryParse(Console.ReadLine(), out int wins);
                            team.GameWin = wins;
                            break;
                        case 4:
                            MessageBox.Show("Football Team Losses:");
                            int.TryParse(Console.ReadLine(), out int losses);
                            team.GameLoss = losses;
                            break;
                        case 5:
                            MessageBox.Show("Football Team Draws:");
                            int.TryParse(Console.ReadLine(), out int draws);
                            team.GameTie = draws;
                            break;
                        case 6:
                            MessageBox.Show("Football Team Goals Scored:");
                            int.TryParse(Console.ReadLine(), out int scoredGoals);
                            team.ScoredGoals = scoredGoals;
                            break;
                        case 7:
                            MessageBox.Show("Football Team Goals Conceded:");
                            int.TryParse(Console.ReadLine(), out int concededGoals);
                            team.MissedHeads = concededGoals;
                            break;
                        default:
                            MessageBox.Show("Invalid choice.");
                            break;
                    }

                    _teamRepository.Update(team.id, team);
                    MessageBox.Show($"Team '{Name}' data updated successfully.");
                }
                else
                {
                    MessageBox.Show("Invalid input.");
                }
            }
            else
            {
                MessageBox.Show($"Team '{Name}' not found.");
            }

        }

        //2
        public void UpdateMatch()
        {
            MessageBox.Show("Enter Match ID:");
            int matchId = int.Parse(Console.ReadLine());


            var matchToUpdate = _matchRepository.GetAll().FirstOrDefault(m => m.Id == matchId);

            if (matchToUpdate != null)
            {
                MessageBox.Show($"Match found with ID: {matchId}");
                MessageBox.Show("Select the parameter to change:");
                MessageBox.Show("1. Team 1");
                MessageBox.Show("2. Team 2");
                MessageBox.Show("3. Goals for Team 1");
                MessageBox.Show("4. Goals for Team 2");
                MessageBox.Show("5. Match Date");
                MessageBox.Show("6. Update Goal Scorers");

                Console.Write("Enter your choice: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            MessageBox.Show("Enter new Team 1:");
                            string team1Name = Console.ReadLine();
                            var team1 = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == team1Name.ToUpper());
                            if (team1 != null)
                            {
                                matchToUpdate.Team1 = team1;
                                MessageBox.Show("Team 1 updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show($"Team '{team1Name}' not found.");
                            }
                            break;
                        case 2:
                            MessageBox.Show("Enter new Team 2:");
                            string team2Name = Console.ReadLine();
                            var team2 = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == team2Name.ToUpper());
                            if (team2 != null)
                            {
                                matchToUpdate.Team2 = team2;
                                MessageBox.Show("Team 2 updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show($"Team '{team2Name}' not found.");
                            }
                            break;
                        case 3:
                            MessageBox.Show("Enter new goals for Team 1:");
                            int goalsTeam1;
                            if (int.TryParse(Console.ReadLine(), out goalsTeam1))
                            {
                                matchToUpdate.GoalsTeam1 = goalsTeam1;
                                MessageBox.Show("Goals for Team 1 updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Invalid input. Goals must be a non-negative integer.");
                            }
                            break;
                        case 4:
                            MessageBox.Show("Enter new goals for Team 2:");
                            int goalsTeam2;
                            if (int.TryParse(Console.ReadLine(), out goalsTeam2))
                            {
                                matchToUpdate.GoalsTeam2 = goalsTeam2;
                                MessageBox.Show("Goals for Team 2 updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Invalid input. Goals must be a non-negative integer.");
                            }
                            break;
                        case 5:
                            MessageBox.Show("Enter new Match Date (yyyy-MM-dd):");
                            DateTime matchDate;
                            if (DateTime.TryParse(Console.ReadLine(), out matchDate))
                            {
                                matchToUpdate.MatchDate = matchDate;
                                MessageBox.Show("Match Date updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Invalid input. Please enter a valid date in the format yyyy-MM-dd.");
                            }
                            break;
                        case 6:
                            UpdateGoalScorers(matchToUpdate);
                            break;
                        default:
                            MessageBox.Show("Invalid choice.");
                            break;
                    }

                    _matchRepository.Update(matchId, matchToUpdate);
                }
                else
                {
                    MessageBox.Show("Invalid input.");
                }
            }
            else
            {
                MessageBox.Show($"Match with ID '{matchId}' not found.");
            }

        }

        //
        public void DeleteMatch(TextBox textBox1, TextBox textBox2, DateTimePicker dateTimePicker,string confirmation)
        {
            string team1Name = textBox1.Text;

            string team2Name = textBox2.Text;

            DateTime matchDate;
            while (!DateTime.TryParse(dateTimePicker.Text, out matchDate))
            {
                MessageBox.Show("Please enter a valid date in the format yyyy-MM-dd.");
            }


            var matchToDelete = _matchRepository
                .GetAll()
                .AsQueryable()
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .FirstOrDefault(m =>
                    (m.Team1.Name.ToUpper() == team1Name.ToUpper() && m.Team2.Name.ToUpper() == team2Name.ToUpper() ||
                     m.Team1.Name.ToUpper() == team2Name.ToUpper() && m.Team2.Name.ToUpper() == team1Name.ToUpper()) &&
                    m.MatchDate.Date == matchDate.Date);

            if (matchToDelete != null)
            {
                MessageBox.Show($"Match found: \nMatch ID: {matchToDelete.Id} \nTeam 1: {matchToDelete.Team1.Name} \nTeam 2: {matchToDelete.Team2.Name} \nMatch Date: {matchToDelete.MatchDate}");

                if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    _matchRepository.Remove(matchToDelete.Id);
                    MessageBox.Show("Match deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Deletion cancelled.");
                }
            }
            else
            {
                MessageBox.Show("No match found with the specified details.");
            }

        }

        //3
        private void UpdateGoalScorers(Match match)
        {
            MessageBox.Show("Enter the number of goal scorers:");
            int numberOfScorers = int.Parse(Console.ReadLine());

            using (var context = new Context())
            {
                context.Matches.Attach(match);

                context.Entry(match).Collection(m => m.GoalScorers).Load();

                match.GoalScorers.Clear();

                for (int i = 0; i < numberOfScorers; i++)
                {
                    MessageBox.Show($"Enter the player ID for goal scorer {i + 1}:");
                    int playerId = int.Parse(Console.ReadLine());

                    MessageBox.Show($"Enter the number of goals scored by player {playerId}:");
                    int goalsScored = int.Parse(Console.ReadLine());

                    var player = context.Players.FirstOrDefault(p => p.Id == playerId);
                    if (player != null)
                    {
                        match.GoalScorers.Add(new GoalScorer { Player = player, GoalsScored = goalsScored });
                    }
                    else
                    {
                        MessageBox.Show($"Player with ID '{playerId}' not found.");
                    }
                }

                context.SaveChanges();
            }
        }



        //next
        public void DisplayMatchDetails(TextBox textBox)
        {
            int matchId = int.Parse(textBox.Text);

            var match = _matchRepository.GetAll()
                .AsQueryable()
                .Include(m => m.GoalScorers)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .FirstOrDefault(m => m.Id == matchId);
            if (match == null)
            {
                MessageBox.Show($"Match with id '{matchId}' not found.");
                return;
            }

            MessageBox.Show($"Match ID: {match.Id} \nMatch Date: {match.MatchDate} \nGoals scored by Team 1 ({match.Team1.Name}): {match.GoalsTeam1} \nGoals scored by Team 2 ({match.Team2.Name}): {match.GoalsTeam2}");
            MessageBox.Show("Players who participated in the match:");
            foreach (var goalScorer in match.GoalScorers)
            {
                if (goalScorer != null)
                {
                    var player = _playerRepository.GetAll()
                    .AsQueryable()
                    .Include(p => p.Team)
                    .FirstOrDefault(p => p.Id == goalScorer.PlayerId);
                    if (player != null)
                    {
                        MessageBox.Show($"Player Name: {player.FullName}, Team: {player.Team.Name}, Goals Scored: {goalScorer.GoalsScored}");
                    }
                    else
                    {
                        MessageBox.Show("Player information not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Encountered null entry in goal scorers list.");
                }
            }


        }

        public void ShowGoalScorersByDate(DateTimePicker dateTimePicker)
        {
            DateTime date;
            while (!DateTime.TryParse(dateTimePicker.Text, out date))
            {
                MessageBox.Show("Please enter a valid date in the format yyyy-MM-dd.");
            }

            var goals = _scorerRepository
                    .GetAll()
                    .AsQueryable()
                    .Include(g => g.Player)
                    .ThenInclude(p => p.Team)
                    .Where(g => g.Match.MatchDate.Date == date.Date)
                    .ToList();

            if (goals.Any())
            {
                MessageBox.Show($"Goal scorers on {date.ToShortDateString()}:");
                foreach (var goal in goals)
                {
                    var player = goal.Player;
                    MessageBox.Show($"Player: {player.FullName}, Team: {player.Team.Name}, Goals Scored: {goal.GoalsScored}");
                }
            }
            else
            {
                MessageBox.Show($"No goals scored on {date.ToShortDateString()}.");
            }

        }

        public void ShowMatchesByDate(DateTimePicker dateTimePicker)
        {
            DateTime date;
            while (!DateTime.TryParse(dateTimePicker.Text, out date))
            {
                MessageBox.Show("Please enter a valid date in the format yyyy-MM-dd.");
            }

            var matches = _matchRepository
            .GetAll()
            .AsQueryable()
            .Include(m => m.Team1)
            .Include(m => m.Team2)
            .Where(m => m.MatchDate.Date == date.Date).ToList();

            if (matches.Any())
            {
                MessageBox.Show($"Matches on {date.ToShortDateString()}:");
                foreach (var match in matches)
                {
                    MessageBox.Show($"Match ID: {match.Id} \nDate: {match.MatchDate} \nTeam 1: {match.Team1.Name}, Goals: {match.GoalsTeam1} \nTeam 2: {match.Team2.Name}, Goals: {match.GoalsTeam2}");

                }
            }
            else
            {
                MessageBox.Show($"No matches found on {date.ToShortDateString()}.");
            }

        }
        public void ShowMatchesByTeam(TextBox textBox)
        {
            string Name = textBox.Text;

            var matches = _matchRepository
            .GetAll()
            .AsQueryable()
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Where(m => m.Team1.Name == Name || m.Team2.Name == Name)
                .ToList();

            if (matches.Any())
            {
                MessageBox.Show($"Matches involving {Name}:");
                foreach (var match in matches)
                {
                    MessageBox.Show($"Match ID: {match.Id} \nDate: {match.MatchDate} \nTeam 1: {match.Team1.Name}, Goals: {match.GoalsTeam1} \nTeam 2: {match.Team2.Name}, Goals: {match.GoalsTeam2}");
                }
            }
            else
            {
                MessageBox.Show($"No matches found involving {Name}.");
            }

        }



        public void DeleteTeam(TextBox textBox)
        {
            string Name = textBox.Text;

            var teamToDelete = _teamRepository.GetAll().FirstOrDefault(t => t.Name.ToUpper() == Name.ToUpper());

            if (teamToDelete != null)
            {
                DialogResult dialogResult = MessageBox.Show($"Team {Name} \nDelete?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult.ToString() == "Yes")
                {
                    _teamRepository.Remove(teamToDelete.id);
                    MessageBox.Show($"Team '{Name}' deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Deletion cancelled.");
                }
            }
            else
            {
                MessageBox.Show($"Team '{Name}' not found.");
            }

        }

        public void DisplayTopScorers(TextBox textBox)
        {
            string teamName = textBox.Text;

            var topScorers = _scorerRepository.GetAll()
            .AsQueryable()
            .Include(s => s.Player)
            .Include(s => s.Match)
            .Where(s => s.Player.Team.Name == teamName)
            .ToList()
            .GroupBy(s => s.Player)
            .Select(g => new { Player = g.Key, GoalsScored = g.Sum(s => s.GoalsScored) })
            .OrderByDescending(g => g.GoalsScored)
            .Take(3)
            .ToList();


            MessageBox.Show($"Топ-3 найкращих бомбардирів команди '{teamName}':");
            foreach (var scorer in topScorers)
            {
                MessageBox.Show($"Ім'я: {scorer.Player.FullName} \nКількість забитих м'ячів: {scorer.GoalsScored}");
            }
        }

        public void DisplayTopScorer(TextBox textBox)
        {

            string teamName = textBox.Text;

            var topScorer = _scorerRepository.GetAll()
                .AsQueryable()
                .Include(s => s.Player)
                .Where(s => s.Player.Team.Name == teamName)
                .OrderByDescending(s => s.GoalsScored)
                .FirstOrDefault();

            if (topScorer != null)
            {
                MessageBox.Show($"Найкращий бомбардир команди '{teamName}':");
                MessageBox.Show($"Ім'я: {topScorer.Player.FullName} \nКількість забитих м'ячів: {topScorer.GoalsScored}");
            }
            else
            {
                MessageBox.Show($"Бомбардирів команди '{teamName}' не знайдено.");
            }
        }

        public void DisplayTopScorersOverall()
        {
            var topScorers = _scorerRepository.GetAll()
                .AsQueryable()
                .Include(s => s.Player)
                .AsEnumerable()
                .GroupBy(s => s.Player)
                .Select(g => new { Player = g.Key, TotalGoals = g.Sum(s => s.GoalsScored) })
                .OrderByDescending(g => g.TotalGoals)
                .Take(3)
                .ToList();

            MessageBox.Show("Топ-3 найкращих бомбардирів усього чемпіонату:");
            foreach (var scorer in topScorers)
            {
                MessageBox.Show($"Ім'я: {scorer.Player.FullName}, Кількість забитих м'ячів: {scorer.TotalGoals}");
            }
        }

        public void DisplayTopScorerOverall()
        {
            var topScorer = _scorerRepository.GetAll()
                .AsQueryable()
                .GroupBy(s => s.PlayerId)
                .Select(g => new { PlayerId = g.Key, TotalGoals = g.Sum(s => s.GoalsScored) })
                .OrderByDescending(g => g.TotalGoals)
                .FirstOrDefault();

            if (topScorer != null)
            {
                var player = GetById(topScorer.PlayerId);
                if (player != null)
                {
                    MessageBox.Show($"Найкращий бомбардир усього чемпіонату: {player.FullName} \nКількість забитих м'ячів: {topScorer.TotalGoals}");
                    return;
                }
            }

            MessageBox.Show("Інформація про найкращого бомбардира відсутня.");
        }


        public void DisplayTopTeamsByPoints()
        {
            var topTeams = _teamRepository.GetAll()
                .OrderByDescending(t => (t.GameWin * 3) + t.GameTie)
                .Take(3)
                .ToList();

            MessageBox.Show("Топ-3 команди за кількістю очок:");
            foreach (var team in topTeams)
            {
                int points = (team.GameWin * 3) + team.GameTie;
                MessageBox.Show($"Команда: {team.Name} \nОчки: {points}");
            }
        }


        public void DisplayTopTeamByPoints()
        {
            var topTeam = _teamRepository.GetAll()
                .OrderByDescending(t => (t.GameWin * 3) + t.GameTie)
                .FirstOrDefault();

            if (topTeam != null)
            {
                int points = (topTeam.GameWin * 3) + topTeam.GameTie;
                MessageBox.Show($"Команда з найбільшою кількістю очок: {topTeam.Name} \nОчки: {points}");
            }
            else
            {
                MessageBox.Show("Не вдалося знайти команду.");
            }
        }

        public void DisplayBottomTeamsByPoints()
        {
            var bottomTeams = _teamRepository.GetAll()
                .OrderBy(t => (t.GameWin * 3) + t.GameTie)
                .Take(3)
                .ToList();

            if (bottomTeams.Any())
            {
                MessageBox.Show("Топ-3 команди з найменшою кількістю очок:");
                foreach (var team in bottomTeams)
                {
                    int points = (team.GameWin * 3) + team.GameTie;
                    MessageBox.Show($"Команда: {team.Name} \nОчки: {points}");
                }
            }
            else
            {
                MessageBox.Show("Не вдалося знайти команди.");
            }
        }

        public void DisplayTeamWithLeastPoints()
        {
            var teamWithLeastPoints = _teamRepository.GetAll()
                .OrderBy(t => (t.GameWin * 3) + t.GameTie)
                .FirstOrDefault();

            if (teamWithLeastPoints != null)
            {
                int points = (teamWithLeastPoints.GameWin * 3) + teamWithLeastPoints.GameTie;
                MessageBox.Show($"Команда з найменшою кількістю очок: {teamWithLeastPoints.Name} \nОчки: {points}");
            }
            else
            {
                MessageBox.Show("Не вдалося знайти команду.");
            }
        }

        public void DisplayTopScoringTeams()
        {
            var topScoringTeams = _teamRepository.GetAll()
                .OrderByDescending(t => t.ScoredGoals)
                .Take(3)
                .ToList();

            MessageBox.Show("Топ-3 команди, які забили найбільше голів:");
            foreach (var team in topScoringTeams)
            {
                MessageBox.Show($"Команда: {team.Name} \nЗабиті голи: {team.ScoredGoals}");
            }
        }

        public void DisplayTeamWithMostGoals()
        {
            var teamWithMostGoals = _teamRepository.GetAll()
                .OrderByDescending(t => t.ScoredGoals)
                .FirstOrDefault();

            if (teamWithMostGoals != null)
            {
                MessageBox.Show($"Команда, яка забила найбільше голів: {teamWithMostGoals.Name} \nЗабиті голи: {teamWithMostGoals.ScoredGoals}");
            }
            else
            {
                MessageBox.Show("Не вдалося знайти команду.");
            }
        }

        public void DisplayTopDefensiveTeams()
        {
            var topDefensiveTeams = _teamRepository.GetAll()
                .OrderBy(t => t.MissedHeads)
                .Take(3)
                .ToList();

            MessageBox.Show("Топ-3 команди, які пропустили найменше голів:");
            foreach (var team in topDefensiveTeams)
            {
                MessageBox.Show($"Команда: {team.Name} \nПропущені голи: {team.MissedHeads}");
            }
        }

        public void DisplayTeamWithLeastGoalsConceded()
        {
            var teamWithLeastGoalsConceded = _teamRepository.GetAll()
                .OrderBy(t => t.MissedHeads)
                .FirstOrDefault();

            if (teamWithLeastGoalsConceded != null)
            {
                MessageBox.Show($"Команда, яка пропустила найменше голів: {teamWithLeastGoalsConceded.Name} \nПропущені голи: {teamWithLeastGoalsConceded.MissedHeads}");
            }
            else
            {
                MessageBox.Show("Не вдалося знайти команду.");
            }
        }


        public Team? GetTeamMostVictories()
        {
            return _teamRepository.GetAll<Team>().OrderByDescending(s => s.GameWin).FirstOrDefault();
        }

        public Team? GetTeamMostLoses()
        {
            return _teamRepository.GetAll<Team>().OrderByDescending(s => s.GameLoss).FirstOrDefault();
        }

        public Team? GetTeamMostDrawGames()
        {
            return _teamRepository.GetAll<Team>().OrderByDescending(s => s.GameTie).FirstOrDefault();
        }

        public Team? GetTeamMostGoalsScored()
        {
            return _teamRepository.GetAll<Team>().OrderByDescending(s => s.ScoredGoals).FirstOrDefault();
        }

        public Team? GetTeamMostGoalsMissed()
        {
            return _teamRepository.GetAll<Team>().OrderByDescending(s => s.MissedHeads).FirstOrDefault();
        }


        public List<Team>? GetTeamsTop3MostPoints()
        {
            var top3TeamsByPoints = _teamRepository.GetAll<Team>()
                .OrderByDescending(s => s.GameWin * 3 + s.GameTie)
                .Take(3)
                .ToList();
            if (top3TeamsByPoints.Count == 0)
            {
                return null;
            }
            return top3TeamsByPoints;
        }

        public Team? GetTeamMostPoints()
        {
            var topTeamByPoints = _teamRepository.GetAll<Team>()
                .OrderByDescending(s => s.GameWin * 3 + s.GameTie)
                .FirstOrDefault();
            return topTeamByPoints;
        }

        public List<Team>? GetTeamsTop3LeastPoints()
        {
            var top3TeamsByPoints = _teamRepository.GetAll<Team>()
                .OrderBy(s => s.GameWin * 3 + s.GameTie)
                .Take(3)
                .ToList();
            if (top3TeamsByPoints.Count == 0)
            {
                return null;
            }
            return top3TeamsByPoints;
        }

        public Team? GetTeamLeastPoints()
        {
            var topTeamByPoints = _teamRepository.GetAll<Team>()
                .OrderBy(s => s.GameWin * 3 + s.GameTie)
                .FirstOrDefault();
            return topTeamByPoints;
        }

        public Team? GetTeamById(int teamId)
        {
            return _teamRepository.GetById<Team>(teamId);
        }

        public Match? GetMatchById(int matchId)
        {
            return _matchRepository.GetById<Match>(matchId);
        }

        public List<Player>? GetPlayersByTeam(int teamId)
        {
            return _playerRepository.GetAll<Player>().Where(p => p.TeamId == teamId).ToList();
        }

        public Player? GetPlayerById(int playerId)
        {
            return _playerRepository.GetById<Player>(playerId);
        }

        public List<Match>? GetMatchesByTeam(int teamId)
        {
            List<Match> matches = new List<Match>();
            foreach (var match in _matchRepository.GetAll<Match>())
            {
                if (match.Team1Id == teamId || match.Team2Id == teamId)
                {
                    matches.Add(match);
                }
            }
            return matches.Count != 0 ? matches : null;
        }

        public void Remove(object entity)
        {
            _repository.Delete(entity);
        }
    }
}
