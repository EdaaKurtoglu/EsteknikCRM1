using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class TeamDefinitionsPage : Page
    {
        public TeamDefinitionsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            TeamGrid.ItemsSource = new List<TeamItem>
            {
                new TeamItem
                {
                    Id = 959,
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON...",
                    TeamName = "BEKİR ÖZKESEK",
                    MemberCount = 0,
                    MemberName = "",
                    Role = "",
                    Status = "Active"
                },
                new TeamItem
                {
                    Id = 958,
                    ServiceName = "ES İKLİMLENDİRME VE OTOMASYON...",
                    TeamName = "İNANIR İLHAN",
                    MemberCount = 1,
                    MemberName = "İnanır İLHAN",
                    Role = "Technician",
                    Status = "Active"
                }
            };
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ServiceTeamDefinitionsOperationPage());
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Takım detayına gidilecek");
        }
    }

    
}