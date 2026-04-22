using EsteknikCRM1.Models;
using EsteknikCRM1.Pages;
using EsteknikCRM1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1
{
    public partial class HomeContentPage : Page
    {
        private List<RecordModel> _allRecords = new List<RecordModel>();
        private int _currentPage = 1;
        private const int _itemsPerPage = 5;
        private bool _isLoadingPage = false;
        private UserModel _user;

        public HomeContentPage(UserModel currentUser)
        {
            InitializeComponent();
            Loaded += HomeContentPage_Loaded;
            _user = currentUser;
        }

        private async void HomeContentPage_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= HomeContentPage_Loaded;
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            if(_user.UserRole != "admin")AddAnnouncementButton.Visibility = Visibility.Collapsed;
            try
            {
                var data = await AppServices.ApiAnnouncementService.GetAnnouncementsAsync();
                _allRecords = data ?? new List<RecordModel>();

                _currentPage = 1;
                RefreshGridAndPagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private List<RecordModel> GetFilteredRecords()
        {
            string search = SearchBox.Text == null ? "" : SearchBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(search))
                return _allRecords;

            return _allRecords
                .Where(x =>
                    (x.Subject ?? "").ToLower().Contains(search) ||
                    (x.BodyText ?? "").ToLower().Contains(search) ||
                    (x.DateText ?? "").ToLower().Contains(search) ||
                    (x.GroupText ?? "").ToLower().Contains(search))
                .ToList();
        }

        private void RefreshGridAndPagination()
        {
            if (_isLoadingPage)
                return;

            _isLoadingPage = true;

            try
            {
                var filtered = GetFilteredRecords();
                int totalItems = filtered.Count;
                int totalPages = totalItems == 0 ? 1 : (int)Math.Ceiling((double)totalItems / _itemsPerPage);

                if (_currentPage > totalPages)
                    _currentPage = totalPages;

                if (_currentPage < 1)
                    _currentPage = 1;

                var pageData = filtered
                    .Skip((_currentPage - 1) * _itemsPerPage)
                    .Take(_itemsPerPage)
                    .ToList();

                RecordsGrid.ItemsSource = pageData;
                BuildPagination(totalPages);
            }
            finally
            {
                _isLoadingPage = false;
            }
        }

        private void BuildPagination(int totalPages)
        {
            PaginationPanel.Children.Clear();

            if (totalPages <= 1)
                return;

            Button prevButton = new Button
            {
                Content = "‹",
                Style = (Style)FindResource("NavPaginationButtonStyle"),
                IsEnabled = _currentPage > 1
            };
            prevButton.Click += (s, e) =>
            {
                if (_currentPage > 1)
                {
                    _currentPage--;
                    RefreshGridAndPagination();
                }
            };
            PaginationPanel.Children.Add(prevButton);

            int maxVisiblePages = 5;
            int startPage = Math.Max(1, _currentPage - 2);
            int endPage = Math.Min(totalPages, startPage + maxVisiblePages - 1);

            if (endPage - startPage < maxVisiblePages - 1)
                startPage = Math.Max(1, endPage - maxVisiblePages + 1);

            if (startPage > 1)
            {
                PaginationPanel.Children.Add(CreatePageButton(1));

                if (startPage > 2)
                {
                    PaginationPanel.Children.Add(new TextBlock
                    {
                        Text = "...",
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(8, 0, 8, 0),
                        FontSize = 15
                    });
                }
            }

            for (int i = startPage; i <= endPage; i++)
            {
                PaginationPanel.Children.Add(CreatePageButton(i));
            }

            if (endPage < totalPages)
            {
                if (endPage < totalPages - 1)
                {
                    PaginationPanel.Children.Add(new TextBlock
                    {
                        Text = "...",
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(8, 0, 8, 0),
                        FontSize = 15
                    });
                }

                PaginationPanel.Children.Add(CreatePageButton(totalPages));
            }

            Button nextButton = new Button
            {
                Content = "›",
                Style = (Style)FindResource("NavPaginationButtonStyle"),
                IsEnabled = _currentPage < totalPages
            };
            nextButton.Click += (s, e) =>
            {
                if (_currentPage < totalPages)
                {
                    _currentPage++;
                    RefreshGridAndPagination();
                }
            };
            PaginationPanel.Children.Add(nextButton);
        }

        private Button CreatePageButton(int pageNumber)
        {
            Button button = new Button
            {
                Content = pageNumber.ToString(),
                Style = pageNumber == _currentPage
                    ? (Style)FindResource("ActivePaginationButtonStyle")
                    : (Style)FindResource("PaginationButtonStyle")
            };

            button.Click += (s, e) =>
            {
                if (_currentPage != pageNumber)
                {
                    _currentPage = pageNumber;
                    RefreshGridAndPagination();
                }
            };

            return button;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentPage = 1;
            RefreshGridAndPagination();
        }

        private void AddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AnnouncementAddPage());
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            RecordModel selectedAnnouncement = btn?.DataContext as RecordModel;

            if (selectedAnnouncement == null)
            {
                MessageBox.Show("Duyuru bulunamadı.");
                return;
            }

            NavigationService?.Navigate(new AnnouncementDetailPage(selectedAnnouncement, _allRecords));
        }
    }
}