using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using skovbyprojekt.ViewModels;

namespace skovbyprojekt.Views
{
    /// <summary>
    /// Interaction logic for CreateTrainingPlanView.xaml
    /// </summary>
    public partial class CreateTrainingPlanView : UserControl
    {
        CreateTrainingPlanViewModel createTrainingPlanViewModel = new CreateTrainingPlanViewModel();
        DisplayTrainingPlansViewModel displayTrainingPlans = new DisplayTrainingPlansViewModel();

        List<string> activities = new List<string>();

        int selectedTeamId = 1; //Needs to be set from choosing the team from the combobox next to "generer træningsplan"
        int selectedSize = 3; //For now we stick to the standard size of 3 activities but this can be changes later if the user wants a bigger programme
        int selectedActivityCombination = 3; //Here we also stick to the standard combination of warmup->sport->cooldown and 3 will signal that. This can also be changed later at the behest of PO/users

        public CreateTrainingPlanView()
        {
            InitializeComponent();
        }
        

        private void ___Generate__Click(object sender, RoutedEventArgs e)
        {
            activities.Clear();
            activities = displayTrainingPlans.ShowActivitiesFromTrainingPlan(createTrainingPlanViewModel.CreateNewTrainingPlan(selectedTeamId, selectedSize, selectedActivityCombination));
            DisplayNew.ItemsSource = activities;
        }
    }
}
