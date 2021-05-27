using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HarwexBank
{
    public partial class JournalClientView : UserControl
    {
        public JournalClientView()
        {
            InitializeComponent();
        }
        private int _barElementsCount;
        
        private void ControlsBarToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            for(var i = 0; i < JournalBar.Items.Count; i++)
            {
                var uiElement = (ContentPresenter)JournalBar.ItemContainerGenerator.ContainerFromIndex(i);
                var item = (uiElement.ContentTemplate.FindName("ControlsBarToggleButton", uiElement) as ToggleButton);
                item.IsChecked = false;
            }
        
            ((ToggleButton) sender).IsChecked = true;
        }
        
        private void ControlsBarToggleButton_OnLoaded(object sender, RoutedEventArgs e)
        {
            _barElementsCount++;
            if (_barElementsCount < 2)
            {
                ControlsBarToggleButton_OnClick(sender, e);
            }
        }
    }
}