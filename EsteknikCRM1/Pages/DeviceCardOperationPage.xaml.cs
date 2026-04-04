using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class DeviceCardOperationPage : Page
    {
        public DeviceCardOperationPage()
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

                string status = "Aktif";
                if (DeviceStatusComboBox.SelectedItem is ComboBoxItem selectedStatusItem)
                {
                    status = selectedStatusItem.Content?.ToString() ?? "Aktif";
                }

                DeviceModel device = new DeviceModel
                {
                    SerialNumber = SerialNoTextBox.Text?.Trim(),
                    DeviceCode = DeviceCodeTextBox.Text?.Trim(),
                    DeviceName = DeviceNameTextBox.Text?.Trim(),
                    CommissionDate = CommissionDatePicker.SelectedDate,
                    Status = status
                };

                string newDeviceId = await FirebaseService.Instance.AddDeviceAsync(device);

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
    }
}