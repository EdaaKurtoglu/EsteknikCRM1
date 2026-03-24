using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class ReturnSetPage : Page
    {
        public ReturnSetPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ReturnSetGrid.ItemsSource = new List<ReturnSetItem>
            {
                new ReturnSetItem
                {
                    Id = 17469,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "2666702",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "12/01/2026\n10:24 +03:00",
                    ItemCount = "3"
                },
                new ReturnSetItem
                {
                    Id = 16836,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "2500800",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "26/11/2025\n12:47 +03:00",
                    ItemCount = "56"
                },
                new ReturnSetItem
                {
                    Id = 14480,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "2046662",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "26/05/2025\n17:30 +03:00",
                    ItemCount = "2"
                },
                new ReturnSetItem
                {
                    Id = 14079,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "2000757",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "26/04/2025\n08:51 +03:00",
                    ItemCount = "1"
                },
                new ReturnSetItem
                {
                    Id = 11903,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "1689954",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "28/11/2024\n14:22 +03:00",
                    ItemCount = "1"
                },
                new ReturnSetItem
                {
                    Id = 11275,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "1536454",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "15/10/2024\n16:37 +03:00",
                    ItemCount = "2"
                },
                new ReturnSetItem
                {
                    Id = 11009,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "1490796",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "24/09/2024\n13:26 +03:00",
                    ItemCount = "6"
                },
                new ReturnSetItem
                {
                    Id = 10670,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "1438428",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "19/08/2024\n17:56 +03:00",
                    ItemCount = "2"
                },
                new ReturnSetItem
                {
                    Id = 2674,
                    ReturnSetType = "Garantiden Yedek Parça İade Seti",
                    ProcessType = "Bedelsiz",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    FlowNumber = "495659",
                    Description = "",
                    Completed = "Evet",
                    SetDate = "11/01/2023\n12:10 +03:00",
                    ItemCount = "1"
                }
            };
        }

        private void CreateReturnSet_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ReturnSetItemsPage());
        }

        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Listeleme yapıldı.");
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("İade seti detay ekranı açılacak.");
        }

        private void CancelSetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("İade seti iptal edilecek.");
        }
    }

    
}