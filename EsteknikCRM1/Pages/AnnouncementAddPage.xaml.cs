using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class AnnouncementAddPage : Page
    {
        private List<string> _selectedFiles = new List<string>();
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

                string announcementId = await AppServices.ApiAnnouncementService.AddAnnouncementAsync(model);
                if (_selectedFiles.Any())
                {
                    await AppServices.ApiAnnouncementService.UploadFilesAsync(announcementId, _selectedFiles);
                }
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
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Dosya seç"
            };

            if (dialog.ShowDialog() == true)
            {
                _selectedFiles.Clear();
                _selectedFiles.AddRange(dialog.FileNames);

                MessageBox.Show($"{_selectedFiles.Count} dosya seçildi.");
            }
        }
    }
}