using System;
using CustomerApplication.GUI.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CustomerApplication.GUI.Views
{
    public sealed partial class LoggedInPage : Page
    {
        /// <summary>Gets the view model.</summary>
        /// <value>The view model.</value>
        public LoggedInViewModel ViewModel { get; } = new LoggedInViewModel();
        /// <summary>Gets the main view model.</summary>
        /// <value>The main view model.</value>
        public MainViewModel MainViewModel { get; } = new MainViewModel();

        public LoggedInPage()
        {
            InitializeComponent();
            Loaded += LoggedInPage_Loaded;
        }

        /// <summary>Handles the Loaded event of the LoggedInPage control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void LoggedInPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await MainViewModel.CheckForInternetError();
            await MainViewModel.LoadEmployeesAsync();
            
            var currentEmployee = ViewModel.GetCurrentEmployee();
            if (currentEmployee != default)
            {
                textBlockUser.Text = "Hello, " + " " + currentEmployee.FirstName + " " + currentEmployee.LastName;
                textBlockId.Text = "Id:" + " " + currentEmployee.Id;
                textBlockFirstName.Text = "Firstname:" + " " + currentEmployee.FirstName;
                textLastname.Text = "Lastname:" + " " + currentEmployee.LastName;
                textEmail.Text = "Email:" + " " + currentEmployee.Email;
                textTelephonenumber.Text = "Telephonenumber:" + " " + currentEmployee.TelephoneNumber;
                textUsername.Text = "Username:" + " " + currentEmployee.Username;
                textCompanyId.Text = "CompanyId:" + " " + currentEmployee.CompanyId;
            }
            else
                Frame.Navigate(typeof(MainPage));
        }


        /// <summary>A button that lets you log out.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private void BtnLogOut(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ClearCurrentEmployee();
            Frame.Navigate(typeof(MainPage)); 

        }
        /// <summary>A button that navigates you to the company.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private void BtnCompany(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ClearCurrentEmployee();
            Frame.Navigate(typeof(JoinACompanyPage));

        }

    }
}
