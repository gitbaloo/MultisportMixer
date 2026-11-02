using skovbyprojekt.Views;
using skovbyprojekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace skovbyprojekt.Commands
{
    public class UpdateViewCommand : ICommand
    {
        //instanciating viewmodel 
        private MainWindowViewModel viewModel;
        //updating our viewmodel
        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }
        // our 'controller' that controls which view to show when the buttons equal to the name set is pressed.
        public void Execute(object? parameter)
        {            
            if (parameter.ToString() == "CreateActivity")
            {
                viewModel.SelectedViewModel = new CreateActivityViewModel();
            }
            else if (parameter.ToString() == "HomePage")
            {
                viewModel.SelectedViewModel = new CreateTrainingPlanViewModel();
            }
            else if (parameter.ToString() == "CreateTeam")
            {
                viewModel.SelectedViewModel = new CreateTeamViewModel();
            }
            else if (parameter.ToString() == "CreateManualTrainingPlan")
            {
                viewModel.SelectedViewModel = new CreateManualTrainingPlanViewModel();
            }
        }
    }
}
