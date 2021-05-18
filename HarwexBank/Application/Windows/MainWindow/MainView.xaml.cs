using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HarwexBank
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        private int _barElementsCount = 0;
        
        private void LeftBarToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            for(var i = 0; i < LeftNavbar.Items.Count; i++)
            {
                var uiElement = (ContentPresenter)LeftNavbar.ItemContainerGenerator.ContainerFromIndex(i);
                var item = (uiElement.ContentTemplate.FindName("LeftBarToggleButton", uiElement) as ToggleButton);
                item.IsChecked = false;
            }
        
            ((ToggleButton) sender).IsChecked = true;
        }
        
        private void LeftBarToggleButton_OnLoaded(object sender, RoutedEventArgs e)
        {
            _barElementsCount++;
            if (_barElementsCount < 2)
            {
                LeftBarToggleButton_OnClick(sender, e);
            }
        }
    }
}