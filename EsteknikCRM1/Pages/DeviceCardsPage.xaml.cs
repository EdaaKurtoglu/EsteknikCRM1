using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace EsteknikCRM1.Pages
{
    /// <summary>
    /// Interaction logic for DeviceCardsPage.xaml
    /// </summary>
    public partial class DeviceCardsPage : Page
    {
        private List<DeviceModel> _allDevices = new List<DeviceModel>();
        private List<DeviceModel> _filteredDevices = new List<DeviceModel>();

        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPages = 1;
        public DeviceCardsPage()
        {
            InitializeComponent();
            Loaded += DeviceCardsPage_Loaded;
        }
       

        private async Task LoadDevicesAsync()
        {
            try
            {
                var devices = await FirebaseService.Instance.GetDevicesAsync();

                _allDevices = devices;
                _filteredDevices = devices;

                _currentPage = 1;
                RefreshPagedGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cihazlar yüklenemedi: " + ex.Message);
            }
        }
        private void RefreshPagedGrid()
        {
            if (_filteredDevices == null)
                _filteredDevices = new List<DeviceModel>();

            _totalPages = (int)Math.Ceiling((double)_filteredDevices.Count / _pageSize);

            if (_totalPages == 0)
                _totalPages = 1;

            if (_currentPage < 1)
                _currentPage = 1;

            if (_currentPage > _totalPages)
                _currentPage = _totalPages;

            var pagedData = _filteredDevices
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            DeviceGrid.ItemsSource = pagedData;

            BuildPaginationButtons();
        }

        private void BuildPaginationButtons()
        {
            PaginationPanel.Children.Clear();

            Button prevButton = new Button
            {
                Content = "‹",
                Width = 34,
                Height = 34,
                Margin = new Thickness(5, 0, 5, 0),
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Foreground = Brushes.Gray,
                Cursor = Cursors.Hand,
                IsEnabled = _currentPage > 1
            };
            prevButton.Click += (s, e) =>
            {
                _currentPage--;
                RefreshPagedGrid();
            };
            PaginationPanel.Children.Add(prevButton);

            int startPage = Math.Max(1, _currentPage - 1);
            int endPage = Math.Min(_totalPages, _currentPage + 1);

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
                        FontSize = 16
                    });
                }
            }

            for (int i = startPage; i <= endPage; i++)
            {
                PaginationPanel.Children.Add(CreatePageButton(i));
            }

            if (endPage < _totalPages)
            {
                if (endPage < _totalPages - 1)
                {
                    PaginationPanel.Children.Add(new TextBlock
                    {
                        Text = "...",
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(8, 0, 8, 0),
                        FontSize = 16
                    });
                }

                PaginationPanel.Children.Add(CreatePageButton(_totalPages));
            }

            Button nextButton = new Button
            {
                Content = "›",
                Width = 34,
                Height = 34,
                Margin = new Thickness(5, 0, 5, 0),
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Foreground = Brushes.Gray,
                Cursor = Cursors.Hand,
                IsEnabled = _currentPage < _totalPages
            };
            nextButton.Click += (s, e) =>
            {
                _currentPage++;
                RefreshPagedGrid();
            };
            PaginationPanel.Children.Add(nextButton);
        }
        private Button CreatePageButton(int pageNumber)
        {
            Button button = new Button
            {
                Content = pageNumber.ToString(),
                Width = 38,
                Height = 38,
                Margin = new Thickness(5, 0, 5, 0),
                Cursor = Cursors.Hand,
                BorderThickness = new Thickness(0),
                FontSize = 16
            };

            if (pageNumber == _currentPage)
            {
                button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#005A9E"));
                button.Foreground = Brushes.White;
            }
            else
            {
                button.Background = Brushes.Transparent;
                button.Foreground = Brushes.Black;
            }

            button.Click += (s, e) =>
            {
                _currentPage = pageNumber;
                RefreshPagedGrid();
            };

            return button;
        }

        private async void DeviceCardsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDevicesAsync();
        }
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DeviceModel selectedDevice = btn?.DataContext as DeviceModel;

            if (selectedDevice != null)
            {
                MessageBox.Show("Seçilen cihaz: " + selectedDevice.DeviceName);
            }
        }
        /*private async Task LoadDevicesAsync()
        {
            try
            {
                var devices = await FirebaseService.Instance.GetDevicesAsync();

                _allDevices = devices;
                DeviceGrid.ItemsSource = devices;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cihazlar yüklenemedi: " + ex.Message);
            }
        }*/
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void AddNewDevice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DeviceCardOperationPage());
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchTextBox.Text?.Trim().ToLower() ?? "";

            _filteredDevices = _allDevices
                .Where(x =>
                    (x.Id ?? "").ToLower().Contains(search) ||
                    (x.SerialNumber ?? "").ToLower().Contains(search) ||
                    (x.DeviceCode ?? "").ToLower().Contains(search) ||
                    (x.DeviceName ?? "").ToLower().Contains(search) ||
                    (x.Brand ?? "").ToLower().Contains(search) ||
                    (x.TopGroup ?? "").ToLower().Contains(search) ||
                    (x.SubGroup ?? "").ToLower().Contains(search) ||
                    (x.SpecialGroup ?? "").ToLower().Contains(search))
                .ToList();

            _currentPage = 1;
            RefreshPagedGrid();
        }

        private void ApplyFilter()
        {
            string searchText = SearchTextBox.Text.ToLower();

            var view = CollectionViewSource.GetDefaultView(DeviceGrid.ItemsSource);
            view.Filter = item =>
            {
                var device = item as Models.DeviceModel;

                return device.SerialNumber.ToLower().Contains(searchText)
                    || device.DeviceName.ToLower().Contains(searchText)
                    || device.Brand.ToLower().Contains(searchText);
            };
        }

       /* private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var row = (sender as Button).DataContext as Models.DeviceModel;
            MessageBox.Show("Selected Device: " + row?.DeviceName);
        }*/

    }
}
