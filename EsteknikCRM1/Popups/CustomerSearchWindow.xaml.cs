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
using System.Windows.Shapes;
using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Popups
{
    /// <summary>
    /// Interaction logic for CustomerSearchWindow.xaml
    /// </summary>
    public partial class CustomerSearchWindow : Window
    {
        public CustomerSearchWindow()
        {
            InitializeComponent();
        }
        public CustomerModel SelectedCustomer { get; set; }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            var customers = await FirebaseService.Instance.GetCustomersAsync();


            CustomerGrid.ItemsSource = customers;
            CustomerGrid.Visibility = Visibility.Visible;
            ResultRow.Height = new GridLength(1, GridUnitType.Star);
        }

        private void SelectCustomer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            CustomerModel customer = btn.DataContext as CustomerModel;

            if (customer != null)
            {
                SelectedCustomer = customer;
                DialogResult = true;
                Close();
            }
        }

    }
}
