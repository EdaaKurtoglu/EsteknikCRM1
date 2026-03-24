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
    /// Interaction logic for DeviceCardsPage.xaml
    /// </summary>
    public partial class DeviceCardsPage : Page
    {
        public DeviceCardsPage()
        {
            InitializeComponent();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void AddNewDevice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DeviceCardOperationPage());
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // canlı arama istersen:
            // ApplyFilter();
        }

        private void ApplyFilter()
        {
            string searchText = SearchTextBox.Text.ToLower();

            var view = CollectionViewSource.GetDefaultView(DeviceGrid.ItemsSource);
            view.Filter = item =>
            {
                var device = item as Models.DeviceModel;

                return device.SerialNumber.ToLower().Contains(searchText)
                    || device.DeviceName.ToLower().Contains(searchText)
                    || device.Brand.ToLower().Contains(searchText);
            };
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var row = (sender as Button).DataContext as Models.DeviceModel;
            MessageBox.Show("Selected Device: " + row?.DeviceName);
        }

    }
}
