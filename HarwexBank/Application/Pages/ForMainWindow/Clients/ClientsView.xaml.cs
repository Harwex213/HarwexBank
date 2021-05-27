using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HarwexBank
{
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
        }
        private int _barElementsCount;
        
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
        
        private void ControlsBarToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var itemsControl in FindVisualChildren<ItemsControl>(this))
            {
                if (itemsControl.Name != "ClientsInfoBar")
                    continue;
                
                for(var i = 0; i < itemsControl.Items.Count; i++)
                {
                    var uiElement = (ContentPresenter)itemsControl.ItemContainerGenerator.ContainerFromIndex(i);
                    var item = (uiElement.ContentTemplate.FindName("ControlsBarToggleButton", uiElement) as ToggleButton);
                    item.IsChecked = false;
                }
        
                ((ToggleButton) sender).IsChecked = true;
            }
        }
        
        private void ControlsBarToggleButton_OnLoaded(object sender, RoutedEventArgs e)
        {
            _barElementsCount++;
            if (_barElementsCount < 2)
            {
                ControlsBarToggleButton_OnClick(sender, e);
            }
        }

        private void ControlsBar_OnLoaded(object sender, RoutedEventArgs e)
        {
            _barElementsCount = 0;
        }
    }
}