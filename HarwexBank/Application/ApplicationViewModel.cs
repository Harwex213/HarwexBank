using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HarwexBank
{
    public class ApplicationViewModel : BaseControlViewModel
    {
        #region Existed Windows

        private readonly AuthorizationViewModel _authorization;
        private MainViewModel _main;

        #endregion // Existed Windows

        public ApplicationViewModel()
        {
            MinHeight = 600;
            MinWidth = 800;
            
            _authorization = new AuthorizationViewModel(this);
            SelectedControlViewModel = _authorization;
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
            SelectedControlViewModel = _main ??= new MainViewModel(this);
        }

        private void ExitOfApplication()
        {
            SelectedControlViewModel = _authorization;
        }

        #endregion
    }
}