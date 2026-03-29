using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class TeamDefinitionsOperationPage : Page
    {
        public TeamDefinitionsOperationPage()
        {
            InitializeComponent();

            CreatedDateBox.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm zzz");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TeamNameBox.Text))
                {
                    MessageBox.Show("Ekip adı zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                TeamItem team = new TeamItem
                {
                    TeamName = TeamNameBox.Text.Trim(),
                    VehiclePlate = VehiclePlateBox.Text?.Trim(),

                    IsActive = true,
                    Status = "Aktif",

                    // Firestore için gerçek kayıt zamanı
                    CreatedDate = DateTime.Now,
                    PassiveDate = null
                };

                string newTeamId = await FirebaseService.Instance.AddTeamAsync(team);

                MessageBox.Show(
                    "Takım başarıyla kaydedildi.\nKayıt ID: " + newTeamId,
                    "Başarılı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kayıt sırasında hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}