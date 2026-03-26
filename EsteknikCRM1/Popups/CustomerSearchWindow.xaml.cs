using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Popups
{
    public partial class CustomerSearchWindow : Window
    {
        public CustomerSearchWindow()
        {
            InitializeComponent();
        }

        public CustomerModel SelectedCustomer { get; set; }

        private List<CustomerModel> _allCustomers = new List<CustomerModel>();

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _allCustomers = await FirebaseService.Instance.GetCustomersAsync();

                ApplyFilters();

                CustomerGrid.Visibility = Visibility.Visible;
                ResultRow.Height = new GridLength(1, GridUnitType.Star);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void ApplyFilters()
        {
            if (_allCustomers == null)
                return;

            string nameFilter = NameTextBox.Text?.Trim().ToLower() ?? "";
            string surnameFilter = SurnameTextBox.Text?.Trim().ToLower() ?? "";
            string phoneFilter = PhoneTextBox.Text?.Trim().ToLower() ?? "";
            string cityFilter = CityTextBox.Text?.Trim().ToLower() ?? "";

            var filtered = _allCustomers.Where(c =>
                (string.IsNullOrEmpty(nameFilter) || (c.Name ?? "").ToLower().Contains(nameFilter)) &&
                (string.IsNullOrEmpty(surnameFilter) || (c.Surname ?? "").ToLower().Contains(surnameFilter)) &&
                (
                    string.IsNullOrEmpty(phoneFilter) ||
                    (c.Phone ?? "").ToLower().Contains(phoneFilter) ||
                    (c.MobilePhone ?? "").ToLower().Contains(phoneFilter)
                ) &&
                (string.IsNullOrEmpty(cityFilter) || (c.City ?? "").ToLower().Contains(cityFilter))
            ).ToList();

            CustomerGrid.ItemsSource = filtered;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SelectCustomer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            CustomerModel customer = btn.DataContext as CustomerModel;

            if (customer != null)
            {
                SelectedCustomer = customer;
                DialogResult = true;
                Close();
            }
        }
    }
}