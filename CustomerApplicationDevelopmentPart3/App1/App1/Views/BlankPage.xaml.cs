using System;

using App1.ViewModels;
using Windows.Devices.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace App1.Views
{
    public sealed partial class BlankPage : Page
    {
        public BlankViewModel ViewModel { get; } = new BlankViewModel();

        public BlankPage()
        {
            InitializeComponent();
            //btn1.Background = new SolidColorBrush(Color.FromArgb(99, 255, 0, 0));
            


        }

        private void btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            
            // this.Frame.Navigate(typeof(MainPage));
            btn1.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
        }

        private void button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void pathEnter(object sender, PointerRoutedEventArgs e)
        {

          
            sirkel.Fill = new SolidColorBrush(Windows.UI.Colors.White);

        }

        private void pathClose(object sender, PointerRoutedEventArgs e)
        {

            sirkel.Fill = new SolidColorBrush(Windows.UI.Colors.DarkGray);
        }
    }
}
