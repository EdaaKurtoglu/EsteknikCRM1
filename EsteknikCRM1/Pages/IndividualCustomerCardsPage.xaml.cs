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
    /// Interaction logic for IndividualCustomerCardsPage.xaml
    /// </summary>
    public partial class IndividualCustomerCardsPage : Page
    {
        public IndividualCustomerCardsPage()
        {
            InitializeComponent();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Filtreleme logic buraya
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var row = (sender as Button).DataContext;
            MessageBox.Show("Selected: " + row.ToString());
        }

    }
}
