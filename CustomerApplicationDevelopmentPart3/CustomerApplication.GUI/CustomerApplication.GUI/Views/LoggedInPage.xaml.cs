using System;
using System.Text.Json;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.ViewModels;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace CustomerApplication.GUI.Views
{
    public sealed partial class LoggedInPage : Page
    {
        public LoggedInViewModel ViewModel { get; } = new LoggedInViewModel();
        public MainViewModel MainViewModel { get; } = new MainViewModel();



        public LoggedInPage()
        {
            InitializeComponent();
            Loaded += LoggedInPage_Loaded;
        }

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


        private void BtnLogOut(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ClearCurrentEmployee();
            Frame.Navigate(typeof(MainPage)); 

        }
        private void BtnCompany(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ClearCurrentEmployee();
            Frame.Navigate(typeof(JoinACompanyPage));

        }

    }
}
