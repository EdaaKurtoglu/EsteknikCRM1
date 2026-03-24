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
    /// Interaction logic for StockActionDetailPage.xaml
    /// </summary>
    public partial class StockActionDetailPage : Page
    {
        public StockActionDetailPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            StockMovementGrid.ItemsSource = new List<StockMovementItem>
            {
                new StockMovementItem
                {
                    Id = 2664867,
                    ExtraDescription2 = "",
                    ServiceName = "ES IKLIMLENDIRME VE OTOMASYON SISTEMLERI OTOMOTIV TAAHHÜT HAYVANCILIK SAN. TIC. LTD. STI.",
                    TeamName = "",
                    Date = "24/03/2026",
                    StockType = "Yedek Parça",
                    StockCode = "7738706478",
                    StockName = "Servis T-shirt S 2025",
                    MovementType = "Giris",
                    Quantity = 9,
                    Balance = 20,
                    Description = ""
                },
                new StockMovementItem
                {
                    Id = 2664866,
                    ExtraDescription2 = "",
                    ServiceName = "ES IKLIMLENDIRME VE OTOMASYON SISTEMLERI OTOMOTIV TAAHHÜT HAYVANCILIK SAN. TIC. LTD. STI.",
                    TeamName = "",
                    Date = "24/03/2026",
                    StockType = "Yedek Parça",
                    StockCode = "7738706479",
                    StockName = "Servis T-shirt M 2025",
                    MovementType = "Giris",
                    Quantity = 29,
                    Balance = 44,
                    Description = ""
                }
            };
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Veriler yenilendi.");
        }

        private void InternalTransferButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("İç transfer kaydı ekranı açılacak.");
        }
    }

}

