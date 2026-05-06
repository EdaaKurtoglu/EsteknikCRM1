using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Popups
{
    public partial class CustomerAddressAddPage : Window
    {
        private readonly CustomerModel _customer;

        public CustomerAddressAddPage(CustomerModel customer)
        {
            InitializeComponent();
            _customer = customer;
            CreatedDatePicker.SelectedDate = DateTime.Now;
            UpdateFullAddressText();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SmartAddressSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // İstersen burada akıllı adres parse işlemi yapabiliriz.
        }

        private void SearchSmartAddress_Click(object sender, RoutedEventArgs e)
        {
            // Gerçek harita / geocode entegrasyonu yok.
            // Şimdilik akıllı arama kutusundaki değeri ana adrese basıyoruz.
            if (!string.IsNullOrWhiteSpace(SmartAddressSearchBox.Text))
            {
                AddressBox.Text = SmartAddressSearchBox.Text.Trim();
                UpdateFullAddressText();
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_customer == null)
                {
                    MessageBox.Show("Müşteri bilgisi bulunamadı.");
                    return;
                }

                string ownershipType = "";
                if (OwnerRadio.IsChecked == true)
                    ownershipType = "Ev Sahibi";
                else if (TenantRadio.IsChecked == true)
                    ownershipType = "Kiracı";
                else if (OtherRadio.IsChecked == true)
                    ownershipType = "Diğer";

                string fullAddress = string.Join(" ",
                    new[]
                    {
                        StreetBox.Text?.Trim(),
                        BuildingNoBox.Text?.Trim(),
                        FlatNoBox.Text?.Trim(),
                        NeighborhoodBox.Text?.Trim(),
                        DistrictBox.Text?.Trim(),
                        CityBox.Text?.Trim(),
                        CountryBox.Text?.Trim(),
                        PostCodeBox.Text?.Trim()
                    }.Where(x => !string.IsNullOrWhiteSpace(x)));

                if (string.IsNullOrWhiteSpace(fullAddress) && string.IsNullOrWhiteSpace(AddressBox.Text))
                {
                    MessageBox.Show("Adres bilgisi giriniz.");
                    return;
                }

                AddressModel address = new AddressModel
                {
                    CustomerId = _customer.Id,
                    AddressLine = string.IsNullOrWhiteSpace(AddressBox.Text) ? fullAddress : AddressBox.Text.Trim(),

                    Country = CountryBox.Text?.Trim(),
                    City = CityBox.Text?.Trim(),
                    District = DistrictBox.Text?.Trim(),
                    Neighborhood = NeighborhoodBox.Text?.Trim(),
                    Street = StreetBox.Text?.Trim(),
                    PostCode = PostCodeBox.Text?.Trim(),
                    BuildingNo = BuildingNoBox.Text?.Trim(),
                    FlatNo = FlatNoBox.Text?.Trim(),

                    IsActive = IsActiveCheckBox.IsChecked == true,
                    IsResidence = IsResidenceCheckBox.IsChecked == true,
                    OwnershipType = ownershipType,
                    CreatedDate = DateTime.UtcNow,
                    PassiveDate = null,
                    Status = IsActiveCheckBox.IsChecked == true ? "active" : "pasive"
                };

                //await FirebaseService.Instance.AddCustomerAddressAsync(address);
                await AppServices.AddressService.AddCustomerAddressAsync(address);

                MessageBox.Show("Adres başarıyla kaydedildi.");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adres kaydedilirken hata oluştu: " + ex.Message);
            }
        }

        private void UpdateFullAddressText()
        {
            AddressBox.Text = string.Format(
                "{0}, Apartman:{1}, Daire:{2}",
                string.IsNullOrWhiteSpace(StreetBox.Text) ? "-" : StreetBox.Text,
                string.IsNullOrWhiteSpace(BuildingNoBox.Text) ? "-" : BuildingNoBox.Text,
                string.IsNullOrWhiteSpace(FlatNoBox.Text) ? "-" : FlatNoBox.Text
            );
        }
    }
}