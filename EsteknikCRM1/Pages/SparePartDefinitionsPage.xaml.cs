using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class SparePartDefinitionsPage : Page
    {
        public SparePartDefinitionsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            SparePartGrid.ItemsSource = new List<SparePartItem>
            {
                new SparePartItem
                {
                    Id = 30770,
                    SparePartCode = "8733501702",
                    SparePartName = "Tutucu",
                    Vat = "20",
                    Price = "₺ 1.380,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30769,
                    SparePartCode = "8733501222",
                    SparePartName = "Demiryolu",
                    Vat = "20",
                    Price = "₺ 4.790,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30768,
                    SparePartCode = "8733501628",
                    SparePartName = "Sag Sızdırmazlık Plakasi",
                    Vat = "20",
                    Price = "₺ 1.080,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30767,
                    SparePartCode = "8733501220",
                    SparePartName = "Demiryolu",
                    Vat = "20",
                    Price = "₺ 890,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30766,
                    SparePartCode = "8733501611",
                    SparePartName = "Plaka arka",
                    Vat = "20",
                    Price = "₺ 1.760,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30765,
                    SparePartCode = "8733501225",
                    SparePartName = "Tasiyici",
                    Vat = "20",
                    Price = "₺ 605,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30764,
                    SparePartCode = "8738215224",
                    SparePartName = "Sicaklik Sensörü (Boru) T3",
                    Vat = "20",
                    Price = "₺ 755,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30754,
                    SparePartCode = "7738706779",
                    SparePartName = "Shimge Pompa",
                    Vat = "20",
                    Price = "₺ 39.105,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Pompa (harici, üçüncü parti)",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30753,
                    SparePartCode = "8738216158",
                    SparePartName = "Elektronik kart CUHP Bo/ Ju/ELM IDU V1.14",
                    Vat = "20",
                    Price = "₺ 11.695,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Baskılı devre kartı",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30752,
                    SparePartCode = "7738706778",
                    SparePartName = "ELCO GT-S43-2-RP2 /TC S LOG",
                    Vat = "20",
                    Price = "₺ 115.575,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Uygun B kodu Mevcut Değil",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30751,
                    SparePartCode = "8733501284",
                    SparePartName = "Kondenser",
                    Vat = "20",
                    Price = "₺ 134.930,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "evaporatör",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30750,
                    SparePartCode = "8738214400",
                    SparePartName = "4 yollu valf",
                    Vat = "20",
                    Price = "₺ 8.770,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "4-yollu vana (soğutucu devresi)",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30749,
                    SparePartCode = "87186661730",
                    SparePartName = "Bakim Kapagi WB5-IV",
                    Vat = "20",
                    Price = "₺ 1.760,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Gövde / Kapak",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30748,
                    SparePartCode = "8755003961",
                    SparePartName = "HCM-Kod Anahtari 20383",
                    Vat = "20",
                    Price = "₺ 415,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Codeplug / BIM / KIM / HKM / BCM",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30747,
                    SparePartCode = "8738215281",
                    SparePartName = "Bosaltma Hortumu",
                    Vat = "20",
                    Price = "₺ 150,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Kontrol hortumları / hortum kiti",
                    Status = "Aktif"
                },
                new SparePartItem
                {
                    Id = 30746,
                    SparePartCode = "8733501626",
                    SparePartName = "Sag Sizdirmazlik Plakasi",
                    Vat = "20",
                    Price = "₺ 1.760,00 TRL",
                    WarrantyPeriod = "12",
                    Supplier = "",
                    BCode = "Brülör conta",
                    Status = "Aktif"
                }
            };
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yedek parça detay sayfası açılacak.");
        }

        private void AddNewSparePart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SparePartDefinitionOperationPage());
        }
    }

    
}