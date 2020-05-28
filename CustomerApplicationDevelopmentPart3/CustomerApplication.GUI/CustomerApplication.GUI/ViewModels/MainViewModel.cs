using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.DataTransferObject;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.Helpers;
using Newtonsoft.Json;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace CustomerApplication.GUI.ViewModels
{
    public class MainViewModel : Observable
    {
        public LoggedInViewModel ViewModel { get; } = new LoggedInViewModel();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public MainViewModel()
        {
        }

        internal async Task LoadEmployeesAsync()
        {
            Employees.Clear();
            IList<Employee> listEmployees = await Data.GetEmployeesAsync();
            foreach (Employee user in listEmployees)
                Employees.Add(user);
                
        }


        internal async Task <bool> LoginUserAsync(string userName, string password)
        {
                var OneEmployee = new UserDto()
                {

                    Username = userName,
                    Password = password
                };
                string userJson = JsonConvert.SerializeObject(OneEmployee);
                var convertToStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

                Uri employeeUri = new Uri("http://localhost:5000/Users/authenticate");

           
                Employee currentUser = await Data.LoginUser<Employee>(employeeUri, convertToStringContent);

                var currentUserJson = JsonConvert.SerializeObject(currentUser);


                if (currentUser.Username == userName)
                {
                    
                  //await LoadEmployeesAsync();
                  SaveCurrentObject("currentObject", currentUserJson);
                  return true;  
                }
            return false;
               
        }

        public static void SaveCurrentObject(string objectName, string objectValue) {

            Windows.Storage.ApplicationDataContainer currentObject = Windows.Storage.ApplicationData.Current.LocalSettings;
            currentObject.Values[objectName] = objectValue;

        }

        public string ReadCurrentObject(string objectName)
        {

            try
            {
                Windows.Storage.ApplicationDataContainer currentObject = Windows.Storage.ApplicationData.Current.LocalSettings;
                var userValue = currentObject.Values[objectName];
                var currentUser = Convert.ToString(userValue);

                if (userValue != null)
                {
                    return currentUser;
                }

                else
                    return "Object dosen't exsist";

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return default;
            }
        }
        public async Task CheckForInternetError()
        {
            if (NetworkInformation.GetInternetConnectionProfile() == null)
            {
                await new MessageDialog("Features may be unavailable, please reconnect your Internet", "No Internet Connection").ShowAsync();
            }

        }

    }
}
