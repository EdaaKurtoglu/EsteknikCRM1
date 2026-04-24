using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Services.Api;

namespace EsteknikCRM1.Pages
{
    public partial class CustomerAddPage : Page
    {
        UserModel _user;
        public CustomerAddPage(UserModel user)
        {
            InitializeComponent();
            _user = user;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NameBox.Text))
                {
                    MessageBox.Show("Ad alanı zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(SurnameBox.Text))
                {
                    MessageBox.Show("Soyad alanı zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CustomerModel customer = new CustomerModel
                {
                    IsVip = VipCheckBox.IsChecked == true,

                    CustomerNo = CustomerIdBox.Text?.Trim(),
                    Name = NameBox.Text?.Trim(),
                    MiddleName = MiddleNameBox.Text?.Trim(),
                    Surname = SurnameBox.Text?.Trim(),

                    Phone = PhoneBox.Text?.Trim(),
                    MobilePhone = MobilePhoneBox.Text?.Trim(),
                    Phone2 = Phone2Box.Text?.Trim(),
                    MobilePhone2 = MobilePhone2Box.Text?.Trim(),

                    UnknownEmail = UnknownEmailCheckBox.IsChecked == true,
                    Email = EmailBox.Text?.Trim(),
                    Email2 = Email2Box.Text?.Trim(),

                    Description = DescriptionBox.Text?.Trim(),

                    Country = CountryBox.Text?.Trim(),
                    City = CityBox.Text?.Trim(),
                    District = DistrictBox.Text?.Trim(),
                    Neighborhood = NeighborhoodBox.Text?.Trim(),
                    Address = AddressBox.Text?.Trim(),

                    SpecialProjectInfo = SpecialProjectBox.Text?.Trim(),
                    BuildingInfo = BuildingInfoBox.Text?.Trim(),

                    Status = "Aktif"
                };

                string newCustomerId = await AppServices.ApiCustomerService.AddCustomerAsync(customer);

                string fullAddress = string.Format("{0} {1} {2} {3} {4}",
                    AddressBox.Text?.Trim(),
                    NeighborhoodBox.Text?.Trim(),
                    DistrictBox.Text?.Trim(),
                    CityBox.Text?.Trim(),
                    CountryBox.Text?.Trim()).Trim();

                if (!string.IsNullOrWhiteSpace(fullAddress))
                {
                    AddressModel address = new AddressModel
                    {
                        CustomerId = newCustomerId,
                        AddressLine = fullAddress,
                        Country = CountryBox.Text?.Trim(),
                        City = CityBox.Text?.Trim(),
                        District = DistrictBox.Text?.Trim(),
                        Neighborhood = NeighborhoodBox.Text?.Trim(),
                        Status = "Aktif",
                        Id = Guid.NewGuid().ToString(), // 🔥 BURASI ÇÖZÜM
                        CreatedDate = DateTime.Now.ToUniversalTime(),

                    };

                    await AppServices.ApiAddressService.AddAddressAsync(address);
                }

                MessageBox.Show(
                    "Müşteri başarıyla kaydedildi.\nKayıt ID: " + newCustomerId,
                    "Başarılı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kayıt sırasında hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(_user));
        }

        private void Cards_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new CardsPage());
        }

        private void IndividualCustomers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new IndividualCustomerCardsPage(_user));
        }

        private void IndividualCustomersOperation_Click(object sender, RoutedEventArgs e)
        {
            // bulunduğun sayfa → boş bırak
        }
    }
}