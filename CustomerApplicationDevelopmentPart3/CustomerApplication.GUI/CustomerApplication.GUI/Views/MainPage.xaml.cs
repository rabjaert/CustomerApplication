using System;
using System.Text.RegularExpressions;
using CustomerApplication.GUI.ViewModels;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CustomerApplication.GUI.Views
{
    public sealed partial class MainPage : Page
    {
        /// <summary>The user name pattern</summary>
        private readonly string userNamePattern = @"^[A-Za-z0-9]{1,15}$";
        /// <summary>The password pattern</summary>
        private readonly string passwordPattern = @"^[A-Za-z0-9]{1,15}$";

        /// <summary>The valid username</summary>
        private bool validUsername;
        /// <summary>The valid password</summary>
        private bool validPassword;

        /// <summary>Gets the view model.</summary>
        /// <value>The view model.</value>
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        /// <summary>Handles the Loaded event of the MainPage control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await ViewModel.CheckForInternetError();
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        }


        /// <summary>BTNs the login.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void BtnLogin(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                if (validUsername && validPassword)
                {

#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                    var loginRequest = await ViewModel.LoginUserAsync(txtUserName.Text, txtPassword.Password);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task

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

        /// <summary>Handles the Click event of the Button control that navigates you to the user page.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
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

        /// <summary>Handles the TextChanged event of the TxtUserName control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
        private void TxtUserName_TextChanged(object sender, TextChangedEventArgs e)
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

        /// <summary>Handles the PasswordChanged event of the TxtPassword control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private void TxtPassword_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
