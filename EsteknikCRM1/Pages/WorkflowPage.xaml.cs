using EsteknikCRM1.Pages;
using EsteknikCRM1.Models;
using EsteknikCRM1.Popups;
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
using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Services;


namespace EsteknikCRM1
{
    /// <summary>
    /// Interaction logic for WorkflowPage.xaml
    /// </summary>
    public partial class WorkflowPage : Page
    {
        private List<WorkflowModel> _allWorkflowItems = new List<WorkflowModel>();
        private List<WorkflowModel> _filteredWorkflowItems = new List<WorkflowModel>();
        private UserModel _loggedUser;
        
        public WorkflowPage(UserModel loginUser)
        {
            InitializeComponent();
            Loaded += WorkflowPage_Loaded;
            _loggedUser = loginUser;
        }
        private async void WorkflowPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadWorkflowsAsync();
        }

        private async Task LoadWorkflowsAsync()
        {
            AssignedToPersonRadioButton.IsChecked = false;
            WaitingOnMeRadioButton.IsChecked = false;
            LiveFlowRadioButton.IsChecked = false;
            CompletedRadioButton.IsChecked = false;
            RejectedRadioButton.IsChecked = false;
            try
            {
                //var workflows = await AppServices.WorkflowService.GetWorkflowsAsync();
                var workflows = await AppServices.ApiWorkflowService.GetWorkflowsAsync();

                //var workflows = await FirebaseService.Instance.GetWorkflowsAsync();
                var gridItems = new List<WorkflowModel>();

                bool isAdmin = (_loggedUser?.UserRole ?? "")
                    .Equals("admin", StringComparison.OrdinalIgnoreCase);

                string userTeam = (_loggedUser?.FullName ?? "").Trim();

                foreach (var item in workflows)
                {
                    // Admin değilse ve Team kullanıcısıysa sadece kendi takımına ait olanları al
                    if (!isAdmin)
                    {
                        if (string.IsNullOrWhiteSpace(userTeam))
                            continue;

                        if (string.Equals(item.WorkTeam ?? "", userTeam, StringComparison.OrdinalIgnoreCase))
                            continue;
                    }

                    string fullName = "";
                    string phone = "";
                    string customerName = "";
                    string customerSurname = "";

                    if (!string.IsNullOrEmpty(item.CustomerId))
                    {
                        //var customer = await AppServices.CustomerService.GetCustomerByIdAsync(item.CustomerId);
                        var customer = await AppServices.ApiCustomerService.GetCustomerByIdAsync(item.CustomerId);

                        // var customer = await FirebaseService.Instance.GetCustomerByIdAsync(item.CustomerId);

                        if (customer != null)
                        {
                            customerName = customer.Name ?? "";
                            customerSurname = customer.Surname ?? "";
                            fullName = (customerName + " " + customerSurname).Trim();
                            phone = customer.Phone ?? "";
                        }
                    }

                    gridItems.Add(new WorkflowModel
                    {
                        Id = item.Id,
                        WorkflowStatus = item.WorkflowStatus,
                        FlowType = item.FlowType,
                        LastAction = item.NotificationTypeName,
                        Subject = item.Subject,
                        CategoryName = item.CategoryName,
                        SubCategoryName = item.SubCategoryName,
                        NotificationTypeName = item.NotificationTypeName,
                        CustomerId = item.CustomerId,
                        CustomerName = customerName,
                        CustomerSurname = customerSurname,
                        CustomerFullName = fullName,
                        CustomerPhone = phone,
                        WorkTeam = item.WorkTeam,
                        AddressLine = item.AddressLine,
                        DeviceName = item.DeviceName,
                        DeviceId = item.DeviceId,
                        CreatedDate = item.CreatedDate,
                        CreatedByFullName = (item.CreatedByName + " " + item.CreatedBySurname).Trim(),
                        CreatedByRole = item.CreatedByRole,
                        StartType = item.StartType
                    });
                }

                _allWorkflowItems = gridItems;
                _filteredWorkflowItems = gridItems;

                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("İş akışları yüklenirken hata oluştu:\n" + ex.Message);
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void FilterRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (WorkflowGrid == null ||
                SearchTextBox == null ||
                CustomerNameFilterTextBox == null ||
                CustomerSurnameFilterTextBox == null ||
                PhoneFilterTextBox == null)
            {
                return;
            }

            IEnumerable<WorkflowModel> query = _allWorkflowItems;

            string generalSearch = SearchTextBox.Text?.Trim().ToLower() ?? "";
            string customerName = CustomerNameFilterTextBox.Text?.Trim().ToLower() ?? "";
            string customerSurname = CustomerSurnameFilterTextBox.Text?.Trim().ToLower() ?? "";
            string phone = PhoneFilterTextBox.Text?.Trim().ToLower() ?? "";

            if (!string.IsNullOrWhiteSpace(generalSearch))
            {
                query = query.Where(x =>
                    (x.Id ?? "").ToLower().Contains(generalSearch) ||
                    (x.Subject ?? "").ToLower().Contains(generalSearch) ||
                    (x.FlowType ?? "").ToLower().Contains(generalSearch) ||
                    (x.LastAction ?? "").ToLower().Contains(generalSearch) ||
                    (x.CustomerFullName ?? "").ToLower().Contains(generalSearch) ||
                    (x.CustomerPhone ?? "").ToLower().Contains(generalSearch) ||
                    (x.CreatedByFullName ?? "").ToLower().Contains(generalSearch) ||
                    (x.CreatedByRole ?? "").ToLower().Contains(generalSearch) ||
                    (x.StartType ?? "").ToLower().Contains(generalSearch) ||
                    (x.WorkflowStatus ?? "").ToLower().Contains(generalSearch));
            }

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                query = query.Where(x =>
                    (x.CustomerName ?? "").ToLower().Contains(customerName));
            }

            if (!string.IsNullOrWhiteSpace(customerSurname))
            {
                query = query.Where(x =>
                    (x.CustomerSurname ?? "").ToLower().Contains(customerSurname));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(x =>
                    (x.CustomerPhone ?? "").ToLower().Contains(phone));
            }

            if (CompletedRadioButton.IsChecked == true)
            {
                query = query.Where(x => string.Equals(x.WorkflowStatus, "Tamamlandı", StringComparison.OrdinalIgnoreCase));
            }
            else if (RejectedRadioButton.IsChecked == true)
            {
                query = query.Where(x => string.Equals(x.WorkflowStatus, "Reddedildi", StringComparison.OrdinalIgnoreCase));
            }
            else if (LiveFlowRadioButton.IsChecked == true)
            {
                query = query.Where(x =>
                    !string.Equals(x.WorkflowStatus, "Yönlendirildi", StringComparison.OrdinalIgnoreCase));
            }
            else if (AssignedToPersonRadioButton.IsChecked == true)
            {
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.CreatedByFullName));
            }
            else if (WaitingOnMeRadioButton.IsChecked == true)
            {
                query = query.Where(x =>
                    !string.Equals(x.WorkflowStatus, "Devam Ediyor", StringComparison.OrdinalIgnoreCase));
            }

            _filteredWorkflowItems = query.ToList();
            WorkflowGrid.ItemsSource = _filteredWorkflowItems;
        }



        private void NewWorkflow_Click(object sender, RoutedEventArgs e)
        {
            var home = Window.GetWindow(this) as HomePage;
            if (home != null)
            {
                home.MainFrame.Navigate(new WorkflowWizardPage(home.CurrentUser));
            }
        }
        
        private bool _filtersVisible = true;

        private void FilterToggleButton_Click(object sender, RoutedEventArgs e)
        {
            _filtersVisible = !_filtersVisible;

            FilterContentPanel.Visibility = _filtersVisible
                ? Visibility.Visible
                : Visibility.Collapsed;

            string buttonText = _filtersVisible ? "Filtreleri Gizle" : "Filtrele";
            string iconText = _filtersVisible ? "⌃" : "⌄";

            FilterToggleButtonTop.Content = buttonText;
            FilterToggleButtonBottom.Content = buttonText;

            FilterToggleIconTop.Text = iconText;
            FilterToggleIconBottom.Text = iconText;
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            CustomerNameFilterTextBox.Text = string.Empty;
            CustomerSurnameFilterTextBox.Text = string.Empty;
            PhoneFilterTextBox.Text = string.Empty;

            AssignedToPersonRadioButton.IsChecked = false;
            WaitingOnMeRadioButton.IsChecked = false;
            LiveFlowRadioButton.IsChecked = false;
            CompletedRadioButton.IsChecked = false;
            RejectedRadioButton.IsChecked = false;

            ApplyFilters();
        }

        private async void RefreshDataButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadWorkflowsAsync();
        }
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            WorkflowModel selectedWorkflow = button?.DataContext as WorkflowModel;

            if (selectedWorkflow == null)
            {
                MessageBox.Show("Seçilen iş akışı bulunamadı.");
                return;
            }

            bool isAdmin = (_loggedUser?.UserRole ?? "")
                .Equals("admin", StringComparison.OrdinalIgnoreCase);

            if (isAdmin)
            {
                NavigationService?.Navigate(new WorkflowOperationDetailPage(selectedWorkflow, _loggedUser));
            }
            else
            {
                NavigationService?.Navigate(new WorkflowTeamOperationPage(selectedWorkflow, _loggedUser));
            }
        }
    }
   
}
