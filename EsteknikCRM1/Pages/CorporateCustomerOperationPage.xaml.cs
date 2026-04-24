using EsteknikCRM1.Models;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class CorporateCustomerOperationPage : Page
    {
        UserModel currentUser;
        public CorporateCustomerOperationPage(UserModel user)
        {
            currentUser = user;
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
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(currentUser));
        }

        private void Cards_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new CardsPage());
        }

        private void CorporateCustomers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CorporateCustomerCardsPage(currentUser));
        }

        private void CorporateCustomersOperation_Click(object sender, RoutedEventArgs e)
        {
            // şu an bulunduğun sayfa → boş
        }
    }
}