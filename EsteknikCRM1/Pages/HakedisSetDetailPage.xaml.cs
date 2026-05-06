using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisSetDetailPage : Page
    {
        private readonly HakedisSetModel _set;
        private readonly UserModel _user;
        private ObservableCollection<WorkflowTeamOperationSaveModel> _operations;

        public HakedisSetDetailPage(HakedisSetModel set, UserModel currentUser)
        {
            InitializeComponent();
            _set = set;
            _user = currentUser;
            Loaded += HakedisSetDetailPage_Loaded;
        }

        private async void HakedisSetDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isAdmin = _user.UserRole?.ToLower() == "admin";

                OperationColumn.Visibility = isAdmin
                    ? Visibility.Visible
                    : Visibility.Collapsed;

                TitleText.Text = $"Hakediş Set Detayı - {_set.SetDate}";

                var operations = await AppServices.ApiOperationService
                    .GetOperationsByHakedisSetIdAsync(_set.Id);

                _operations = new ObservableCollection<WorkflowTeamOperationSaveModel>(operations);

                OperationsGrid.ItemsSource = _operations;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hakediş detayları yüklenemedi:\n" + ex.Message);
            }
        }

        private async void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is WorkflowTeamOperationSaveModel item)
            {
                await UpdateApproval(item.Id, 1);

                item.ApprovalStatus = 1;

                await UpdateSetApproveDateIfCompletedAsync();

                OperationsGrid.Items.Refresh();
            }
        }

        private async void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is WorkflowTeamOperationSaveModel item)
            {
                await UpdateApproval(item.Id, 2);

                item.ApprovalStatus = 2;

                await UpdateSetApproveDateIfCompletedAsync();

                OperationsGrid.Items.Refresh();
            }
        }

        private async Task UpdateApproval(string operationId, int status)
        {
            try
            {
                await AppServices.ApiOperationService.UpdateApprovalAsync(operationId, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem onay durumu güncellenemedi:\n" + ex.Message);
            }
        }

        private async Task UpdateSetApproveDateIfCompletedAsync()
        {
            if (_operations == null || _operations.Count == 0)
                return;

            bool hasPendingOperation = _operations.Any(x => x.ApprovalStatus == 0);

            if (hasPendingOperation)
                return;

            await AppServices.ApiHakedisSetService.UpdateSetApproveDateAsync(_set.Id);

            _set.SetApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            MessageBox.Show("Tüm işlemler tamamlandı. Set onay tarihi güncellendi.");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}