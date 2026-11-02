using skovbyprojekt.Models;
using skovbyprojekt.Technical_Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace skovbyprojekt.ViewModels
{
    public class CreateTrainingPlanViewModel : BaseViewModel
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        DisplayTrainingPlansViewModel displayTrainingPlans = new();

        public TrainingPlanModel CreateNewTrainingPlan(int teamId, int size, int activityCombination)
        {
            TrainingPlanModel newTrainingPlan = new TrainingPlanModel(teamId, size, activityCombination);

            displayTrainingPlans.UpdateTrainingPlans();

            List<TrainingPlanModel>? previousTrainingPlans = displayTrainingPlans.GetPreviousTrainingPlans(teamId, size); //we create a nullable list that will old previous tplans
            
            DisplayActivitiesViewModel displayActivities = new();

            List<ActivityModel> availableActivities = displayActivities.Activities.OrderBy(x => x.Id).ToList(); //We create a list that at this point holds all activities but will later be altered
            
            if (previousTrainingPlans != null)
            {
                foreach (TrainingPlanModel trainingPlan in previousTrainingPlans)
                {
                    foreach (int activityId in trainingPlan.ActivityIds)
                    {
                        availableActivities.RemoveAt(activityId + 1);
                    }
                }
            }

            //Here we extract the activities based on type and put it into new lists
            List<ActivityModel> availableWarmupActivities = availableActivities.FindAll(delegate (ActivityModel warmup)
            {
                return warmup.Type == "Opvarmning";
            });
            List<ActivityModel> availableSportActivities = availableActivities.FindAll(delegate (ActivityModel sport)
            {
                return sport.Type == "Sport";
            });
            List<ActivityModel> availableCooldownActivities = availableActivities.FindAll(delegate (ActivityModel cooldown)
            {
                return cooldown.Type == "Nedkøling";
            });



            if (activityCombination == 3) //activityCombination is to be used for other modes than the standard 3 activities (warmup, sport, cooldown). ActivityCombination will be 1 for standard
            {
                Random rnd = new Random();
                int index;

                index = rnd.Next(availableWarmupActivities.Count);
                ActivityModel warmup = availableWarmupActivities[index];
                newTrainingPlan.ActivityIds[0] = warmup.Id;

                index = rnd.Next(availableSportActivities.Count);
                ActivityModel sport = availableSportActivities[index];
                newTrainingPlan.ActivityIds[1] = sport.Id;

                index = rnd.Next(availableCooldownActivities.Count);
                ActivityModel cooldown = availableCooldownActivities[index];
                newTrainingPlan.ActivityIds[2] = cooldown.Id;
            }

            AddTrainingPlanToDatabase(newTrainingPlan);
            return newTrainingPlan;
        }
        public string AddTrainingPlanToDatabase(TrainingPlanModel trainingPlan)
        {
            dbConnection.Open();
            try
            {                
                int rowsAffected = dbConnection.ExecuteNonQuery($"INSERT INTO [dbo].[TrainingPlan] (TrainingPlanDate, TrainingPlanActivityIDs, TrainingPlanTeamID) VALUES ('{trainingPlan.Date}', '{string.Join(",", trainingPlan.ActivityIds)}', '{trainingPlan.TeamId}')");
                return rowsAffected.ToString();
            }
            catch (InvalidOperationException ex) 
            {
                return $"An error occurred: {ex.Message}";
            }

            dbConnection.Close();
        }
    }
}
