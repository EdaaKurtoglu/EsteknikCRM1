using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class ProductDefinitionOperationPage : Page
    {
        public ProductDefinitionOperationPage()
        {
            InitializeComponent();
        }

        private void SearchCostCenterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Masraf merkezi arama penceresi açılacak.");
        }

        private void SearchBCodeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("B kodu arama penceresi açılacak.");
        }

        private void SearchProductManagerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ürün yöneticisi arama penceresi açılacak.");
        }

        private void SearchBrandButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Marka arama penceresi açılacak.");
        }

        private void SearchTopGroupButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Üst ürün grubu arama penceresi açılacak.");
        }

        private void SearchSubGroupButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Alt ürün grubu arama penceresi açılacak.");
        }

        private void SearchSpecialGroupButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ürün grubu özel arama penceresi açılacak.");
        }

        private void SearchCountryButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ülke arama penceresi açılacak.");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}