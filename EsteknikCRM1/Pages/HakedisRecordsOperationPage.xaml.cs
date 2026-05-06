using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Google.LongRunning.Operations;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisRecordsOperationPage : Page
    {
        private string _selectedPayType = "Merkez Ödeyecek";

        private readonly Brush ActiveTabBrush =
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#14A0DB"));

        private readonly Brush InactiveTabBrush =
            new SolidColorBrush(Colors.Transparent);
        UserModel _user;
        public HakedisRecordsOperationPage(UserModel user)
        {
            _user = user;
            InitializeComponent();
            Loaded += HakedisRecordsOperationPage_Loaded;
        }

        private async void HakedisRecordsOperationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveTabVisualAsync();
            await LoadWorkflowOperationsAsync();
        }

        private async Task LoadWorkflowOperationsAsync()
        {
            try
            {
                var operations = await AppServices.ApiOperationService.GetWorkflowTeamOperationsAsync();

                var filtered = operations
                    .Where(x =>
                        (x.IsBilled == false) && // 🔥 EN ÖNEMLİ SATIR
                        string.Equals(x.CustomerOrCenterPay ?? "", _selectedPayType, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var rows = new List<HakedisOperationItem>();
                int rowNo = 1;

                foreach (var item in filtered)
                {
                    string customerName = "";
                    string serialNo = item.SerialNumber ?? "";
                    string productCode = item.StockCode ?? "";
                    string productName = item.DeviceName ?? "";
                    string serviceReceiptType = "";
                    string laborCode = item.LaborCode ?? "";
                    string laborName = !string.IsNullOrWhiteSpace(item.LaborName)
                        ? item.LaborName
                        : item.OperationType ?? "";
                    string subLaborName = "";
                    decimal operationPrice = 0;

                    if (!string.IsNullOrWhiteSpace(item.CustomerId))
                    {
                        var customer = await AppServices.ApiCustomerService.GetCustomerByIdAsync(item.CustomerId);

                        if (customer != null)
                            customerName = $"{customer.Name ?? ""} {customer.Surname ?? ""}".Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(item.DeviceId))
                    {
                        var device = await AppServices.ApiDeviceService.GetDeviceByIdAsync(item.DeviceId);

                        if (device != null)
                        {
                            serialNo = string.IsNullOrWhiteSpace(device.SerialNumber)
                                ? serialNo
                                : device.SerialNumber;

                            productCode = string.IsNullOrWhiteSpace(device.StockCode)
                                ? (device.DeviceCode ?? productCode)
                                : device.StockCode;

                            productName = string.IsNullOrWhiteSpace(device.DeviceName)
                                ? productName
                                : device.DeviceName;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(item.WorkflowId))
                    {
                        var workflow = await AppServices.ApiWorkflowService.GetWorkflowByIdAsync(item.WorkflowId);

                        if (workflow != null)
                        {
                            serviceReceiptType = workflow.FlowType ?? "";
                            subLaborName = workflow.SubCategoryName ?? "";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(productCode) && !string.IsNullOrWhiteSpace(laborCode))
                    {
                        operationPrice = await AppServices.ApiOperationService
                            .GetOperationPriceAsync(productCode, laborCode);
                    }
                    else
                    {
                        operationPrice = item.Price;
                    }

                    rows.Add(new HakedisOperationItem
                    {
                        Id = rowNo,
                        OperationId = item.Id,
                        WorkflowReceiptNo = item.WorkflowId ?? "",
                        Customer = customerName,
                        ServiceReceiptType = serviceReceiptType,
                        DeviceSerialNo = serialNo,
                        ProductCode = productCode,
                        ProductName = productName,
                        LaborName = laborName,
                        SubLaborName = subLaborName,
                        Amount = operationPrice.ToString("0.##"),
                        Quantity = item.Quantity.ToString()
                    });

                    rowNo++;
                }

                HakedisOperationGrid.ItemsSource = rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hakediş gridi yüklenirken hata oluştu:\n" + ex.Message);
            }
        }
        private async Task SetActiveTabVisualAsync()
        {
            if (_selectedPayType == "Merkez Ödeyecek")
            {
                CenterTabUnderline.Background = ActiveTabBrush;
                CustomerTabUnderline.Background = InactiveTabBrush;

                CenterTabText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F2D3D"));
                CustomerTabText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F2D3D"));
            }
            else
            {
                CenterTabUnderline.Background = InactiveTabBrush;
                CustomerTabUnderline.Background = ActiveTabBrush;

                CenterTabText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F2D3D"));
                CustomerTabText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1F2D3D"));
            }
            
        }

        private async void CenterTabButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedPayType = "Merkez Ödeyecek";
            SetActiveTabVisualAsync();
            await LoadWorkflowOperationsAsync();
        }

        private async void CustomerTabButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedPayType = "Müşteri Ödeyecek";
            SetActiveTabVisualAsync();
            await LoadWorkflowOperationsAsync();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void CreateSetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = HakedisOperationGrid.ItemsSource as List<HakedisOperationItem>;

                if (items == null || items.Count == 0)
                {
                    MessageBox.Show("Set oluşturulacak kayıt bulunamadı.",
                        "Uyarı",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                decimal totalAmount = items.Sum(x =>
                    decimal.TryParse(x.Amount, out var val) ? val : 0);

                int totalQuantity = items.Sum(x =>
                    int.TryParse(x.Quantity, out var q) ? q : 0);

                var setModel = new HakedisSetModel
                {
                    Id = Guid.NewGuid().ToString(),
                    ServiceTitle = "ES İKLİMLENDİRME SAN.TİC.LTD.ŞTİ.",
                    SapServiceCode = "SAP-001",
                    ServiceResponsible = _selectedPayType == "Merkez Ödeyecek"
                        ? "Merkez"
                        : "Müşteri",
                    
                    GreenCount = 0,
                    BlueCount = 0,
                    RedCount = 0,

                    InvoiceNumber = "",
                    InvoiceDate = "",
                    PreApprovalDate = "",
                    PreApprovalApproveDate = "",
                    SetDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    SetApproveDate = "",
                    ExportDate = "",
                    
                    PayType = _selectedPayType,
                    TotalAmount = totalAmount,
                    TotalQuantity = totalQuantity,
                    CreatedDate = DateTime.UtcNow
                };

                string hakedisSetId = await AppServices.ApiHakedisSetService.AddHakedisSetAsync(setModel);

                // 🔥 ID listesi çıkar
                var operationIds = items
                 .Select(x => x.OperationId)
                 .Where(x => !string.IsNullOrWhiteSpace(x))
                 .ToList();

                // 🔥 SILME YOK → FLAG
                await AppServices.ApiOperationService.MarkOperationsAsBilledAsync(hakedisSetId, operationIds);

                // grid temizle
                HakedisOperationGrid.ItemsSource = null;

                MessageBox.Show("Hakediş seti oluşturuldu ve kayıtlar işaretlendi.");

                NavigationService?.Navigate(new HakedisRecordsPage(_user));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:\n" + ex.Message);
            }
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(_user));
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new OperationsPage());
        }

        private void HakedisRecords_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HakedisRecordsPage(_user));
        }

        private void HakedisOperation_Click(object sender, RoutedEventArgs e)
        {
            // aktif sayfa → boş bırak
        }
    }
}