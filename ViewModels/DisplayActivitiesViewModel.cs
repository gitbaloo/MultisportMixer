using skovbyprojekt.Models;
using skovbyprojekt.Technical_Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skovbyprojekt.ViewModels
{
    public class DisplayActivitiesViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public List<ActivityModel> Activities = new List<ActivityModel>();

        public DisplayActivitiesViewModel() 
        { 
            UpdateActivities();
        }
        public void UpdateActivities()
        {
            dbConnection.Open();
            SqlDataReader reader = dbConnection.ExecuteQuery($"SELECT * FROM [dbo].[Activities]");

            while (reader.Read())
            {
                ActivityModel activity = new ActivityModel(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                Activities.Add(activity);
            }

            dbConnection.Close();
        }

        //Use in conjuction with AddActivityToDatabase in CreateActivityViewModel
        public bool DoesActivityExist(ActivityModel activity) 
        {
            ActivityModel? existingActivity = Activities.Find(delegate (ActivityModel foundActivity)
            {
                return foundActivity.Name == activity.Name;
            });

            if (existingActivity == null) 
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
