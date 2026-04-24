using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class AnnouncementFilesPage : Page
    {
        private readonly string _announcementId;

        private ObservableCollection<AnnouncementFileModel> _files =
            new ObservableCollection<AnnouncementFileModel>();

        public AnnouncementFilesPage(string announcementId)
        {
            InitializeComponent();
            _announcementId = announcementId;
            FilesGrid.ItemsSource = _files;

            Loaded += AnnouncementFilesPage_Loaded;
        }

        private async void AnnouncementFilesPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _files.Clear();

                var files = await AppServices.ApiAnnouncementService
                    .GetAnnouncementFilesAsync(_announcementId);

                foreach (var file in files)
                    _files.Add(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosyalar yüklenemedi:\n" + ex.Message);
            }
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is Button button))
                    return;

                var file = button.DataContext as AnnouncementFileModel;

                if (file == null)
                    return;

                var dialog = new SaveFileDialog
                {
                    FileName = file.FileName,
                    Title = "Dosyayı kaydet"
                };

                if (dialog.ShowDialog() == true)
                {
                    await AppServices.ApiAnnouncementService
                        .DownloadAnnouncementFileAsync(file.Id, dialog.FileName);

                    MessageBox.Show("Dosya indirildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya indirilemedi:\n" + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}