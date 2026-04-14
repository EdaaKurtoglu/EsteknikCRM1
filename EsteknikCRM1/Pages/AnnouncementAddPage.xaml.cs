using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class AnnouncementAddPage : Page
    {
        public AnnouncementAddPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SubjectTextBox.Text))
                {
                    MessageBox.Show("Başlık zorunludur.");
                    return;
                }

                RecordModel model = new RecordModel
                {
                    Subject = SubjectTextBox.Text.Trim(),
                    BodyText = BodyTextBox.Text?.Trim() ?? "",
                    CreatedDate = DateTime.UtcNow,
                    DateText = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm"),

                };

                await AppServices.ApiAnnouncementService.AddAnnouncementAsync(model);

                MessageBox.Show("Duyuru başarıyla kaydedildi.");
                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında hata oluştu:\n" + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dosya yükleme özelliğini ikinci aşamada ekleyeceğiz.");
        }
    }
}