using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class CustomerDeviceInfoPage : Page
    {
        private readonly WorkflowModel _workflow;

        public CustomerDeviceInfoPage(WorkflowModel workflow)
        {
            InitializeComponent();
            _workflow = workflow;
            Loaded += CustomerDeviceInfoPage_Loaded;
        }

        private async void CustomerDeviceInfoPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCustomerAndDevicesAsync();
        }

        private async Task LoadCustomerAndDevicesAsync()
        {
            try
            {
                if (_workflow == null)
                    return;

                await LoadCustomerInfoAsync();
                await LoadCustomerDevicesAsync();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private async Task LoadCustomerInfoAsync()
        {
            if (string.IsNullOrWhiteSpace(_workflow.CustomerId))
                return;

            var customer = await AppServices.ApiCustomerService.GetCustomerByIdAsync(_workflow.CustomerId);

            if (customer != null)
            {
                CustomerNameText.Text = ((customer.Name ?? "") + " " + (customer.Surname ?? "")).Trim();
                CustomerPhoneText.Text = string.IsNullOrWhiteSpace(customer.Phone) ? "-" : customer.Phone;
                CustomerEmailText.Text = string.IsNullOrWhiteSpace(customer.Email) ? "-" : customer.Email;
            }

            if (!string.IsNullOrWhiteSpace(_workflow.AddressId))
            {
                var address = await AppServices.ApiAddressService.GetAddressByIdAsync(_workflow.AddressId);

                if (address != null)
                {
                    CustomerAddressText.Text = string.IsNullOrWhiteSpace(address.AddressLine) ? "-" : address.AddressLine;
                }
                else
                {
                    CustomerAddressText.Text = "-";
                }
            }
            else
            {
                CustomerAddressText.Text = "-";
            }
        }

        private async Task LoadCustomerDevicesAsync()
        {
            if (string.IsNullOrWhiteSpace(_workflow.CustomerId))
            {
                CustomerDeviceGrid.ItemsSource = new List<CustomerDeviceGridItem>();
                return;
            }

            var devices = await AppServices.ApiCustomerDeviceService.GetDevicesByCustomerIdAsync(_workflow.CustomerId);
            var rows = new List<CustomerDeviceGridItem>();

            foreach (var device in devices)
            {
                rows.Add(new CustomerDeviceGridItem
                {
                    Id = device.Id,
                    DeviceId = device.Id,
                    SerialNo = device.SerialNumber,
                    ProductName = device.DeviceName,
                    ProductCode = device.DeviceCode,
                    CommissionDate = device.CommissionDate.HasValue
                        ? device.CommissionDate.Value.ToString("dd/MM/yyyy")
                        : "-",
                    Status = string.IsNullOrWhiteSpace(device.Status) ? "Aktif" : device.Status
                });
            }

            CustomerDeviceGrid.ItemsSource = rows;
        }

        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Müşteri bilgileri güncellenecek.");
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CustomerDeviceGridItem selectedDevice = button?.DataContext as CustomerDeviceGridItem;

            if (selectedDevice == null)
            {
                MessageBox.Show("Cihaz bulunamadı.");
                return;
            }

            MessageBox.Show("Seçilen cihaz: " + selectedDevice.ProductName);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Güncelle işlemi çalışacak.");
        }

        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bu müşteriye cihaz ekleme sayfası açılacak.");
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new HomeContentPage(currentUser));
        }

        private void Workflows_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new WorkflowPage());
        }

        private void WorkflowOperation_Click(object sender, RoutedEventArgs e)
        {
            // Şu an bulunduğun sayfa
        }
    }

    public class CustomerDeviceGridItem
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string SerialNo { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string CommissionDate { get; set; }
        public string Status { get; set; }
    }
}