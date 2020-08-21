using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CustomerApplication.GUI.Views
{
    /// <summary>An empty page that can be used on its own or navigated to within a Frame.</summary>
    public sealed partial class JoinACompanyPage : Page
    {
        /// <summary>The naming pattern</summary>
        private readonly string namingPattern = @"^[/a-zA-Z]+${1,30}";

        /// <summary>The valid company name</summary>
        private bool validCompanyName;
        /// <summary>The valid description</summary>
        private bool validDescription;

        /// <summary>Gets the view model.</summary>
        /// <value>The view model.</value>
        public CompanyViewModel ViewModel { get; } = new CompanyViewModel();
        /// <summary>Gets the main view model.</summary>
        /// <value>The main view model.</value>
        public MainViewModel MainViewModel { get; } = new MainViewModel();

        /// <summary>Gets or sets the selected identifier for update.</summary>
        /// <value>The selected identifier for update.</value>
        private static int _selectedIdForUpdate { get; set; }

        public JoinACompanyPage()
        {
            this.InitializeComponent();
            Loaded += JoinACompanyPage_Loaded;
        }

        /// <summary>Handles the Loaded event of the JoinACompanyPage control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void JoinACompanyPage_Loaded(object sender, RoutedEventArgs e)
        {
            await MainViewModel.CheckForInternetError();
            await ViewModel.LoadCompaniesAsync();
        }

        /// <summary>A button that creates the company.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void BtnCompany(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (validCompanyName && validDescription)
            {

                Company OneCompany = new Company
                {
                    CompanyName = txtCompanyName.Text,
                    Description = txtCompanyDescription.Text
                };


                string userJson = JsonConvert.SerializeObject(OneCompany);
                StringContent convertToStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

                Uri companyUri = new Uri("http://localhost:5000/api/Companies");
                await Data.RegisterUser(companyUri, convertToStringContent);
                await ViewModel.LoadCompaniesAsync();
                txtExceptionMessage.Text = "Company successfully created.";
            }
            else
                txtExceptionMessage.Text = "None of the fields can be empty.";
        }

        /// <summary>Handles the Changed event of the CompaniesList control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void CompaniesList_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = gvCompanies.SelectedIndex;
                _selectedIdForUpdate = index + 1;
                ViewModel.SaveCurrentObject("currentCompany", ViewModel.Companies[index].Id.ToString());

            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }

        /// <summary>A button that lets you join a company.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void BtnJoinCompany(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {

                var testUser = System.Text.Json.JsonSerializer.Deserialize<Employee>(ViewModel.ReadCurrentObject("currentObject"));


            Employee employee = new Employee()
            {
                Id = testUser.Id,
                FirstName = testUser.FirstName,
                LastName = testUser.LastName,
                Email = testUser.Email,
                TelephoneNumber = testUser.TelephoneNumber,
                Username = testUser.Username,
                CompanyId = Convert.ToInt32(ViewModel.ReadCurrentObject("currentCompany"))
            };

            string userJson1 = JsonConvert.SerializeObject(employee);

            StringContent convertToStringContent1 = new StringContent(userJson1, Encoding.UTF8, "application/json");
            Uri companyUri1 = new Uri("http://localhost:5000/Users/" + testUser.Id);

                await Data.UpdateUser(companyUri1, convertToStringContent1);

            Uri companyUri2 = new Uri("http://localhost:5000/Users/" + testUser.Id);
            Employee user = await Data.GetUserAsync<Employee>(companyUri2);
            var userJson = JsonConvert.SerializeObject(user);
            ViewModel.SaveCurrentObject("currentObject", userJson);
            txtExceptionMessage.Text = "Company successfully joined.";
        }
            catch (JsonException) { Frame.Navigate(typeof(MainPage)); }
        }





        /// <summary>A button that update company.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void BtnUpdateCompany(object sender, Windows.UI.Xaml.RoutedEventArgs e)
            {
                if (validCompanyName && validDescription)
                {

                    Uri companyUri1 = new Uri("http://localhost:5000/api/Companies/" + _selectedIdForUpdate);
                    Company user = await Data.GetUserAsync<Company>(companyUri1);
                    user.CompanyName = txtCompanyName.Text;
                    user.Description = txtCompanyDescription.Text;
                    var userJson = JsonConvert.SerializeObject(user);
                    StringContent convertToStringContent1 = new StringContent(userJson, Encoding.UTF8, "application/json");
                    await Data.UpdateUser(companyUri1, convertToStringContent1);
                    await ViewModel.LoadCompaniesAsync();
                    txtExceptionMessage.Text = "Company successfully updated.";

                }
                else
                    txtExceptionMessage.Text = "None of the fields can be empty.";

            }

            private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }

            private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            }

            private void Button_PointerEntered_1(object sender, PointerRoutedEventArgs e)
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }

            private void Button_PointerExited_1(object sender, PointerRoutedEventArgs e)
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            }

            private void Button_PointerEntered_2(object sender, PointerRoutedEventArgs e)
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }

            private void Button_PointerExited_2(object sender, PointerRoutedEventArgs e)
            {
                Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            }

        /// <summary>Handles the TextChanged event of the TxtCompanyName control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
        private void TxtCompanyName_TextChanged(object sender, TextChangedEventArgs e)
            {
                validCompanyName = Regex.IsMatch(txtCompanyName.Text, namingPattern);

                if (!validCompanyName)
                {
                    txtCompanyName.Text = "";
                    txtCompanyName.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else if (validCompanyName)

                    txtCompanyName.BorderBrush = new SolidColorBrush(Colors.Green);
            }


        /// <summary>Handles the TextChanged event of the TxtCompanyDescription control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
        private void TxtCompanyDescription_TextChanged(object sender, TextChangedEventArgs e)
            {
                validDescription = Regex.IsMatch(txtCompanyDescription.Text, namingPattern);

                if (!validDescription)
                {
                    txtCompanyDescription.Text = "";
                    txtCompanyDescription.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else if (validDescription)

                    txtCompanyDescription.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }
    }

