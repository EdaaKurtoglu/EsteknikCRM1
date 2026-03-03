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
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var data = button.DataContext;

            MessageBox.Show("Selected: " + data?.ToString());
        }

    }
}
