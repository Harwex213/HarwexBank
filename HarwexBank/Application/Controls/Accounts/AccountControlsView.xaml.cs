﻿using System.Windows;
using System.Windows.Controls;

namespace HarwexBank
{
    public partial class AccountControlsView
    {
        void AccountAdditionalInfo_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ControlsBase.CollapseVisibility_OnLoad(sender, routedEventArgs);
        }
    }
}