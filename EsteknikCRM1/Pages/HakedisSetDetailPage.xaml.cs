using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisSetDetailPage : Page
    {
        private readonly HakedisSetModel _set;

        public HakedisSetDetailPage(HakedisSetModel set)
        {
            InitializeComponent();
            _set = set;
            Loaded += HakedisSetDetailPage_Loaded;
        }

        private async void HakedisSetDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TitleText.Text = $"Hakediş Set Detayı - {_set.SetDate}";

                var operations = await AppServices.ApiOperationService
                    .GetOperationsByHakedisSetIdAsync(_set.Id);

                OperationsGrid.ItemsSource = operations;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hakediş detayları yüklenemedi:\n" + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}