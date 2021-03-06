﻿using System;
using System.Text.Json;
using CustomerApplication.GUI.Core.Models;
using CustomerApplication.GUI.Helpers;

namespace CustomerApplication.GUI.ViewModels
{
    public class LoggedInViewModel : Observable
    {



        public LoggedInViewModel()
        {

        }


        /// <summary>Gets the current employee.</summary>
        /// <returns></returns>
        public Employee GetCurrentEmployee()
        {
            try
            {
                var current = JsonSerializer.Deserialize<Employee>(ReadCurrentObject("currentObject"));

                return current;
            }
            catch (Exception)
            {
                return default;
            }
        }



        /// <summary>Clears the current employee.</summary>
        public void ClearCurrentEmployee()
        {

            Windows.Storage.ApplicationDataContainer currentObject = Windows.Storage.ApplicationData.Current.LocalSettings;
            currentObject.Values.Clear();


        }



        /// <summary>Reads the current object.</summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        public string ReadCurrentObject(string objectName)
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

    }
}
