using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerApplication.GUI.Core.Datahandler;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.Helpers;
using Newtonsoft.Json;

namespace CustomerApplication.GUI.ViewModels
{
    public class RegisterUserViewModel : Observable
    {
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();
        public CompanyViewModel CompanyViewModel { get; } = new CompanyViewModel();

        public RegisterUserViewModel()
        {
        }

        internal async Task LoadEmployeesAsync()
        {
            Employees.Clear();
            IList<Employee> listEmployees = await Data.GetEmployeesAsync();
            foreach (Employee user in listEmployees)
                Employees.Add(user);
        }

        internal async Task CheckIfCompaniesExsist()
        {
            Uri countUri = new Uri("http://localhost:5000/api/Companies/GetCompanyCount");
            var count = (await Data.GetCount(countUri));

            if (count.Equals(0) || count.Equals(null))
            {

                Company OneCompany = new Company
                {
                    CompanyName = "ExamTestCompany",
                    Description = "CompanyHolder for users"
                };


                string userJson = JsonConvert.SerializeObject(OneCompany);
                StringContent convertToStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

                Uri companyUri = new Uri("http://localhost:5000/api/Companies");
                await Data.RegisterUser(companyUri, convertToStringContent);
                await CompanyViewModel.LoadCompaniesAsync();

            }
            

        }
    }
}
