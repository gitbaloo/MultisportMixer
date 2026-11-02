using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skovbyprojekt.Models;
using skovbyprojekt.Technical_Services;

namespace skovbyprojekt.ViewModels
{
    public class CreateActivityViewModel : BaseViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();

        public string CreateNewActivity(string name, string type, string? description, string? tools, string? rules)
        {            
            if (name == null && type == null)
            {
                return "Der skal angives både navn og type";
            }
            else if (name == null) 
            {
                return "Der skal angives navn";
            }
            else if (type == null) 
            {
                return "Der skal angives type";
            }
            else
            {
                ActivityModel newActivity = new ActivityModel(name, type, description, tools, rules);
                return AddActivityToDatabase(newActivity);
            }
        }

        public string AddActivityToDatabase(ActivityModel activity)
        {
            DisplayActivitiesViewModel displayActivities = new();

            if (!displayActivities.DoesActivityExist(activity))
            {
                dbConnection.Open();
                dbConnection.ExecuteQuery($"INSERT INTO [dbo].[Activities] (ActivityName, ActivityType, ActivityTools, ActivityDescription, ActivityRules) VALUES ({activity.Name}, {activity.Type}, {activity.Tools}, {activity.Description}, {activity.Rules}");
                dbConnection.Close();
                return "Aktiviteten blev tilføjet til databasen";
            }
            else
            {
                return "En aktivitet med samme navn eksisterer allerede";
            }
        }
    }
}
