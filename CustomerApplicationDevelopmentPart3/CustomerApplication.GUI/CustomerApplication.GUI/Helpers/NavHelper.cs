using System;

using Microsoft.UI.Xaml.Controls;

using Windows.UI.Xaml;

namespace CustomerApplication.GUI.Helpers
{
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class NavHelper
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        // This helper class allows to specify the page that will be shown when you click on a NavigationViewItem
        //
        // Usage in xaml:
        // <winui:NavigationViewItem x:Uid="Shell_Main" Icon="Document" helpers:NavHelper.NavigateTo="views:MainPage" />
        //
        // Usage in code:
        // NavHelper.SetNavigateTo(navigationViewItem, typeof(MainPage));
        public static Type GetNavigateTo(NavigationViewItem item)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            return (Type)item.GetValue(NavigateToProperty);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        public static void SetNavigateTo(NavigationViewItem item, Type value)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            item.SetValue(NavigateToProperty, value);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(null));
    }
}
