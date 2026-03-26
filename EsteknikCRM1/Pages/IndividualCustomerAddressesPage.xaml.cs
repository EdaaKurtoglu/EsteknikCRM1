using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class IndividualCustomerAddressesPage : Page
    {
        private readonly CustomerModel _selectedCustomer;

        public IndividualCustomerAddressesPage(CustomerModel customer)
        {
            InitializeComponent();
            _selectedCustomer = customer;
            Loaded += IndividualCustomerAddressesPage_Loaded;
        }

        private async void IndividualCustomerAddressesPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Müşteri bilgisi bulunamadı.");
                    return;
                }

                var addresses = await FirebaseService.Instance.GetCustomerAddressesAsync(_selectedCustomer.Id);

                var gridData = addresses.Select(x => new AddressModel
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    AddressLine = x.AddressLine,
                    FlatNo = "",
                    BuildingNo = "",
                    Street = "",
                    Neighborhood = x.Neighborhood,
                    District = x.District,
                    City = x.City,
                    Country = x.Country,
                    IsResidence = false,
                    Status = "Aktif",
                    OwnershipType = ""
                }).ToList();

                AddressGrid.ItemsSource = gridData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adresler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private async void AddNewAddress_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("Müşteri seçilmedi.");
                return;
            }

            var popup = new EsteknikCRM1.Popups.CustomerAddressAddPage(_selectedCustomer)
            {
                Owner = Window.GetWindow(this)
            };

            bool? result = popup.ShowDialog();

            if (result == true)
            {
                var addresses = await FirebaseService.Instance.GetCustomerAddressesAsync(_selectedCustomer.Id);

                var gridData = addresses.Select(x => new AddressModel
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    AddressLine = x.AddressLine,
                    FlatNo = x.FlatNo,
                    BuildingNo = x.BuildingNo,
                    Street = x.Street,
                    Neighborhood = x.Neighborhood,
                    District = x.District,
                    City = x.City,
                    Country = x.Country,
                    IsResidence = x.IsResidence,
                    Status = x.Status,
                    OwnershipType = x.OwnershipType
                }).ToList();

                AddressGrid.ItemsSource = gridData;
            }
        }

        private void DeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Silme işlemi daha sonra bağlanacak.");
        }

        private void OperationAddress_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Adres detay/işlem sayfası daha sonra açılacak.");
        }
    }
}