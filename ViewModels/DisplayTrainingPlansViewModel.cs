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
    public class DisplayTrainingPlansViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public List<TrainingPlanModel> TrainingPlans = new();

        public DisplayTrainingPlansViewModel() 
        {
            UpdateTrainingPlans();
        }
        public void UpdateTrainingPlans()
        {
            dbConnection.Open();
            SqlDataReader reader = dbConnection.ExecuteQuery($"SELECT * FROM [dbo].[TrainingPlan]");

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader[0]);
                DateTime date = Convert.ToDateTime(reader[1]);
                string activitiesString = reader[2].ToString();
                int teamId = Convert.ToInt32(reader[3]);
                string[] activityIdsString = activitiesString.Split(',');
                int[] activityIds = Array.ConvertAll(activityIdsString, int.Parse);

                
                TrainingPlanModel trainingPlan = new TrainingPlanModel(id, date, teamId, activityIds);
                TrainingPlans.Add(trainingPlan);
            }

            dbConnection.Close();
        }

        public List<string> ShowActivitiesFromTrainingPlan(TrainingPlanModel trainingPlan)
        {
            DisplayActivitiesViewModel displayActivities = new();
            List<string> activities = new();
            
            foreach (int activityID in trainingPlan.ActivityIds)
            {
                displayActivities.Activities.Where(x => x.Id == activityID).Select(x => x.Name).ToString();
            }
            
            return activities;
        }

        public TrainingPlanModel? GetMostRecentTrainingPlan(int teamId)
        {
            return GetPreviousTrainingPlans(teamId, 1).First();
        }

        public List<TrainingPlanModel>? GetPreviousTrainingPlans(int teamId, int size)
        {
            UpdateTrainingPlans();
            //we find all tplans belonging to the team
            List<TrainingPlanModel>? previousTrainingPlans = TrainingPlans.FindAll(delegate (TrainingPlanModel trainingPlan)
            {
                return trainingPlan.TeamId == teamId;
            });

            //if we find one ore more tplans this code will run
            if (previousTrainingPlans.Any())
            {
                previousTrainingPlans = previousTrainingPlans.OrderByDescending(x => x.Date).ToList();
                int count = previousTrainingPlans.Count();

                previousTrainingPlans.RemoveRange(size, count - 3);
                return previousTrainingPlans;

            }

            else
            {
                return null;
            }
        }
    }
}
