using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CustomerApplication.GUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewEmployeesCompanyPage : Page
    {

       
        public CompanyViewModel ViewModel { get; } = new CompanyViewModel();
        public MainViewModel MainViewModel { get; } = new MainViewModel();

        public ViewEmployeesCompanyPage()
        {
            this.InitializeComponent();
            Loaded += ViewEmployeesCompanyPage_Loaded;
        }

        private async void ViewEmployeesCompanyPage_Loaded(object sender, RoutedEventArgs e)
        {
            await MainViewModel.CheckForInternetError();
            await ViewModel.LoadEmployeesCompaniesAsync();
        }

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
