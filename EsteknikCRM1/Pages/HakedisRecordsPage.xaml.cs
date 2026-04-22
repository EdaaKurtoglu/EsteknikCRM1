using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisRecordsPage : Page
    {
        public HakedisRecordsPage()
        {
            InitializeComponent();
            Loaded += HakedisRecordsPage_Loaded;
        }

        private async void HakedisRecordsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadSetsAsync();
        }

        private async Task LoadSetsAsync()
        {
            try
            {
                var sets = await AppServices.ApiHakedisSetService.GetHakedisSetsAsync();
                HakedisGrid.ItemsSource = sets;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hakediş setleri yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void AddNewRecord_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HakedisRecordsOperationPage());
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}