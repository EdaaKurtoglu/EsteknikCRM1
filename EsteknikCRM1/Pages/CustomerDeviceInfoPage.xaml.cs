using EsteknikCRM1.DatabaseCon;
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

            var customer = await AppServices.CustomerService.GetCustomerByIdAsync(_workflow.CustomerId);
            //var customer = await FirebaseService.Instance.GetCustomerByIdAsync(_workflow.CustomerId);
            if (customer != null)
            {
                CustomerNameText.Text = (customer.Name + " " + customer.Surname).Trim();
                CustomerPhoneText.Text = customer.Phone ?? "-";
                CustomerEmailText.Text = string.IsNullOrWhiteSpace(customer.Email) ? "-" : customer.Email;
            }

            if (!string.IsNullOrWhiteSpace(_workflow.AddressId))
            {
                var address = await AppServices.AddressService.GetAddressByIdAsync(_workflow.AddressId);
                //var address = await FirebaseService.Instance.GetAddressByIdAsync(_workflow.AddressId);
                if (address != null)
                {
                    CustomerAddressText.Text = address.AddressLine ?? "-";
                }
            }
        }

        private async Task LoadCustomerDevicesAsync()
        {
            if (string.IsNullOrWhiteSpace(_workflow.CustomerId))
            {
                CustomerDeviceGrid.ItemsSource = new List<CustomerDeviceGridItem>();
                return;
            }
            var devices = await AppServices.CustomerDeviceService.GetDevicesByCustomerIdAsync(_workflow.CustomerId);
            //var devices = await FirebaseService.Instance.GetDevicesByCustomerIdAsync(_workflow.CustomerId);
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