using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.DataTransferObject;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.ViewModels;
using Newtonsoft.Json;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CustomerApplication.GUI.Views
{
    public sealed partial class MainPage : Page
    {
        private readonly string userNamePattern = @"^[A-Za-z0-9]{1,15}$";
        private readonly string passwordPattern = @"^[A-Za-z0-9]{1,15}$";

        private bool validUsername;
        private bool validPassword;

        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.CheckForInternetError();
        }


        private async void btnLogin(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                if (validUsername && validPassword)
                {

                    var loginRequest = await ViewModel.LoginUserAsync(txtUserName.Text, txtPassword.Password);

                    if (loginRequest)
                    {
                        txtExceptionMessage.Text = "Login successful";
                        this.Frame.Navigate(typeof(LoggedInPage));

                    }
                    else
                        this.Frame.Navigate(typeof(MainPage));
                }
                else
                    txtExceptionMessage.Text = "None of the fields can be empty.";


            }

            catch (NullReferenceException)
            {

                txtExceptionMessage.Text = "Login was not successful.\nAre you sure you typed the correct password?";



            }

        }   

            private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterUserPage));
        }

        private void Button_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Button_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void Button_PointerEntered_1(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Button_PointerExited_1(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            validUsername = Regex.IsMatch(txtUserName.Text, userNamePattern);

            if (!validUsername)
            {
                txtUserName.Text = "";
                txtUserName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validUsername)

                txtUserName.BorderBrush = new SolidColorBrush(Colors.Green);
        }

        private void txtPassword_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            validPassword = Regex.IsMatch(txtPassword.Password, passwordPattern);

            if (!validPassword)
            {
                txtPassword.Password = "";
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validPassword)

                txtPassword.BorderBrush = new SolidColorBrush(Colors.Green);
        }
    }
}
