using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class CorporateCustomerOperationPage : Page
    {
        public CorporateCustomerOperationPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kaydet işlemi çalışacak.");
        }

        private void SelectFilesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dosya seçme işlemi çalışacak.");
        }
    }
}