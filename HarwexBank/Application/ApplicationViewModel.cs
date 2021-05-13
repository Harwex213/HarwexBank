using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HarwexBank
{
    public class ApplicationViewModel : BaseControlViewModel
    {

        public ApplicationViewModel()
        {
            MinHeight = 600;
            MinWidth = 800;
            
            SelectedControlViewModel = new AuthorizationViewModel(this);
        }
        
        public double MinHeight { get; set; }
        public double MinWidth { get; set; }

        #region Commands

        // Fields.
        private ICommand _enterToApplicationCommand;
        private ICommand _exitOfApplicationCommand;

        // Props.
        public ICommand EnterToApplicationCommand
        {
            get
            {
                _enterToApplicationCommand ??= new RelayCommand(
                    c => EnterToApplication());

                return _enterToApplicationCommand;
            }
        }
        
        public ICommand ExitOfApplicationCommand
        {
            get
            {
                _exitOfApplicationCommand ??= new RelayCommand(
                    c => ExitOfApplication());

                return _exitOfApplicationCommand;
            }
        }
        
        // Methods.
        private void EnterToApplication()
        {
            SelectedControlViewModel = new MainViewModel(this);
        }

        private void ExitOfApplication()
        {
            SelectedControlViewModel = new AuthorizationViewModel(this);
        }

        #endregion
    }
}