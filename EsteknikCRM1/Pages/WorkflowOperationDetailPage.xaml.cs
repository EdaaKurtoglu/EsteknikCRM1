using EsteknikCRM1.Popups;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using Microsoft.Win32;
using System.Collections.Generic;

namespace EsteknikCRM1.Pages
{
    public partial class WorkflowOperationDetailPage : Page
    {
        private readonly WorkflowModel _workflow;
        public UserModel _loggedUser { get; set; }
        private readonly List<string> _selectedWorkflowFiles = new List<string>();

        public WorkflowOperationDetailPage(WorkflowModel workflow, UserModel currentUser)
        {
            InitializeComponent();
            _workflow = workflow;
            _loggedUser = currentUser;
            userControl();
            LoadWorkflowDetails();
            Loaded += WorkflowOperationDetailPage_Loaded;
        }

        private void userControl()
        {
            if (_loggedUser != null && _loggedUser.UserRole != "admin")
            {
                AssignButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void LoadWorkflowDetails()
        {
            if (_workflow == null)
                return;

            var device = await AppServices.ApiDeviceService.GetDeviceByIdAsync(_workflow.DeviceId);

            if (device != null)
            {
                DeviceSerialText.Text = string.IsNullOrWhiteSpace(device.SerialNumber) ? "-" : device.SerialNumber;
                ProductGroupText.Text = string.IsNullOrWhiteSpace(device.DeviceName) ? "-" : device.DeviceName;
                BrandText.Text = string.IsNullOrWhiteSpace(device.Brand) ? "-" : device.Brand;
                MainProductGroupText.Text = string.IsNullOrWhiteSpace(device.TopGroup) ? "-" : device.TopGroup;
                CommissionDateText.Text = device.CommissionDate?.ToString("dd.MM.yyyy") ?? "-";
            }
            else
            {
                DeviceSerialText.Text = "-";
                ProductGroupText.Text = "-";
                BrandText.Text = "-";
                MainProductGroupText.Text = "-";
                CommissionDateText.Text = "-";
            }

            CategoryText.Text = _workflow.CategoryName ?? "-";
            NotificationTypeText.Text = _workflow.NotificationTypeName ?? "-";
            SubCategoryText.Text = _workflow.SubCategoryName ?? "-";
            status.Text = _workflow.WorkflowStatus ?? "-";
            CustomerNameText.Text = _workflow.CustomerFullName ?? "-";
            CustomerAddressText.Text = _workflow.AddressLine ?? "-";
            CustomerPhoneText.Text = _workflow.CustomerPhone ?? "-";

            AppointmentStartText.Text = _workflow.CreatedDate.ToString("dd/MM/yyyy HH:mm");
            PreferredAppointmentText.Text = _workflow.CreatedDate.ToString("dd/MM/yyyy HH:mm");
        }

        private async void WorkflowOperationDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await AppServices.ApiAuthService.GetUsersAsync();
                UsersItemsControl.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e) { }

        private void CustomerProjectComboBox_SelectionChanged(object sender, RoutedEventArgs e) { }

        private async void PutOnHoldButton_Click(object sender, RoutedEventArgs e)
        {
            await AppServices.ApiWorkflowService.UpdateWorkflowStatusAsync(_workflow.Id, "Beklemeye Alındı");
        }

        private async void AssignTechnicalTeamButton_Click(object sender, RoutedEventArgs e)
        {
            var popup = new AssignTechnicalTeamWindow
            {
                Owner = Window.GetWindow(this)
            };

            if (popup.ShowDialog() == true)
            {
                string selectedTeam = popup.SelectedTeamName;
                MessageBox.Show("Seçilen takım: " + selectedTeam);

                await AppServices.ApiWorkflowService.UpdateWorkflowTeamAsync(_workflow.Id, selectedTeam);
                await AppServices.ApiWorkflowService.UpdateWorkflowStatusAsync(_workflow.Id, "Yönlendirildi");

                NavigationService?.GoBack();
            }
        }

        private async void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (_workflow == null)
            {
                MessageBox.Show("İş akışı bulunamadı.");
                return;
            }

            try
            {
                await AppServices.ApiWorkflowService.UpdateWorkflowStatusAsync(_workflow.Id, "Reddedildi");
                MessageBox.Show("İş akışı reddedildi.");

                _workflow.WorkflowStatus = "Reddedildi";
                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
        private async void SelectWorkflowFiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_workflow.Id))
                {
                    MessageBox.Show("Önce iş akışı kaydı seçilmelidir.");
                    return;
                }

                var dialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Title = "İş akışına ait dosyaları seçin"
                };

                if (dialog.ShowDialog() == true)
                {
                    _selectedWorkflowFiles.Clear();
                    _selectedWorkflowFiles.AddRange(dialog.FileNames);

                    var uploaded = await AppServices.ApiWorkflowFileService
                        .UploadFilesAsync(_workflow.Id, _selectedWorkflowFiles);

                    MessageBox.Show($"{uploaded.Count} dosya başarıyla yüklendi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya yükleme hatası:\n" + ex.Message);
            }
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(_loggedUser));
        }

        private void Workflows_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new WorkflowPage(_loggedUser));
        }

        private void WorkflowOperation_Click(object sender, RoutedEventArgs e)
        {
            // aktif sayfa
        }
    }
}