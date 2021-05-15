using System;
using System.Windows;

namespace HarwexBank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                ModelResourcesManager.GetInstance();
                
                var app = new ApplicationView { DataContext = new ApplicationViewModel() };
                app.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Environment.Exit(0);
            }
        }
    }
}