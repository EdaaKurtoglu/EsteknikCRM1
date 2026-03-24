using System.Collections.Generic;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class FreeMaterialRequestsPage : Page
    {
        public FreeMaterialRequestsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            FreeMaterialGrid.ItemsSource = new List<FreeMaterialRequestItem>
            {
                new FreeMaterialRequestItem
                {
                    Id = 119686,
                    ServiceCode = "687",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    ServiceReceipt = "2012163",
                    ServiceSapCode = "65299259",
                    Customer = "ABDULLAH KARABAĞ",
                    SparePartProductName = "Ön Kapak",
                    SparePartProductSapCode = "8750503418",
                    TtsmOrderNumber = "1000216435",
                    SapOrderNumber = "0699218826",
                    CreatedDate = "06/02/2026\n13:55 +03:00",
                    WorkflowNo = "2742358",
                    OrderType = "İş Akışı Kaynaklı Bedelsiz",
                    ReturnSetNo = ""
                },
                new FreeMaterialRequestItem
                {
                    Id = 117392,
                    ServiceCode = "687",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    ServiceReceipt = "1920428",
                    ServiceSapCode = "65299259",
                    Customer = "ZİRAAT BANKASI KAYSERİ PGM 2238050 NO.LU 15 TEMMUZ YERLEŞKESİ",
                    SparePartProductName = "DC inverter kompresör",
                    SparePartProductSapCode = "7733700165",
                    TtsmOrderNumber = "1000213105",
                    SapOrderNumber = "0699216815",
                    CreatedDate = "28/01/2026\n19:00 +03:00",
                    WorkflowNo = "2604735",
                    OrderType = "İade Seti Kaynaklı Bedelsiz",
                    ReturnSetNo = "17469"
                },
                new FreeMaterialRequestItem
                {
                    Id = 98407,
                    ServiceCode = "687",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    ServiceReceipt = "1662179",
                    ServiceSapCode = "65299259",
                    Customer = "BUZÇELİK BUZÇELİK BUZÇELİK",
                    SparePartProductName = "ARC T-B, Siyah",
                    SparePartProductSapCode = "7738114025",
                    TtsmOrderNumber = "1000180857",
                    SapOrderNumber = "0699200455",
                    CreatedDate = "20/08/2025\n19:27 +03:00",
                    WorkflowNo = "2236993",
                    OrderType = "İş Akışı Kaynaklı Bedelsiz",
                    ReturnSetNo = ""
                },
                new FreeMaterialRequestItem
                {
                    Id = 98405,
                    ServiceCode = "687",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    ServiceReceipt = "1662176",
                    ServiceSapCode = "65299259",
                    Customer = "BUZÇELİK BUZÇELİK BUZÇELİK",
                    SparePartProductName = "ARC T-B, Siyah",
                    SparePartProductSapCode = "7738114025",
                    TtsmOrderNumber = "1000180856",
                    SapOrderNumber = "0699200454",
                    CreatedDate = "20/08/2025\n19:24 +03:00",
                    WorkflowNo = "2236991",
                    OrderType = "İş Akışı Kaynaklı Bedelsiz",
                    ReturnSetNo = ""
                },
                new FreeMaterialRequestItem
                {
                    Id = 98404,
                    ServiceCode = "687",
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    ServiceReceipt = "1662174",
                    ServiceSapCode = "65299259",
                    Customer = "BUZÇELİK BUZÇELİK BUZÇELİK",
                    SparePartProductName = "ARC T-B, Siyah",
                    SparePartProductSapCode = "7738114025",
                    TtsmOrderNumber = "1000180855",
                    SapOrderNumber = "0699200453",
                    CreatedDate = "20/08/2025\n19:22 +03:00",
                    WorkflowNo = "2236987",
                    OrderType = "İş Akışı Kaynaklı Bedelsiz",
                    ReturnSetNo = ""
                }
            };
        }
    }

    
}