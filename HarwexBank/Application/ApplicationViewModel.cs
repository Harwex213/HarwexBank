using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace HarwexBank
{
    public class ApplicationViewModel : BaseControlViewModel
    {
        #region Exites Windows

        private readonly AuthorizationViewModel _authorization;
        private MainViewModel _main;

        #endregion // Existed Windows

        public ApplicationViewModel()
        {
            _authorization = new AuthorizationViewModel(this);
            SelectedControlViewModel = _authorization;
        }

        public void EnterToApplication()
        {
            SelectedControlViewModel = _main ??= new MainViewModel(this);
        }

        public void ExitOfApplication()
        {
            SelectedControlViewModel = _authorization;
        }
    }
}