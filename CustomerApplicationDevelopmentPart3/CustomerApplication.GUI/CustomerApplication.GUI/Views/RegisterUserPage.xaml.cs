using System;
using System.Net.Http;
using System.Text;
using CustomerApplication.GUI.Core.DataTransferObject;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.ViewModels;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;
using CustomerApplication.GUI.Core.Datahandler;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.Devices.HumanInterfaceDevice;
using System.Text.RegularExpressions;
using Windows.Security.Cryptography.Core;
using System.Net;

namespace CustomerApplication.GUI.Views
{
    public sealed partial class RegisterUserPage : Page
    {
        public RegisterUserViewModel ViewModel { get; } = new RegisterUserViewModel();
        public MainViewModel MainViewModel { get; } = new MainViewModel();

        private readonly string namingPattern = @"^[/a-zA-Z]+${1,30}";
        private readonly string phoneNumberPattern = "^[0-9]+$";
        private readonly string emailPattern = @"[A-Za-z0-9@.-]+${1,15}";
        private readonly string userNamePattern = @"^[A-Za-z0-9]{1,15}$";
        private readonly string passwordPattern = @"^[A-Za-z0-9]{1,15}$";



        private bool validFirstname;
        private bool validLastname;
        private bool validTelephoneNumber;
        private bool validEmail;
        private bool validUsername;
        private bool validPassword;



        public RegisterUserPage()
        {
            InitializeComponent();
            Loaded += RegisterUserPage_Loaded;
        }

        private async void RegisterUserPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await MainViewModel.CheckForInternetError();
            await ViewModel.CheckIfCompaniesExsist();
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {


            try
            {

                if (validFirstname && validLastname && validTelephoneNumber && validEmail && validUsername && validPassword)
                {

                    {
                        UserDto OneEmployee = new UserDto
                        {
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            TelephoneNumber = Convert.ToInt32(txtPhoneNumber.Text),
                            Email = txtEmail.Text,
                            Username = txtUserName.Text,
                            Password = txtPasswordBox.Password,
                            CompanyId = 1

                        };


                        string userJson = JsonConvert.SerializeObject(OneEmployee);
                        StringContent convertToStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

                        Uri employeeUri = new Uri("http://localhost:5000/Users/register");
                        if (await Data.RegisterUser(employeeUri, convertToStringContent) == true)
                        {

                            await ViewModel.LoadEmployeesAsync();

                            txtExceptionMessage.Text = "User sucessfully created.";
                        }
                        else
                            txtExceptionMessage.Text = "Failed to create the user.";
                    }
                }
                else
                    txtExceptionMessage.Text = "None of the fields can be empty";
            }
            catch (WebException ex)
            {

                txtExceptionMessage.Text = "There was an error creating the user" + ex;
            }


        }
        private void Button_Click1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MainPage));

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

        private void TxtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

            validFirstname = Regex.IsMatch(txtFirstName.Text, namingPattern);

            if (!validFirstname)
            {
                txtFirstName.Text = "";
                txtFirstName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validFirstname)

                txtFirstName.BorderBrush = new SolidColorBrush(Colors.Green);


        }

        private void TxtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            validLastname = Regex.IsMatch(txtLastName.Text, namingPattern);

            if (!validLastname)
            {
                txtLastName.Text = "";
                txtLastName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validLastname)
            {

                txtLastName.BorderBrush = new SolidColorBrush(Colors.Green);
            }


        }

        private void PhoneNumber_Changed(object sender, TextChangedEventArgs e)
        {
            validTelephoneNumber = Regex.IsMatch(txtPhoneNumber.Text, phoneNumberPattern);

            if (!validTelephoneNumber)
            {
                txtPhoneNumber.Text = "";
                txtPhoneNumber.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validTelephoneNumber)
            {
                txtPhoneNumber.BorderBrush = new SolidColorBrush(Colors.Green);

            }



        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            validEmail = Regex.IsMatch(txtEmail.Text, emailPattern);
            if (!validEmail)
            {
                txtEmail.Text = "";
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validEmail)
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }

        private void TxtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            validUsername = Regex.IsMatch(txtUserName.Text, userNamePattern);
            if (!validUsername)
            {
                txtUserName.Text = "";
                txtUserName.BorderBrush = new SolidColorBrush(Colors.Red);

            }
            else if (validUsername)
            {
                txtUserName.BorderBrush = new SolidColorBrush(Colors.Green);

            }



        }

        private void TxtPasswordBox_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            validPassword = Regex.IsMatch(txtPasswordBox.Password, passwordPattern);

            if (!validPassword)
            {
                txtPasswordBox.Password = "";
                txtPasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);

            }
            else if (validPassword)
            {
                txtPasswordBox.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
