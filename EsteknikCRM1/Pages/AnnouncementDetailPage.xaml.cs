using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using Grpc.Net.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EsteknikCRM1.Pages
{
    public partial class AnnouncementDetailPage : Page
    {
        private readonly RecordModel _selectedAnnouncement;
        private readonly List<RecordModel> _allAnnouncements;
        UserModel currentUser;

        public AnnouncementDetailPage(RecordModel selectedAnnouncement, List<RecordModel> allAnnouncements, UserModel user)
        {
            InitializeComponent();
            currentUser = user;
            _selectedAnnouncement = selectedAnnouncement;
            _allAnnouncements = allAnnouncements ?? new List<RecordModel>();

            LoadRightPanel();
            LoadLeftPanel();
            Load();
        }
        private async void Load()
        {
            await LoadAnnouncementFilesAsync(_selectedAnnouncement.Id);
        }
        private void LoadRightPanel()
        {
            if (_selectedAnnouncement == null)
                return;

            AnnouncementDateText.Text = _selectedAnnouncement.DateText;
            AnnouncementTitleText.Text = _selectedAnnouncement.Subject;
            AnnouncementBodyText.Text = _selectedAnnouncement.BodyText;

            var files = new List<AnnouncementFileViewModel>();

            for (int i = 0; i < _selectedAnnouncement.FileNames.Count; i++)
            {
                files.Add(new AnnouncementFileViewModel
                {
                    FileName = "📄 " + _selectedAnnouncement.FileNames[i],
                    FileSize = i < _selectedAnnouncement.FileSizes.Count ? _selectedAnnouncement.FileSizes[i] : "-",
                    FileUrl = i < _selectedAnnouncement.FileUrls.Count ? _selectedAnnouncement.FileUrls[i] : ""
                });
            }

            FilesItemsControl.ItemsSource = files;
        }

        private void LoadLeftPanel()
        {
            AnnouncementListPanel.Children.Clear();

            string currentGroup = "";

            foreach (var item in _allAnnouncements)
            {
                if (item.GroupText != currentGroup)
                {
                    currentGroup = item.GroupText;

                    Border groupHeader = new Border
                    {
                        Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#D9D9D9"),
                        Height = 36,
                        Child = new TextBlock
                        {
                            Text = "› " + currentGroup,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(10, 0, 0, 0),
                            FontSize = 16,
                            FontWeight = FontWeights.SemiBold
                        }
                    };

                    AnnouncementListPanel.Children.Add(groupHeader);
                }

                Border itemBorder = new Border
                {
                    BorderBrush = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFrom("#D9D9D9"),
                    BorderThickness = new Thickness(1, 0, 1, 1),
                    Background = System.Windows.Media.Brushes.White,
                    Padding = new Thickness(8),
                    Child = CreateAnnouncementListItem(item)
                };

                AnnouncementListPanel.Children.Add(itemBorder);
            }
        }

        private UIElement CreateAnnouncementListItem(RecordModel item)
        {
            StackPanel panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "📢",
                FontSize = 24,
                Margin = new Thickness(0, 0, 0, 4)
            });

            panel.Children.Add(new TextBlock
            {
                Text = item.DateText,
                FontSize = 16
            });

            panel.Children.Add(new TextBlock
            {
                Text = item.Subject,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap
            });

            return panel;
        }
        private async Task LoadAnnouncementFilesAsync(string announcementId)
        {
            try
            {
                var files = await AppServices.ApiAnnouncementService
                    .GetAnnouncementFilesAsync(announcementId);

                FilesItemsControl.ItemsSource = files;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duyuru dosyaları yüklenemedi:\n" + ex.Message);
            }
        }
        private void DownloadFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            string url = tb?.Tag?.ToString();

            if (string.IsNullOrWhiteSpace(url))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        private async void DownloadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is Button button))
                    return;

                var file = button.DataContext as AnnouncementFileModel;

                if (file == null)
                    return;

                var dialog = new Microsoft.Win32.SaveFileDialog
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
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(currentUser));
        }

        private void Announcements_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(currentUser)); // liste sayfan burasıysa
        }

        private void CurrentPage_Click(object sender, RoutedEventArgs e)
        {
            // zaten bu sayfadasın → boş bırakabilirsin
        }
    }

    public class AnnouncementFileViewModel
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileUrl { get; set; }
    }
}