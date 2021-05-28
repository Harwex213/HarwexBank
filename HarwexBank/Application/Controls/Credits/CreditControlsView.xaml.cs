using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public partial class CreditControlsView_xaml
    {
        void CreditAdditionalInfo_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ControlsBase.CollapseVisibility_OnLoad(sender, routedEventArgs);
        }
        
        private void GeneralInfo_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 400)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 5)
                    return;
                
                Grid.SetRow(((Grid)sender).Children[2], 1);
                Grid.SetColumn(((Grid)sender).Children[2], 1);
                ((FrameworkElement) ((Grid) sender).Children[2]).Margin = new Thickness(0, 8.5, 0, 0);
                
                Grid.SetRow(((Grid)sender).Children[3], 1);
                Grid.SetColumn(((Grid)sender).Children[3], 3);
                ((FrameworkElement) ((Grid) sender).Children[3]).Margin = new Thickness(0, 8.5, 0, 0);
                
                ((Grid) sender).ColumnDefinitions.RemoveAt(8);
                ((Grid) sender).ColumnDefinitions.RemoveAt(7);
                ((Grid) sender).ColumnDefinitions.RemoveAt(6);
                ((Grid) sender).ColumnDefinitions.RemoveAt(5);
            }
            else
            {
                if (((Grid) sender).ColumnDefinitions.Count == 9)
                    return;
                
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                
                Grid.SetRow(((Grid)sender).Children[2], 0);
                Grid.SetColumn(((Grid)sender).Children[2], 5);
                ((FrameworkElement) ((Grid) sender).Children[2]).Margin = new Thickness(0);
                
                Grid.SetRow(((Grid)sender).Children[3], 0);
                Grid.SetColumn(((Grid)sender).Children[3], 7);
                ((FrameworkElement) ((Grid) sender).Children[3]).Margin = new Thickness(0);
            }
        }
        
        private void AdditionalInfoClient_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 440)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 3)
                    return;
                
                Grid.SetRow(((Grid)sender).Children[1], 1);
                Grid.SetColumn(((Grid)sender).Children[1], 1);
                ((FrameworkElement) ((Grid) sender).Children[1]).Margin = new Thickness(0, 8.5, 0, 0);
                
                Grid.SetRow(((Grid)sender).Children[2], 2);
                Grid.SetColumn(((Grid)sender).Children[2], 1);
                ((FrameworkElement) ((Grid) sender).Children[2]).Margin = new Thickness(0, 8.5, 0, 0);
                
                ((Grid) sender).ColumnDefinitions.RemoveAt(6);
                ((Grid) sender).ColumnDefinitions.RemoveAt(5);
                ((Grid) sender).ColumnDefinitions.RemoveAt(4);
                ((Grid) sender).ColumnDefinitions.RemoveAt(3);
            }
            else
            {
                if (((Grid) sender).ColumnDefinitions.Count == 7)
                    return;
                
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                
                Grid.SetRow(((Grid)sender).Children[1], 0);
                Grid.SetColumn(((Grid)sender).Children[1], 3);
                ((FrameworkElement) ((Grid) sender).Children[1]).Margin = new Thickness(0);
                
                Grid.SetRow(((Grid)sender).Children[2], 0);
                Grid.SetColumn(((Grid)sender).Children[2], 5);
                ((FrameworkElement) ((Grid) sender).Children[2]).Margin = new Thickness(0);
            }
        }

        private void AdditionalInfo_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 800)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 7)
                    return;
                
                Grid.SetRow(((Grid)sender).Children[3], 1);
                Grid.SetColumn(((Grid)sender).Children[3], 1);
                ((FrameworkElement) ((Grid) sender).Children[3]).Margin = new Thickness(0, 8.5, 0, 0);
                
                Grid.SetRow(((Grid)sender).Children[4], 1);
                Grid.SetColumn(((Grid)sender).Children[4], 3);
                ((FrameworkElement) ((Grid) sender).Children[4]).Margin = new Thickness(0, 8.5, 0, 0);
                
                ((Grid) sender).ColumnDefinitions.RemoveAt(8);
                ((Grid) sender).ColumnDefinitions.RemoveAt(7);
                ((Grid) sender).ColumnDefinitions.RemoveAt(6);
                ((Grid) sender).ColumnDefinitions.RemoveAt(5);
            }
            else
            {
                if (((Grid) sender).ColumnDefinitions.Count == 11)
                    return;
                
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                
                Grid.SetRow(((Grid)sender).Children[3], 0);
                Grid.SetColumn(((Grid)sender).Children[3], 7);
                ((FrameworkElement) ((Grid) sender).Children[3]).Margin = new Thickness(0);
                
                Grid.SetRow(((Grid)sender).Children[4], 0);
                Grid.SetColumn(((Grid)sender).Children[4], 9);
                ((FrameworkElement) ((Grid) sender).Children[4]).Margin = new Thickness(0);
            }
        }
    }
}