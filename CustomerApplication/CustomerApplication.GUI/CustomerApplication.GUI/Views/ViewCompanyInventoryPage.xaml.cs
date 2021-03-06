﻿using CustomerApplication.GUI.Core.Datahandler;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewCompanyInventoryPage : Page
    {
        /// <summary>The naming pattern</summary>
        private readonly string namingPattern = @"^[/a-zA-Z]+${1,30}";

        /// <summary>The valid inventory name</summary>
        private bool validInventoryName;
        /// <summary>The valid description name</summary>
        private bool validDescriptionName;


        /// <summary>Gets the view model.</summary>
        /// <value>The view model.</value>
        public CompanyViewModel ViewModel { get; } = new CompanyViewModel();
        /// <summary>Gets the main view model.</summary>
        /// <value>The main view model.</value>
        public MainViewModel MainViewModel { get; } = new MainViewModel();



        public ViewCompanyInventoryPage()
        {
            this.InitializeComponent();
            Loaded += ViewCompanyInventoryPage_Loaded;
        }

        /// <summary>Handles the Loaded event of the ViewCompanyInventoryPage control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void ViewCompanyInventoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            await MainViewModel.CheckForInternetError();
            await ViewModel.LoadInventoryCompaniesAsync();
        }

        /// <summary>Handles the AddInventory event of the Button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void Button_AddInventory(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (validInventoryName && validDescriptionName && DatePicker.SelectedDate != null)
            {
                
                Inventory OneInventory = new Inventory
                {
                    ItemName = txtItemName.Text,
                    Description = txtItemDescription.Text,
                    Quantity = Convert.ToInt32(volumeSlider.Value),
                    AddDate = DatePicker.SelectedDate.Value.DateTime,
                    CompanyId = Convert.ToInt32(ViewModel.ReadCurrentObject("currentCompany"))

                };
                

                string userJson = JsonConvert.SerializeObject(OneInventory);
                StringContent convertToStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

                Uri inventoryUri = new Uri("http://localhost:5000/api/Inventories");
                await Data.RegisterUser(inventoryUri, convertToStringContent);
                await ViewModel.LoadInventoryCompaniesAsync();
                txtExceptionMessage.Text = "Item successfully added in Company inventory.";
            }
            else
                txtExceptionMessage.Text = "None of the fields can be empty.";
        }

        /// <summary>Handles the DeleteInventory event of the Button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void Button_DeleteInventory(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.DeleteInventoryAsync();
            await ViewModel.LoadInventoryCompaniesAsync();
            txtExceptionMessage.Text = "Item successfully deleted from Company inventory.";
        }

        /// <summary>Handles the Changed event of the InventoryChanged control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void InventoryChanged_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = gvInventories.SelectedIndex;
                ViewModel.SaveCurrentObject("currentInventory", ViewModel.InventoryCompanies[index].Id.ToString());

            }
            catch (ArgumentOutOfRangeException)
            {

            }

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

        /// <summary>Handles the TextChanged event of the TxtItemName control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
        private void TxtItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            validInventoryName = Regex.IsMatch(txtItemName.Text, namingPattern);
            if (!validInventoryName)
            {

                txtItemName.Text = "";
                txtItemName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (validInventoryName) {
                txtItemName.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }

        /// <summary>Handles the TextChanged event of the TxtItemDescription control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
        private void TxtItemDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            validDescriptionName = Regex.IsMatch(txtItemDescription.Text, namingPattern);
            if (!validDescriptionName) {
                txtItemDescription.Text = "";
                txtItemDescription.BorderBrush = new SolidColorBrush(Colors.Green);
            }


        }
    }
}
