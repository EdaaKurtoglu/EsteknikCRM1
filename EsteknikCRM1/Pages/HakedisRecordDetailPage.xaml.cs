using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Google.LongRunning.Operations;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisRecordDetailPage : Page
    {
        public HakedisRecordDetailPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            HakedisDetailGrid.ItemsSource = new List<HakedisDetailItem>
            {
                new HakedisDetailItem
                {
                    Id = 7392589,
                    WorkflowReceiptNo = "2825893",
                    Customer = "İSMAİL ÖZDEMİR",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5790002567733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "BAKIR BORU SET BEDELİ",
                    SubLaborName = "",
                    Amount = "1.852,50 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392577,
                    WorkflowReceiptNo = "2802885",
                    Customer = "MESUT YAZICI",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5790025607733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "BAKIR BORU SET BEDELİ",
                    SubLaborName = "",
                    Amount = "1.852,50 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392569,
                    WorkflowReceiptNo = "2821922",
                    Customer = "CEM ULUTÜRK",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5790005627733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "BAKIR BORU SET BEDELİ",
                    SubLaborName = "",
                    Amount = "1.852,50 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392557,
                    WorkflowReceiptNo = "2822479",
                    Customer = "MERTCAN BÜYÜKİNCE",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5790024777733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "BAKIR BORU SET BEDELİ",
                    SubLaborName = "",
                    Amount = "1.852,50 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392588,
                    WorkflowReceiptNo = "2825893",
                    Customer = "İSMAİL ÖZDEMİR",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5790027527733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "BAKIR BORU SET BEDELİ",
                    SubLaborName = "",
                    Amount = "1.852,50 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392559,
                    WorkflowReceiptNo = "2815455",
                    Customer = "MEHMET SAYGILI",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5770002897733701569",
                    ProductCode = "7733701569",
                    ProductName = "Climate 3000i - CL3000i 53 E",
                    LaborName = "BAKIR BORU SET BEDELİ",
                    SubLaborName = "",
                    Amount = "2.041,67 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392566,
                    WorkflowReceiptNo = "2820491",
                    Customer = "FEYZULLAH ÇANKAYA",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5370045997733703536",
                    ProductCode = "7733703536",
                    ProductName = "CL2001U W 35 E",
                    LaborName = "İLK ÇALIŞTIRMA",
                    SubLaborName = "",
                    Amount = "270,83 TRL"
                },
                new HakedisDetailItem
                {
                    Id = 7392555,
                    WorkflowReceiptNo = "2820497",
                    Customer = "SAİT AKYEL SAİT AKYEL",
                    ServiceReceiptType = "İlk Çalıştırma",
                    DeviceSerialNo = "86DM5370051347733703537",
                    ProductCode = "7733703537",
                    ProductName = "CL2001 35 E",
                    LaborName = "İLK ÇALIŞTIRMA",
                    SubLaborName = "",
                    Amount = "458,33 TRL"
                }
            };
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Veriler yenilendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kayıt silinecek.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new HomeContentPage(currentUser));
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new OperationsPage());
        }

        private void HakedisRecords_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new HakedisRecordsPage());
        }

        private void HakedisOperation_Click(object sender, RoutedEventArgs e)
        {
            // aktif sayfa → boş bırak
        }
    }

}