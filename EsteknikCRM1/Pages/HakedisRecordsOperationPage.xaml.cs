using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    /// <summary>
    /// Interaction logic for HakedisRecordsOperationPage.xaml
    /// </summary>
    public partial class HakedisRecordsOperationPage : Page
    {
        public HakedisRecordsOperationPage()
        {
            InitializeComponent(); 
            Loaded += HakedisRecordsOperationPage_Loaded;
        }

        private async void HakedisRecordsOperationPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompletedWorkflowsAsync();
        }

        private async Task LoadCompletedWorkflowsAsync()
        {
            try
            {
                var workflows = await FirebaseService.Instance.GetCompletedWorkflowsAsync();
                var rows = new List<HakedisOperationItem>();

                foreach (var item in workflows)
                {
                   
                    string customerName = "";
                    string serialNo = "";
                    string productCode = "";
                    string productName = "";

                    if (!string.IsNullOrWhiteSpace(item.CustomerId))
                    {
                        var customer = await FirebaseService.Instance.GetCustomerByIdAsync(item.CustomerId);
                        if (customer != null)
                            customerName = (customer.Name + " " + customer.Surname).Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(item.DeviceId))
                    {
                        var device = await FirebaseService.Instance.GetDeviceByIdAsync(item.DeviceId);
                        if (device != null)
                        {
                            serialNo = device.SerialNumber ?? "";
                            productCode = device.DeviceCode ?? "";
                            productName = device.DeviceName ?? "";
                        }
                    }
                    int row = 1;
                    rows.Add(new HakedisOperationItem
                    {
                        Id = row,
                        WorkflowReceiptNo = item.Id, // istersen ayrı fis no alanın varsa onu ver
                        Customer = customerName,
                        ServiceReceiptType = item.FlowType ?? "",
                        DeviceSerialNo = serialNo,
                        ProductCode = productCode,
                        ProductName = productName,
                        LaborName = item.CategoryName ?? "",
                        SubLaborName = item.SubCategoryName ?? "",
                        Amount = "0",
                        Quantity = "1"
                        
                    });
                    row++;
                }

                HakedisOperationGrid.ItemsSource = rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hakediş gridi yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }


        private void CreateSetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hakediş seti oluşturuldu.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}
