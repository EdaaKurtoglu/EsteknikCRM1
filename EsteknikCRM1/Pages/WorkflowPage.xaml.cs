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


namespace EsteknikCRM1
{
    /// <summary>
    /// Interaction logic for WorkflowPage.xaml
    /// </summary>
    public partial class WorkflowPage : Page
    {
        public WorkflowPage()
        {
            InitializeComponent();
            Loaded += WorkflowPage_Loaded;
        }
        private async void WorkflowPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadWorkflowsAsync();
        }

        private async Task LoadWorkflowsAsync()
        {
            try
            {
                var workflows = await FirebaseService.Instance.GetWorkflowsAsync();
                var gridItems = new List<WorkflowModel>();

                foreach (var item in workflows)
                {
                    string fullName = "";
                    string phone = "";

                    // 🔥 BURASI KRİTİK
                    if (!string.IsNullOrEmpty(item.CustomerId))
                    {
                        var customer = await FirebaseService.Instance.GetCustomerByIdAsync(item.CustomerId);

                        if (customer != null)
                        {
                            fullName = (customer.Name + " " + customer.Surname).Trim();
                            phone = customer.Phone;
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
                        CustomerFullName = fullName,
                        CustomerPhone = phone,
                        AddressLine = item.AddressLine,
                        DeviceName = item.DeviceName,
                        DeviceId = item.DeviceId,
                        CreatedDate = item.CreatedDate,
                        CreatedByFullName = (item.CreatedByName + " " + item.CreatedBySurname).Trim(),
                        CreatedByRole = item.CreatedByRole,
                        StartType = item.StartType
                        
                        
                    });
                }

                WorkflowGrid.ItemsSource = gridItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("İş akışları yüklenirken hata oluştu:\n" + ex.Message);
            }
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
            CustomerNameFilterTextBox.Text = string.Empty;
            CustomerSurnameFilterTextBox.Text = string.Empty;
            PhoneFilterTextBox.Text = string.Empty;

            AssignedToPersonRadioButton.IsChecked = false;
            WaitingOnMeRadioButton.IsChecked = true;
            LiveFlowRadioButton.IsChecked = false;
            CompletedRadioButton.IsChecked = false;
            RejectedRadioButton.IsChecked = false;
        }

        private void RefreshDataButton_Click(object sender, RoutedEventArgs e)
        {
            // burada Firestore'dan tekrar veri çekebilirsin
            MessageBox.Show("Veriler yenilendi.");
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

            NavigationService?.Navigate(new WorkflowOperationDetailPage(selectedWorkflow));
        }
    }
   
}
