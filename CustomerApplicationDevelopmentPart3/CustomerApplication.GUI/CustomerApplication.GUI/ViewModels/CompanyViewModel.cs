using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.Helpers;

namespace CustomerApplication.GUI.ViewModels
{
    public class CompanyViewModel : Observable
    {

        /// <summary>Gets the companies.</summary>
        /// <value>The companies.</value>
        public ObservableCollection<Company> Companies { get; } = new ObservableCollection<Company>();
        /// <summary>Gets the employee companies.</summary>
        /// <value>The employee companies.</value>
        public ObservableCollection<Employee> EmployeeCompanies { get; } = new ObservableCollection<Employee>();
        /// <summary>Gets the inventory companies.</summary>
        /// <value>The inventory companies.</value>
        public ObservableCollection<Inventory> InventoryCompanies { get; } = new ObservableCollection<Inventory>();

        public CompanyViewModel()
        {
           
        }
        /// <summary>Loads the companies asynchronous.</summary>
        internal async Task LoadCompaniesAsync()
        {
            Companies.Clear();
            IList<Company> listCompanies = await Data.GetCompaniesAsync();
            foreach (Company comp in listCompanies)
                Companies.Add(comp);
        }

        /// <summary>Loads the employees companies asynchronous.</summary>
        internal async Task LoadEmployeesCompaniesAsync()
        {
            EmployeeCompanies.Clear();
            var testUser = System.Text.Json.JsonSerializer.Deserialize<Employee>(ReadCurrentObject("currentObject"));
            IList<Employee> listCompanies = await Data.GetEmployeesCompaniesAsync<Employee>(testUser.CompanyId);
            foreach (Employee comp in listCompanies)
                EmployeeCompanies.Add(comp);
        }

        /// <summary>Loads the inventory companies asynchronous.</summary>
        internal async Task LoadInventoryCompaniesAsync()
        {
            InventoryCompanies.Clear();
            var testCompany = Convert.ToInt32(ReadCurrentObject("currentCompany"));
            IList<Inventory> listInventories = await Data.GetInventoriesCompaniesAsync<Inventory>(testCompany);
            foreach (Inventory comp in listInventories)
                InventoryCompanies.Add(comp);
        }

        /// <summary>Deletes the inventory asynchronous.</summary>
        internal async Task DeleteInventoryAsync()

        {
            var testCompany = Convert.ToInt32(ReadCurrentObject("currentInventory"));
            Uri uri = new Uri("http://localhost:5000/api/Inventories/" + testCompany);
            await Data.DeleteInventory(uri);

        }


        /// <summary>Reads the current object.</summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
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

        /// <summary>Saves the current object.</summary>
        /// <param name="objectName">Name of the object.</param>
        /// <param name="objectValue">The object value.</param>
        public void SaveCurrentObject(string objectName, string objectValue)
        {

            Windows.Storage.ApplicationDataContainer currentObject = Windows.Storage.ApplicationData.Current.LocalSettings;
            currentObject.Values[objectName] = objectValue;

        }


    }
}
