using skovbyprojekt.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace skovbyprojekt.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // creates a new iNotify (BaseViewModel) and calls it selectedviewmodel. 
        private BaseViewModel selectedViewModel = new CreateTrainingPlanViewModel();

        //our selectedViewmodel which keeps track of where the user is in the navigation
        public BaseViewModel SelectedViewModel 
        { 
          get { return selectedViewModel; } 
          set 
          { 
            selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel)); 
          } 
        }

        // an ICommand called UpdateViewCommand which is later called and set equal to a new UpdateViewCommand(a class in the commands folder)
        public ICommand UpdateViewCommand { get; set; } 

        public MainWindowViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
