using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EsteknikCRM1
{
    public partial class HomeContentPage : Page
    {
        private List<RecordModel> _allRecords;
        private int _currentPage = 1;
        private int _itemsPerPage = 5;

        public HomeContentPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _allRecords = new List<RecordModel>();

            for (int i = 1; i <= 50; i++)
            {
                _allRecords.Add(new RecordModel
                {
                    Subject = $"Bilgilendirme Kaydı {i}"
                });
            }

            LoadPage();
        }

        // PAGINATION
        private void LoadPage()
        {
            var filtered = ApplyFilter();
            var pageData = filtered
                .Skip((_currentPage - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            RecordsGrid.ItemsSource = pageData;

            GeneratePagination(filtered.Count);
        }

        private void GeneratePagination(int totalItems)
        {
            PaginationPanel.Children.Clear();

            int totalPages = (int)Math.Ceiling((double)totalItems / _itemsPerPage);

            int maxVisiblePages = 5; // Aynı anda kaç sayfa görünsün
            int startPage = Math.Max(1, _currentPage - 2);
            int endPage = Math.Min(totalPages, startPage + maxVisiblePages - 1);

            // DÜZELTME (son sayfada kaymayı engelle)
            if (endPage - startPage < maxVisiblePages - 1)
                startPage = Math.Max(1, endPage - maxVisiblePages + 1);

            // SOL OK
            if (_currentPage > 1)
            {
                Button prevBtn = CreateNavButton("‹");
                prevBtn.Click += (s, e) =>
                {
                    _currentPage--;
                    LoadPage();
                };
                PaginationPanel.Children.Add(prevBtn);
            }

            // İLK SAYFA + ...
            if (startPage > 1)
            {
                PaginationPanel.Children.Add(CreatePageButton(1));

                if (startPage > 2)
                {
                    PaginationPanel.Children.Add(CreateDots());
                }
            }

            // ORTA SAYFALAR
            for (int i = startPage; i <= endPage; i++)
            {
                PaginationPanel.Children.Add(CreatePageButton(i));
            }

            // SON SAYFA + ...
            if (endPage < totalPages)
            {
                if (endPage < totalPages - 1)
                {
                    PaginationPanel.Children.Add(CreateDots());
                }

                PaginationPanel.Children.Add(CreatePageButton(totalPages));
            }

            // SAĞ OK
            if (_currentPage < totalPages)
            {
                Button nextBtn = CreateNavButton("›");
                nextBtn.Click += (s, e) =>
                {
                    _currentPage++;
                    LoadPage();
                };
                PaginationPanel.Children.Add(nextBtn);
            }
        }
        private Button CreatePageButton(int pageNumber)
        {
            Button btn = new Button
            {
                Content = pageNumber.ToString(),
                Width = 35,
                Height = 35,
                Margin = new Thickness(4),
                BorderThickness = new Thickness(0),
                Cursor = Cursors.Hand
            };

            if (pageNumber == _currentPage)
            {
                btn.Background = Brushes.Blue;
                btn.Foreground = Brushes.White;
            }
            else
            {
                btn.Background = Brushes.White;
            }

            btn.Click += (s, e) =>
            {
                _currentPage = pageNumber;
                LoadPage();
            };

            return btn;
        }

        private Button CreateNavButton(string symbol)
        {
            return new Button
            {
                Content = symbol,
                Width = 35,
                Height = 35,
                Margin = new Thickness(4),
                BorderThickness = new Thickness(0),
                Background = Brushes.White,
                Cursor = Cursors.Hand
            };
        }

        private TextBlock CreateDots()
        {
            return new TextBlock
            {
                Text = "...",
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(8, 0, 8, 0),
                FontSize = 14
            };
        }


        // SEARCH
        private List<RecordModel> ApplyFilter()
        {
            if (_allRecords == null)
                return new List<RecordModel>();

            string search = SearchBox.Text?.ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(search))
                return _allRecords;

            return _allRecords
                .Where(x => x.Subject.ToLower().Contains(search))
                .ToList();
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentPage = 1;
            LoadPage();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (RecordsGrid.SelectedItem is RecordModel selected)
            {
                MessageBox.Show(selected.Subject);
            }
        }
    }
}
