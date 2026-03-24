using EsteknikCRM1.Models;
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

namespace EsteknikCRM1.Pages
{
    /// <summary>
    /// Interaction logic for DeviceCardOperationPage.xaml
    /// </summary>
    public partial class DeviceCardOperationPage : Page
    {
        public DeviceCardOperationPage()
        {
            InitializeComponent();
        }
        /*private void LoadDevices()
        {
            var devices = new List<DeviceModel>
            {
                new DeviceModel
                {
                    Id = "1",
                    SerialNumber = "SN-1001",
                    DeviceCode = "CMB-01",
                    DeviceName = "Climate 5000",
                    CommissionDate = "12.03.2024",
                    Brand = "Bosch",
                    TopGroup = "Kombi",
                    SubGroup = "Yoğuşmalı",
                    SpecialGroup = "Ev Tipi"
                },
                new DeviceModel
                {
                    Id = "2",
                    SerialNumber = "SN-1002",
                    DeviceCode = "KLM-02",
                    DeviceName = "Inverter Klima",
                    CommissionDate = "01.06.2023",
                    Brand = "Bosch",
                    TopGroup = "Klima",
                    SubGroup = "Split",
                    SpecialGroup = "Duvar Tipi"
                }
            };

            DeviceGrid.ItemsSource = devices;
        }*/

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cihaz kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
