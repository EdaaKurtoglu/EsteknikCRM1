using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Popups
{
    public partial class AssignTechnicalTeamWindow : Window
    {
        public string SelectedTeamName { get; private set; }

        public AssignTechnicalTeamWindow()
        {
            InitializeComponent();
            LoadTeams();
        }

        public AssignTechnicalTeamWindow(List<string> teamNames)
        {
            InitializeComponent();
            LoadTeams(teamNames);
        }

        private async void LoadTeams()
        {
            try
            {
                var teams = await AppServices.TeamService.GetTeamsAsync();
                //var teams = await FirebaseService.Instance.GetTeamsAsync();
                TeamComboBox.Items.Clear();

                TeamComboBox.ItemsSource = teams;
                TeamComboBox.DisplayMemberPath = "TeamName";
                TeamComboBox.SelectedValuePath = "TeamId";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Takımlar yüklenemedi: " + ex.Message);
            }

        }

        private void LoadTeams(List<string> teamNames)
        {
            TeamComboBox.Items.Clear();
            TeamComboBox.Items.Add(new ComboBoxItem { Content = "Lütfen seçiniz..." });

            if (teamNames != null)
            {
                foreach (var team in teamNames)
                {
                    TeamComboBox.Items.Add(new ComboBoxItem { Content = team });
                }
            }

            TeamComboBox.SelectedIndex = 0;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        public string SelectedTeamId { get; private set; }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen bir takım seçiniz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (TeamComboBox.SelectedItem is TeamItem selectedTeam)
            {
                SelectedTeamName = selectedTeam.TeamName;
                SelectedTeamId = selectedTeam.TeamId;
            }

            DialogResult = true;
            Close();
        }
    }
}