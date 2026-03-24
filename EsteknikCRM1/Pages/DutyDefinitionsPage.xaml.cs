using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class DutyDefinitionsPage : Page
    {
        public DutyDefinitionsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var items = new List<DutyDefinitionItem>();

            DutyGrid.ItemsSource = items;

            bool isEmpty = items.Count == 0;
            EmptyStatePanel.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
            EmptyFooterText.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Listeleme yapılacak.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    
}