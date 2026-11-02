using skovbyprojekt.Models;
using skovbyprojekt.Technical_Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skovbyprojekt.ViewModels
{
    public class CreateManualTrainingPlanViewModel : BaseViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();

        DisplayActivitiesViewModel displayActivities = new();     
        
        public TrainingPlanModel CreateNewManualTrainingPlan(int teamId, ActivityModel[] activities) 
        {
            //We use a lambda expression to select activityIDs in the ActivityModel array and put these ints into an int array
            int[] activityIDs = activities.Select(activity => activity.Id).ToArray();

            TrainingPlanModel newTrainingPlan = new TrainingPlanModel(teamId, activityIDs);

            AddTrainingPlanToDatabase(newTrainingPlan);
            return newTrainingPlan;
        }

        public void AddTrainingPlanToDatabase(TrainingPlanModel trainingPlan)
        {
            dbConnection.Open();
            dbConnection.ExecuteQuery($"INSERT INTO [dbo].[TrainingPlan] (TrainingPlanDate, TrainingPlanActivityID, TeamID) VALUES ({trainingPlan.Date}, {string.Join(",", trainingPlan.ActivityIds)}, {trainingPlan.TeamId})"); //needs completion
            dbConnection.Close();
        }
    }
}
