using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class IndividualCustomerCardsPage : Page
    {
        private List<CustomerModel> _allCustomers = new List<CustomerModel>();

        public IndividualCustomerCardsPage()
        {
            InitializeComponent();
            Loaded += IndividualCustomerCardsPage_Loaded;
        }

        private async void IndividualCustomerCardsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCustomersAsync();
        }

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            try
            {
                _allCustomers = await FirebaseService.Instance.GetCustomersAsync();
                CustomerGrid.ItemsSource = _allCustomers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteriler yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (_allCustomers == null)
                return;

            string searchText = SearchTextBox.Text?.Trim().ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(searchText))
            {
                CustomerGrid.ItemsSource = _allCustomers;
                return;
            }

            var filtered = _allCustomers.Where(c =>
                (c.FullName ?? "").ToLower().Contains(searchText) ||
                (c.MobilePhone ?? "").ToLower().Contains(searchText) ||
                (c.Phone ?? "").ToLower().Contains(searchText) ||
                (c.Email ?? "").ToLower().Contains(searchText) ||
                (c.Address ?? "").ToLower().Contains(searchText) ||
                (c.Status ?? "").ToLower().Contains(searchText)
            ).ToList();

            CustomerGrid.ItemsSource = filtered;
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CustomerAddPage());
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CustomerModel selectedCustomer = button?.DataContext as CustomerModel;

            if (selectedCustomer == null)
                return;

            MessageBox.Show(
                $"Seçilen müşteri: {selectedCustomer.FullName}",
                "Bilgi",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}