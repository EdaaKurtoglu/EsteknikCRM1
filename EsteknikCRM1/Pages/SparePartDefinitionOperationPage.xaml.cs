using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class SparePartDefinitionOperationPage : Page
    {
        public SparePartDefinitionOperationPage()
        {
            InitializeComponent();
        }

        private void SearchBCodeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("B Kodu arama penceresi açılacak.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yeni yedek parça kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}