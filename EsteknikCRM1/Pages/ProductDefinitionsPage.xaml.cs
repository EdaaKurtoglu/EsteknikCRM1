using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class ProductDefinitionsPage : Page
    {
        public ProductDefinitionsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ProductGrid.ItemsSource = new List<ProductItem>
            {
                new ProductItem
                {
                    Id = 10422,
                    ProductCode = "348020271",
                    ProductName = "YV28YH022KAS-D-Y Mini VRF Series",
                    Brand = "York",
                    TopGroup = "VRF SISTEMLER",
                    SubGroup = "Mini VRF Dış Üniteler",
                    SpecialGroup = "VRF KLİMA",
                    CostCenter = "829991",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10421,
                    ProductCode = "348020270",
                    ProductName = "RAS-14QHA(T) İÇ ÜNİTE",
                    Brand = "Hitachi",
                    TopGroup = "Split Klimalar",
                    SubGroup = "Split Duvar Tipi İç Üniteler",
                    SpecialGroup = "SPLIT KLİMA",
                    CostCenter = "829992",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10420,
                    ProductCode = "348020269",
                    ProductName = "RAC-35NPA(T) DIŞ ÜNİTE",
                    Brand = "Hitachi",
                    TopGroup = "Split Klimalar",
                    SubGroup = "Split Dış Üniteler",
                    SpecialGroup = "SPLIT KLİMA",
                    CostCenter = "829992",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10419,
                    ProductCode = "348020268",
                    ProductName = "YHKF09YEEBMJO-X 9000 Btu OU",
                    Brand = "York",
                    TopGroup = "Split Klimalar",
                    SubGroup = "Split Dış Üniteler",
                    SpecialGroup = "SPLIT KLİMA",
                    CostCenter = "829991",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Pasif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10418,
                    ProductCode = "YVKVXH036WAR-GY 4 Way Compact Cassette 3,6 Kw",
                    ProductName = "",
                    Brand = "York",
                    TopGroup = "VRF SISTEMLER",
                    SubGroup = "VRF Kaset Tipi İç Üniteler",
                    SpecialGroup = "VRF KLİMA",
                    CostCenter = "829991",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10406,
                    ProductCode = "7724002676",
                    ProductName = "GCK G20 to G31 GC1200W 28/30 AZ, GE, SY",
                    Brand = "Bosch",
                    TopGroup = "Isıtma Sistemleri Aksesuarları",
                    SubGroup = "Gaz Dönüşüm Setleri",
                    SpecialGroup = "",
                    CostCenter = "",
                    WarrantyMonth = "",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10405,
                    ProductCode = "7738706711",
                    ProductName = "Retrofit Kazan Paneli BCO(250521)",
                    Brand = "Bosch",
                    TopGroup = "Buhar Kazanları",
                    SubGroup = "Buhar Kazanı Aksesuarları",
                    SpecialGroup = "BUHAR KAZANI",
                    CostCenter = "829940",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10403,
                    ProductCode = "7733704761",
                    ProductName = "WDC3-86S Kablolu VRF oda kumandasi",
                    Brand = "Bosch",
                    TopGroup = "VRF Sistemler",
                    SubGroup = "VRF Merkezi Kumandalar",
                    SpecialGroup = "VRF KLİMA",
                    CostCenter = "829990",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10402,
                    ProductCode = "7738706708",
                    ProductName = "Bosch UL-S (1.250;10) Buhar Kaz.(239218)",
                    Brand = "Bosch",
                    TopGroup = "Buhar Kazanları",
                    SubGroup = "Buhar Kazanları",
                    SpecialGroup = "BUHAR KAZANI",
                    CostCenter = "829940",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10401,
                    ProductCode = "7738706709",
                    ProductName = "ElcoVG6.1600M-RKN 11/2''TC O2+F(239218)",
                    Brand = "ELCO",
                    TopGroup = "Endüstriyel Brülörler",
                    SubGroup = "Gaz Brülörü - 300 mbar",
                    SpecialGroup = "BRÜLÖR",
                    CostCenter = "829937",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10400,
                    ProductCode = "7738706710",
                    ProductName = "Bosch UL-S 28000 Buhar K.(249063)",
                    Brand = "Bosch",
                    TopGroup = "Buhar Kazanları",
                    SubGroup = "Buhar Kazanları",
                    SpecialGroup = "BUHAR KAZANI",
                    CostCenter = "829940",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10399,
                    ProductCode = "7738346565",
                    ProductName = "Fillcontrol Smart",
                    Brand = "REFLEX",
                    TopGroup = "Genleşme Depoları",
                    SubGroup = "Su Besleme Sistemleri",
                    SpecialGroup = "ISITMA AKSESUARI",
                    CostCenter = "829942",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10398,
                    ProductCode = "7724003124",
                    ProductName = "Grundfos UPM20M 25-130 180 Auto Pompa",
                    Brand = "Grundfos",
                    TopGroup = "Pompalar",
                    SubGroup = "Sirkülasyon Pompaları",
                    SpecialGroup = "ISITMA AKSESUARI",
                    CostCenter = "829940",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                },
                new ProductItem
                {
                    Id = 10397,
                    ProductCode = "7724003122",
                    ProductName = "Grundfos UPM3L 25-70 130 Auto Pompa",
                    Brand = "Grundfos",
                    TopGroup = "Pompalar",
                    SubGroup = "Sirkülasyon Pompaları",
                    SpecialGroup = "ISITMA AKSESUARI",
                    CostCenter = "829940",
                    WarrantyMonth = "24",
                    Price = "",
                    Status = "Aktif",
                    Country = "Türkiye"
                }
            };
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProductDefinitionOperationPage());
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ürün detay sayfası açılacak.");
        }
    }

    
}