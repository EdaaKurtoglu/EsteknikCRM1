using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class DeviceCardOperationPage : Page
    {
        private readonly UserModel _user;
        public DeviceCardOperationPage(UserModel user)
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SerialNoTextBox.Text))
                {
                    MessageBox.Show("Seri numarası zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(DeviceNameTextBox.Text))
                {
                    MessageBox.Show("Cihaz adı zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string status = "Aktive";
                if (DeviceStatusComboBox.SelectedItem is ComboBoxItem selectedStatusItem)
                {
                    status = selectedStatusItem.Content?.ToString() ?? "Aktive";
                }

                DeviceModel device = new DeviceModel
                {
                    SerialNumber = SerialNoTextBox.Text?.Trim() ?? "",
                    DeviceCode = DeviceCodeTextBox.Text?.Trim() ?? "",
                    StockCode = DeviceCodeTextBox?.Text?.Trim() ?? "",
                    DeviceName = DeviceNameTextBox.Text?.Trim() ?? "",
                    CommissionDate = CommissionDatePicker.SelectedDate,
                    Status = status
                };

                string newDeviceId = await AppServices.ApiDeviceService.AddDeviceAsync(device);

                MessageBox.Show(
                    "Cihaz başarıyla kaydedildi.\nKayıt ID: " + newDeviceId,
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

        private void DeviceCards_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DeviceCardsPage(_user));
        }

        private void DeviceCardsOperation_Click(object sender, RoutedEventArgs e)
        {
            // aktif sayfa
        }
    }
}