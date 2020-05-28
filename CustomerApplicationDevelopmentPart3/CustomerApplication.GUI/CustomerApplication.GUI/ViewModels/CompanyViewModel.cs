using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.Helpers;

namespace CustomerApplication.GUI.ViewModels
{
    public class CompanyViewModel : Observable
    {

        public ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();
        public ObservableCollection<Employee> EmployeeCompanies { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Inventory> InventoryCompanies { get; set; } = new ObservableCollection<Inventory>();

        public CompanyViewModel()
        {
           
        }
        internal async Task LoadCompaniesAsync()
        {
            Companies.Clear();
            IList<Company> listCompanies = await Data.GetCompaniesAsync();
            foreach (Company comp in listCompanies)
                Companies.Add(comp);
        }

        internal async Task LoadEmployeesCompaniesAsync()
        {
            EmployeeCompanies.Clear();
            var testUser = System.Text.Json.JsonSerializer.Deserialize<Employee>(ReadCurrentObject("currentObject"));
            IList<Employee> listCompanies = await Data.GetEmployeesCompaniesAsync<Employee>(testUser.CompanyId);
            foreach (Employee comp in listCompanies)
                EmployeeCompanies.Add(comp);
        }

        internal async Task LoadInventoryCompaniesAsync()
        {
            InventoryCompanies.Clear();
            var testCompany = Convert.ToInt32(ReadCurrentObject("currentCompany"));
            IList<Inventory> listInventories = await Data.GetInventoriesCompaniesAsync<Inventory>(testCompany);
            foreach (Inventory comp in listInventories)
                InventoryCompanies.Add(comp);
        }

        internal async Task DeleteInventoryAsync()

        {
            var testCompany = Convert.ToInt32(ReadCurrentObject("currentInventory"));
            Uri uri = new Uri("http://localhost:5000/api/Inventories/" + testCompany);
            await Data.DeleteInventory(uri);

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
            catch (Exception) { return "Object dosen't exsist"; }

        }

        public void SaveCurrentObject(string objectName, string objectValue)
        {

            Windows.Storage.ApplicationDataContainer currentObject = Windows.Storage.ApplicationData.Current.LocalSettings;
            currentObject.Values[objectName] = objectValue;

        }


    }
}
