using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class ReturnSetItemsPage : Page
    {
        public ReturnSetItemsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ReturnSetItemsGrid.ItemsSource = new List<ReturnSetItemDetail>
            {
                new ReturnSetItemDetail
                {
                    Id = 14,
                    ReturnProcessSetId = "",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SparePartName = "",
                    SparePartSapCode = "7738114025",
                    TtsmOrderNumber = "",
                    SapOrderNumber = "",
                    DeliveryNumber = "0",
                    ReceiptNumber = "0",
                    ServiceReceiptNumber = "1734658",
                    ReceiptArrivalDate = ""
                },
                new ReturnSetItemDetail
                {
                    Id = 2,
                    ReturnProcessSetId = "",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SparePartName = "Dış ünite ana kontrol kartı",
                    SparePartSapCode = "7739833591",
                    TtsmOrderNumber = "",
                    SapOrderNumber = "",
                    DeliveryNumber = "0",
                    ReceiptNumber = "0",
                    ServiceReceiptNumber = "125851",
                    ReceiptArrivalDate = ""
                },
                new ReturnSetItemDetail
                {
                    Id = 1,
                    ReturnProcessSetId = "",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    SparePartName = "Kompresör",
                    SparePartSapCode = "7739832538",
                    TtsmOrderNumber = "",
                    SapOrderNumber = "",
                    DeliveryNumber = "0",
                    ReceiptNumber = "0",
                    ServiceReceiptNumber = "125851",
                    ReceiptArrivalDate = ""
                }
            };
        }

        private void IncludeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kalem sete dahil edildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void CreateReturnSetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("İade seti oluşturuldu.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PrintFaultLabelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Arıza etiketi yazdırılacak.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    
}