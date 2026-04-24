using EsteknikCRM1.Models;
using EsteknikCRM1.Models.EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class WorkflowTeamOperationPage : Page
    {
        private readonly WorkflowModel _workflow;
        private readonly UserModel _loggedUser;
        private DeviceModel _selectedDevice;

        private ObservableCollection<WorkflowTeamOperationItem> _items =
            new ObservableCollection<WorkflowTeamOperationItem>();

        public WorkflowTeamOperationPage(WorkflowModel workflow, UserModel loggedUser)
        {
            InitializeComponent();
            _workflow = workflow;
            _loggedUser = loggedUser;

            OperationItemsGrid.ItemsSource = _items;

            Loaded += Page_Loaded;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadLaborOperationsAsync();
        }

        private async Task LoadLaborOperationsAsync()
        {
            try
            {
                OperationTypeComboBox.ItemsSource = null;
                OperationTypeComboBox.Items.Clear();

                var laborOperations = await AppServices.ApiOperationService.GetLaborOperationsAsync();

                var defaultItem = new LaborOperationModel
                {
                    Id = "",
                    LaborCode = "",
                    LaborName = "Lütfen seçiniz...",
                    SubLaborName = "",
                    Status = "active"
                };

                laborOperations.Insert(0, defaultItem);

                OperationTypeComboBox.DisplayMemberPath = "DisplayName";
                OperationTypeComboBox.ItemsSource = laborOperations;
                OperationTypeComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşçilik tipleri yüklenemedi:\n" + ex.Message);
            }
        }

        private async void DeviceLookupTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string serialNo = SerialNumberTextBox.Text?.Trim() ?? "";
                string stockCode = StockCodeTextBox.Text?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(serialNo) && string.IsNullOrWhiteSpace(stockCode))
                {
                    _selectedDevice = null;
                    DeviceNameTextBox.Text = "";
                    return;
                }

                var device = await AppServices.ApiDeviceService
                    .GetDeviceBySerialOrStockCodeAsync(serialNo, stockCode);

                if (device != null)
                {
                    _selectedDevice = device;

                    DeviceNameTextBox.Text = device.DeviceName ?? "";

                    if (string.IsNullOrWhiteSpace(StockCodeTextBox.Text))
                    {
                        StockCodeTextBox.Text = device.StockCode ?? device.DeviceCode ?? "";
                    }
                }
                else
                {
                    _selectedDevice = null;
                    DeviceNameTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                _selectedDevice = null;
                DeviceNameTextBox.Text = "";
                MessageBox.Show("Cihaz aranırken hata oluştu:\n" + ex.Message);
            }
        }

        private async void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedDevice == null)
                {
                    MessageBox.Show("Lütfen geçerli bir cihaz seçiniz.");
                    return;
                }

                var selectedLabor = OperationTypeComboBox.SelectedItem as LaborOperationModel;

                if (selectedLabor == null ||
                    string.IsNullOrWhiteSpace(selectedLabor.LaborCode) ||
                    selectedLabor.LaborName == "Lütfen seçiniz...")
                {
                    MessageBox.Show("Lütfen işçilik tipi seçiniz.");
                    return;
                }

                string productCode = _selectedDevice.DeviceCode ?? "";
                string stockCode = _selectedDevice.StockCode ?? _selectedDevice.DeviceCode ?? "";

                if (string.IsNullOrWhiteSpace(productCode))
                {
                    MessageBox.Show("Seçilen cihazın ürün kodu bulunamadı.");
                    return;
                }

                decimal price = await AppServices.ApiOperationService
                    .GetOperationPriceAsync(productCode, selectedLabor.LaborCode);

                
                _items.Add(new WorkflowTeamOperationItem
                {
                    DeviceId = _selectedDevice.Id,
                    SerialNumber = _selectedDevice.SerialNumber,
                    StockCode = stockCode,
                    DeviceName = _selectedDevice.DeviceName,

                    LaborCode = selectedLabor.LaborCode,
                    LaborName = selectedLabor.LaborName,
                    OperationType = selectedLabor.DisplayName,

                    Price = price,
                    Quantity = 1
                });
                SerialNumberTextBox.Text = "";
                StockCodeTextBox.Text = "";
                DeviceNameTextBox.Text = "";
                OperationTypeComboBox.SelectedIndex = 0;
                _selectedDevice = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem eklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WorkflowTeamOperationItem selectedItem)
            {
                _items.Remove(selectedItem);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_items.Count == 0)
                {
                    MessageBox.Show("Kaydedilecek işlem bulunamadı.");
                    return;
                }

                string payType = CenterPayRadio.IsChecked == true
                    ? "Merkez Ödeyecek"
                    : "Müşteri Ödeyecek";

                foreach (var item in _items)
                {
                    await AppServices.ApiOperationService.AddWorkflowTeamOperationAsync(
                        new WorkflowTeamOperationSaveModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            WorkflowId = _workflow.Id,
                            CustomerId = _workflow?.CustomerId ?? "",

                            DeviceId = item.DeviceId,
                            SerialNumber = item.SerialNumber,
                            StockCode = item.StockCode,
                            DeviceName = item.DeviceName,

                            OperationType = item.OperationType,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            TotalAmount = item.TotalAmount,

                            CreatedByUserMail = _loggedUser?.UserMail ?? "",
                            CreatedByName = _loggedUser?.Name ?? "",
                            CreatedBySurname = _loggedUser?.Surname ?? "",
                            CreatedByRole = _loggedUser?.UserRole ?? "",
                            CreatedDate = DateTime.UtcNow,
                            CustomerOrCenterPay = payType,
                            LaborCode = item.LaborCode,
                            LaborName = item.LaborName,
                        });
                }

                await AppServices.ApiWorkflowService.UpdateWorkflowStatusAsync(_workflow.Id, "Devam Ediyor");

                _workflow.WorkflowStatus = "Devam Ediyor";

                MessageBox.Show("İşlemler başarıyla kaydedildi.");
                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında hata oluştu:\n" + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}