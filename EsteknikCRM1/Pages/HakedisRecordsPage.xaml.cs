using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;
using static Google.LongRunning.Operations;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisRecordsPage : Page
    {
        UserModel _user;
        public HakedisRecordsPage(UserModel user)
        {
            _user = user;
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
            NavigationService?.Navigate(new HakedisRecordsOperationPage(_user));
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is HakedisSetModel selectedSet)
            {
                NavigationService?.Navigate(new HakedisSetDetailPage(selectedSet));
            }
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(_user));
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService?.Navigate(new OperationsPage());
        }

        private void HakedisRecords_Click(object sender, RoutedEventArgs e)
        {
            // bulunduğun sayfa
        }
    }
}