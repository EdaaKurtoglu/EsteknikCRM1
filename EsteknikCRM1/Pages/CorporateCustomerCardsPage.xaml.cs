using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class CorporateCustomerCardsPage : Page
    {
        public CorporateCustomerCardsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var items = new List<CorporateCustomerItem>();

            CorporateCustomerGrid.ItemsSource = items;

            bool isEmpty = items.Count == 0;
            EmptyStatePanel.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
            EmptyFooterText.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yeni kurumsal müşteri ekleme sayfası açılacak.");
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kurumsal müşteri detay sayfası açılacak.");
        }
    }

    
}