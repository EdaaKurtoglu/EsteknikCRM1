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
    /// Interaction logic for IndividualCustomerOperationPage.xaml
    /// </summary>
    public partial class IndividualCustomerOperationPage : Page
    {
        public IndividualCustomerOperationPage()
        {
            InitializeComponent();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new HomeContentPage());
        }

        private void Cards_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new CardsPage());
        }

        private void IndividualCustomers_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new IndividualCustomerCardsPage());
        }

        private void IndividualCustomersOperation_Click(object sender, RoutedEventArgs e)
        {
            // aktif sayfa
        }
    }
}
