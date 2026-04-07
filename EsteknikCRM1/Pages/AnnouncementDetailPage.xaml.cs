using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EsteknikCRM1.Pages
{
    public partial class AnnouncementDetailPage : Page
    {
        private readonly RecordModel _selectedAnnouncement;
        private readonly List<RecordModel> _allAnnouncements;

        public AnnouncementDetailPage(RecordModel selectedAnnouncement, List<RecordModel> allAnnouncements)
        {
            InitializeComponent();
            _selectedAnnouncement = selectedAnnouncement;
            _allAnnouncements = allAnnouncements ?? new List<RecordModel>();

            LoadRightPanel();
            LoadLeftPanel();
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }

    public class AnnouncementFileViewModel
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileUrl { get; set; }
    }
}