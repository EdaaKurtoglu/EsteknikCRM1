using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Linq;
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
                    Id = Guid.NewGuid().ToString(),
                    TeamName = TeamNameBox.Text.Trim(),
                    VehiclePlate = VehiclePlateBox.Text?.Trim() ?? "",
                    ServiceName = "ES İKLİMLENDİRME SAN.TİC.LTD.ŞTİ.",
                    IsActive = true,
                    Status = "Aktif",
                    MemberCount = 1,
                    CreatedDate = DateTime.UtcNow,
                    PassiveDate = null
                };

                var parts = TeamNameBox.Text
                    .Trim()
                    .Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                string name = parts.FirstOrDefault() ?? "";
                string surname = parts.LastOrDefault() ?? "";

                UserModel user = new UserModel
                {
                    Id = team.Id,
                    UserMail = GenerateEmail(TeamNameBox.Text.Trim()),
                    Password = "123",
                    UserRole = "team",
                    Name = name,
                    Surname = surname
                };

                await AppServices.ApiAuthService.AddUserAsync(user);

                string newTeamId = await AppServices.ApiTeamService.AddTeamAsync(team);

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

        private string GenerateEmail(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "";

            string clean = fullName
                .ToLower()
                .Replace(" ", "")
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c");

            return clean + "@bosch.com";
        }
    }
}