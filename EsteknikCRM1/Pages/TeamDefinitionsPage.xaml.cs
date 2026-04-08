using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using EsteknikCRM1.Services;

namespace EsteknikCRM1.Pages
{
    public partial class TeamDefinitionsPage : Page
    {
        public TeamDefinitionsPage()
        {
            InitializeComponent();
            Loaded += TeamDefinitionsPage_Loaded;
        }

        private async void TeamDefinitionsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                //var teams = await AppServices.TeamService.GetTeamsAsync();
                var teams = await AppServices.ApiTeamService.GetTeamsAsync();
                TeamGrid.ItemsSource = teams;
                //var teams = await FirebaseService.Instance.GetTeamsAsync();
                //TeamGrid.ItemsSource = teams;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Takımlar yüklenirken hata oluştu:\n" + ex.Message,
                                "Hata",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new TeamDefinitionsOperationPage());
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Takım detayına gidilecek");
        }
    }

    
}