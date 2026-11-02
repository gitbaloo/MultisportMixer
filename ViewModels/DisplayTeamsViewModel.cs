using skovbyprojekt.Models;
using skovbyprojekt.Technical_Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skovbyprojekt.ViewModels
{
    public class DisplayTeamsViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public List<TeamModel> Teams = new List<TeamModel>();

        public void UpdateTeams()
        {
            dbConnection.Open();
            SqlDataReader reader = dbConnection.ExecuteQuery($"SELECT * FROM [dbo].[Team]");

            while (reader.Read())
            {
                TeamModel team = new TeamModel(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString());
                Teams.Add(team);
            }

            dbConnection.Close();
        }

        public bool DoesTeamExist(TeamModel team)
        {
            TeamModel? existingTeam = Teams.Find(delegate (TeamModel foundTeam)
            {
                return foundTeam.Name == team.Name;
            });

            if (existingTeam == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
