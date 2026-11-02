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
    public class CreateTeamViewModel : BaseViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public string CreateNewTeam(string name, string? description)
        {
            if (name == null)
            {
                return "Der skal angives navn";
            }
           
            else
            {
                TeamModel newTeam = new TeamModel(name, description);
                return AddTeamToDatabase(newTeam);
            }
        }
        public string AddTeamToDatabase(TeamModel team)
        {
            DisplayTeamsViewModel displayTeams = new();

            if (!displayTeams.DoesTeamExist(team))
            {
                dbConnection.Open();
                dbConnection.ExecuteQuery($"INSERT INTO [dbo].[Team] (TeamName, TeamDescription) VALUES ({team.Name}, {team.Description}");
                dbConnection.Close();
                return "Holdet blev tilføjet til databasen";
            }
            else
            {
                return "Et hold med samme navn eksisterer allerede";
            }
        }
    }
}
