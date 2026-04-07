using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
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
        private List<string> _selectedFilePaths = new List<string>();

        public AnnouncementAddPage()
        {
            InitializeComponent();
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Tüm Dosyalar|*.*|PDF Dosyaları|*.pdf|Resimler|*.jpg;*.jpeg;*.png"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                _selectedFilePaths = dialog.FileNames.ToList();
                FilesListBox.ItemsSource = null;
                FilesListBox.ItemsSource = _selectedFilePaths.Select(System.IO.Path.GetFileName).ToList();
            }
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
                    BodyText = BodyTextBox.Text?.Trim(),
                    CreatedDate = DateTime.UtcNow
                };

                if (_selectedFilePaths.Count > 0)
                {
                    var uploadResult = await FirebaseService.Instance.UploadAnnouncementFilesAsync(_selectedFilePaths);

                    model.FileNames = uploadResult.fileNames;
                    model.FileUrls = uploadResult.fileUrls;
                    model.FileSizes = uploadResult.fileSizes;
                }

                await FirebaseService.Instance.AddAnnouncementAsync(model);

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
    }
}