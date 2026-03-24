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
using EsteknikCRM1.Pages;

namespace EsteknikCRM1
{
    /// <summary>
    /// Interaction logic for StockActionPage.xaml
    /// </summary>
    public partial class StockActionPage : Page
    {
        public StockActionPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            StockDataGrid.ItemsSource = new List<StockActionItem>
            {
                new StockActionItem
                {
                    Id = 687,
                    ServiceTitle = "ES İKLİMLENDİRME VE OTOMASYON SİSTEMLERİ OTOMOTİV TAAHHÜT HAYVANCILIK SAN. TİC. LTD. ŞTİ.",
                    ServiceType = "Yetkili Servis",
                    City = "Kayseri",
                    District = "Melikgazi"
                }
            };
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new StockActionDetailPage());

        }

    }
}
