using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public partial class CardsView : UserControl
    {
        public CardsView()
        {
            InitializeComponent();
        }
        
         private void AccountInfo_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Grid)sender).ActualWidth < 400)
            {
                if (((Grid) sender).ColumnDefinitions.Count == 5)
                    return;
                
                ((Grid) sender).RowDefinitions.Add(new RowDefinition()
                    // ReSharper disable once PossibleNullReferenceException
                    {Height = (GridLength) new GridLengthConverter().ConvertFrom("8.5")});
                ((Grid) sender).RowDefinitions.Add(new RowDefinition()
                    // ReSharper disable once PossibleNullReferenceException
                    {Height = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                
                Grid.SetRow(((Grid)sender).Children[2], 2);
                Grid.SetColumn(((Grid)sender).Children[2], 1);
                
                ((Grid) sender).ColumnDefinitions.RemoveAt(6);
                ((Grid) sender).ColumnDefinitions.RemoveAt(5);
            }
            else
            {
                if (((Grid) sender).ColumnDefinitions.Count == 7)
                    return;

                ((Grid) sender).RowDefinitions.RemoveAt(2);
                ((Grid) sender).RowDefinitions.RemoveAt(1);
                
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("Auto")});
                ((Grid) sender).ColumnDefinitions.Add(new ColumnDefinition
                    // ReSharper disable once PossibleNullReferenceException
                    {Width = (GridLength) new GridLengthConverter().ConvertFrom("*")});
                
                Grid.SetRow(((Grid)sender).Children[2], 0);
                Grid.SetColumn(((Grid)sender).Children[2], 5);
            }
        }
    }
}