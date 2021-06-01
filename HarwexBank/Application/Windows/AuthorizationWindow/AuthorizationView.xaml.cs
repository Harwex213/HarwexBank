using System.Windows;
using System.Windows.Controls;
using HarwexBank.Languages;

namespace HarwexBank
{
    public partial class AuthorizationView : UserControl
    {
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new ChangeLanguage();
        }
    }
}