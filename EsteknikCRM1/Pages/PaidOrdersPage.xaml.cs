using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;

namespace EsteknikCRM1.Pages
{
    public partial class PaidOrdersPage : Page
    {
        public PaidOrdersPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var items = new List<PaidOrderItem>();

            PaidOrdersGrid.ItemsSource = items;

            bool isEmpty = items.Count == 0;
            EmptyStatePanel.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
            EmptyFooterText.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    
}