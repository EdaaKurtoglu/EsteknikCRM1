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
            InitializeComponent(); LoadData();
        }

        private void LoadData()
        {
            HakedisOperationGrid.ItemsSource = new List<HakedisOperationItem>
            {
                new HakedisOperationItem
                {
                    Id = 3180595,
                    WorkflowReceiptNo = "2833419",
                    Customer = "DERYA OFLAZ",
                    ServiceReceiptType = "Garantili İş",
                    DeviceSerialNo = "86DM5390030307733703539",
                    ProductCode = "7733703539",
                    ProductName = "CL2001 53 E",
                    LaborName = "MALZEMESİZ İŞÇİLİK BEDELİ",
                    SubLaborName = "",
                    Amount = "1.083,33 TRL",
                    Quantity = ""
                },
                new HakedisOperationItem
                {
                    Id = 3180105,
                    WorkflowReceiptNo = "2811439",
                    Customer = "OSMAN ALİBEYOĞLU OSMAN",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5390027877733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "MONTAJ İŞÇİLİĞİ",
                    SubLaborName = "",
                    Amount = "2.266,67 TRL",
                    Quantity = ""
                },
                new HakedisOperationItem
                {
                    Id = 3178598,
                    WorkflowReceiptNo = "2819673",
                    Customer = "GÜLTEPE KONFERANS SALONU :",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "400001234230302621",
                    ProductCode = "7759009881",
                    ProductName = "YVGVXH112WAR--GY High static (100-196Pa) duct 11,2kW",
                    LaborName = "İLK ÇALIŞTIRMA",
                    SubLaborName = "",
                    Amount = "312,50 TRL",
                    Quantity = ""
                },
                new HakedisOperationItem
                {
                    Id = 3178595,
                    WorkflowReceiptNo = "2819673",
                    Customer = "GÜLTEPE KONFERANS SALONU :",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "400001234230302616",
                    ProductCode = "7759009881",
                    ProductName = "YVGVXH112WAR--GY High static (100-196Pa) duct 11,2kW",
                    LaborName = "İLK ÇALIŞTIRMA",
                    SubLaborName = "",
                    Amount = "312,50 TRL",
                    Quantity = ""
                },
                new HakedisOperationItem
                {
                    Id = 3178605,
                    WorkflowReceiptNo = "2819673",
                    Customer = "GÜLTEPE KONFERANS SALONU :",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "400001234230302599",
                    ProductCode = "7759009881",
                    ProductName = "YVGVXH112WAR--GY High static (100-196Pa) duct 11,2kW",
                    LaborName = "İLK ÇALIŞTIRMA",
                    SubLaborName = "",
                    Amount = "312,50 TRL",
                    Quantity = ""
                },
                new HakedisOperationItem
                {
                    Id = 3178602,
                    WorkflowReceiptNo = "2819673",
                    Customer = "GÜLTEPE KONFERANS SALONU :",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "400001234230302598",
                    ProductCode = "7759009881",
                    ProductName = "YVGVXH112WAR--GY High static (100-196Pa) duct 11,2kW",
                    LaborName = "İLK ÇALIŞTIRMA",
                    SubLaborName = "",
                    Amount = "312,50 TRL",
                    Quantity = ""
                }
            };
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
