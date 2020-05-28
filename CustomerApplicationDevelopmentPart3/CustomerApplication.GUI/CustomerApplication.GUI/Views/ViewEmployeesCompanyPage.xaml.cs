using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CustomerApplication.GUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewEmployeesCompanyPage : Page
    {


        /// <summary>Gets the view model.</summary>
        /// <value>The view model.</value>
        public CompanyViewModel ViewModel { get; } = new CompanyViewModel();
        /// <summary>Gets the main view model.</summary>
        /// <value>The main view model.</value>
        public MainViewModel MainViewModel { get; } = new MainViewModel();

        public ViewEmployeesCompanyPage()
        {
            this.InitializeComponent();
            Loaded += ViewEmployeesCompanyPage_Loaded;
        }

        /// <summary>Handles the Loaded event of the ViewEmployeesCompanyPage control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void ViewEmployeesCompanyPage_Loaded(object sender, RoutedEventArgs e)
        {
            await MainViewModel.CheckForInternetError();
            await ViewModel.LoadEmployeesCompaniesAsync();
        }

        /// <summary>Handles the RemoveUser event of the Button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void Button_RemoveUser(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var testEmployee = Convert.ToInt32(ViewModel.ReadCurrentObject("currentObject"));
            Uri uri = new Uri("http://localhost:5000/Users/" + testEmployee);
            Employee user = await Data.GetUserAsync<Employee>(uri);
            user.CompanyId = 1;
            var userJson = JsonConvert.SerializeObject(user);
            StringContent convertToStringContent1 = new StringContent(userJson, Encoding.UTF8, "application/json");
            ViewModel.SaveCurrentObject("currentObject", userJson);
            await Data.UpdateUser(uri, convertToStringContent1);
            await ViewModel.LoadEmployeesCompaniesAsync();
        }

        /// <summary>Handles the CompanyList event of the Employees control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void Employees_CompanyList(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                int index = gvEmployeesCompany.SelectedIndex;
                ViewModel.SaveCurrentObject("currentObject", ViewModel.EmployeeCompanies[index].Id.ToString());
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }
    }
}
