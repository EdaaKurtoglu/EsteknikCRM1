using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class ServiceTeamDefinitionsDetailPage : Page
    {
        public ServiceTeamDefinitionsDetailPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var items = new List<TeamMemberItem>();

            TeamMemberGrid.ItemsSource = items;

            bool isEmpty = items.Count == 0;
            EmptyStatePanel.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
            EmptyFooterText.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Takım bilgileri kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    
}