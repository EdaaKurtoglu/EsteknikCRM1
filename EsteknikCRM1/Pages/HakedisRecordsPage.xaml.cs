using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisRecordsPage : Page
    {
        public HakedisRecordsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            HakedisGrid.ItemsSource = new List<HakedisRecordItem>
            {
                new HakedisRecordItem
                {
                    Id = 59420,
                    ServiceTitle = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SapServiceCode = "63201392",
                    ServiceResponsible = "Vedat Demir",
                    GreenCount = 0,
                    BlueCount = 0,
                    RedCount = 0,
                    InvoiceNumber = "",
                    InvoiceDate = "",
                    PreApprovalDate = "23/03/2026\n14:41:05",
                    PreApprovalApproveDate = "",
                    SetDate = "",
                    SetApproveDate = "",
                    ExportDate = ""
                },
                new HakedisRecordItem
                {
                    Id = 58977,
                    ServiceTitle = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SapServiceCode = "63201392",
                    ServiceResponsible = "Vedat Demir",
                    GreenCount = 70,
                    BlueCount = 0,
                    RedCount = 0,
                    InvoiceNumber = "AAA2026000000037",
                    InvoiceDate = "23/03/2026",
                    PreApprovalDate = "13/03/2026\n12:36:10",
                    PreApprovalApproveDate = "19/03/2026\n10:21:38",
                    SetDate = "23/03/2026\n09:06:22",
                    SetApproveDate = "24/03/2026\n10:01:43",
                    ExportDate = "24/03/2026\n12:53:58"
                },
                new HakedisRecordItem
                {
                    Id = 58366,
                    ServiceTitle = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SapServiceCode = "63201392",
                    ServiceResponsible = "Vedat Demir",
                    GreenCount = 223,
                    BlueCount = 0,
                    RedCount = 0,
                    InvoiceNumber = "AAA2026000000035",
                    InvoiceDate = "16/03/2026",
                    PreApprovalDate = "02/03/2026\n15:08:16",
                    PreApprovalApproveDate = "16/03/2026\n13:28:08",
                    SetDate = "16/03/2026\n13:42:38",
                    SetApproveDate = "18/03/2026\n13:28:36",
                    ExportDate = "18/03/2026\n14:35:12"
                },
                new HakedisRecordItem
                {
                    Id = 58145,
                    ServiceTitle = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SapServiceCode = "63201392",
                    ServiceResponsible = "Vedat Demir",
                    GreenCount = 68,
                    BlueCount = 0,
                    RedCount = 0,
                    InvoiceNumber = "AAA2026000000032",
                    InvoiceDate = "02/03/2026",
                    PreApprovalDate = "26/02/2026\n17:03:48",
                    PreApprovalApproveDate = "02/03/2026\n11:03:03",
                    SetDate = "02/03/2026\n13:44:33",
                    SetApproveDate = "03/03/2026\n08:38:53",
                    ExportDate = "03/03/2026\n08:46:23"
                }
            };
        }

        private void AddNewRecord_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HakedisRecordsOperationPage());
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Filtreleme çalıştırıldı.");
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hakediş detay sayfası açılacak.");
        }
    }

    
}