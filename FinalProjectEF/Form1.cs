using FinalProjectEF.DAL.Models;

namespace FinalProjectEF
{
    public partial class Form1 : Form
    {
        Service service = new Service();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewTeams.AllowUserToAddRows = false;
            dataGridViewPlayers.AllowUserToAddRows = false;
            dataGridViewMatches.AllowUserToAddRows = false;
        }

        private void comboBoxTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewTeams.Rows.Clear();
            dataGridViewTeams.Columns[0].ReadOnly = true;

            List<Team>? teams = new List<Team>();

            if (comboBoxTeams.SelectedIndex == 0)
            {
                teams = service.GetAllTeams();
            }
            else if (comboBoxTeams.SelectedIndex == 1)
            {
                teams.Add(service.GetTeamMostVictories());
            }
            else if (comboBoxTeams.SelectedIndex == 2)
            {
                teams.Add(service.GetTeamMostLoses());
            }
            else if (comboBoxTeams.SelectedIndex == 3)
            {
                teams.Add(service.GetTeamMostDrawGames());
            }
            else if (comboBoxTeams.SelectedIndex == 4)
            {
                teams.Add(service.GetTeamMostGoalsScored());
            }
            else if (comboBoxTeams.SelectedIndex == 5)
            {
                teams.Add(service.GetTeamMostGoalsMissed());
            }
            else if (comboBoxTeams.SelectedIndex == 6)
            {
                teams = service.GetTeamsTop3MostPoints();
            }
            else if (comboBoxTeams.SelectedIndex == 7)
            {
                teams.Add(service.GetTeamMostPoints());
            }
            else if (comboBoxTeams.SelectedIndex == 8)
            {
                teams = service.GetTeamsTop3LeastPoints();
            }
            else if (comboBoxTeams.SelectedIndex == 9)
            {
                teams.Add(service.GetTeamLeastPoints());
            }
            else if (comboBoxTeams.SelectedIndex == 10)
            {
                teams.Add(service.GetTeamMostVictories());
            }

            if (teams != null)
            {
                foreach (var t in teams)
                {
                    dataGridViewTeams.Rows.Add(
                        t.id,
                        t.Name,
                        t.City,
                        t.GameWin,
                        t.GameLoss,
                        t.GameTie,
                        t.ScoredGoals,
                        t.MissedHeads
                    );
                }
            }
        }

        private void dataGridViewTeams_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this team? All associated matches and players will also be deleted", "Deleting Team", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DataGridViewRow row = dataGridViewTeams.Rows[e.Row.Index];
                int teamId = Convert.ToInt32(row.Cells["columnId"].Value);
                Team? team = service.GetTeamById(teamId);
                if (team != null)
                {
                    List<Match>? matches = service.GetMatchesByTeam(teamId);
                    List<Player>? players = service.GetPlayersByTeam(teamId);
                    if (matches != null)
                    {
                        foreach (Match match in matches)
                        {
                            service.Remove(match);
                        }
                    }
                    if (players != null)
                    {
                        foreach (Player player in players)
                        {
                            service.Remove(player);
                        }
                    }
                    service.Remove(team);
                    dataGridViewTeams.Rows.RemoveAt(e.Row.Index);
                }
                else
                {
                    MessageBox.Show("Error while deleting team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            e.Cancel = true;
        }
    }
}
