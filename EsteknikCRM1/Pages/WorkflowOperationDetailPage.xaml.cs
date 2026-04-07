using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Popups;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;
using EsteknikCRM1.Models;
using EsteknikCRM1.Services;

namespace EsteknikCRM1.Pages
{
    public partial class WorkflowOperationDetailPage : Page
    {
        private readonly WorkflowModel _workflow;
        public UserModel _loggedUser { get; set; }
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
            if(_loggedUser.UserRole != "admin"){
                AssignButton.Visibility = Visibility.Collapsed;
            }
        }
        private async void LoadWorkflowDetails()
        {
            if (_workflow == null)
                return;
            var device = await AppServices.DeviceService.GetDeviceByIdAsync(_workflow.DeviceId);

            //var device = await FirebaseService.Instance.GetDeviceByIdAsync(_workflow.DeviceId);

            if (device != null)
            {
                DeviceSerialText.Text = device.SerialNumber;
                ProductGroupText.Text = device.DeviceName;
                BrandText.Text = device.Brand;
                MainProductGroupText.Text = device.TopGroup;

                CommissionDateText.Text = device.CommissionDate?.ToString("dd.MM.yyyy") ?? "-";

            }
            // örnek
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
                var users = await AppServices.AuthService.GetUsersAsync();
               // var users = await FirebaseService.Instance.GetUsersAsync();
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
        private void PutOnHoldButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Beklemeye alma işlemi çalışacak.");
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
                await AppServices.WorkflowService.UpdateWorkflowTeamAsync(_workflow.Id, selectedTeam);
                await AppServices.WorkflowService.UpdateWorkflowStatusAsync(_workflow.Id, "Yönlendirildi");
                //await FirebaseService.Instance.UpdateWorkflowTeamAsync(_workflow.Id, selectedTeam);
                //await FirebaseService.Instance.UpdateWorkflowStatusAsync(_workflow.Id, "Yönlendirildi");

                // İstersen geri dön
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
                //await FirebaseService.Instance.UpdateWorkflowStatusAsync(_workflow.Id, "Reddedildi");
                await AppServices.WorkflowService.UpdateWorkflowStatusAsync(_workflow.Id, "Reddedildi");
                MessageBox.Show("İş akışı reddedildi.");

                // UI güncelle
                _workflow.WorkflowStatus = "Reddedildi";

                // İstersen geri dön
                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
    }
}