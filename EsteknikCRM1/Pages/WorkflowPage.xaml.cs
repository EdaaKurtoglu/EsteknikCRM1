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
                var gridItems = new List<WorkflowGridItem>();

                foreach (var item in workflows)
                {
                    gridItems.Add(new WorkflowGridItem
                    {
                        Id = item.Id,
                        Status = item.WorkflowStatus,
                        FlowType = item.FlowType,
                        LastAction = item.NotificationTypeName,
                        Subject = item.Subject,
                        Customer = item.CustomerFullName,
                        Phone = "",
                        CreatedDate = item.CreatedDate == DateTime.MinValue
                            ? ""
                            : item.CreatedDate.ToString("dd/MM/yyyy\nHH:mm"),
                        CreatedBy = (item.CreatedByName + " " + item.CreatedBySurname).Trim(),
                        CreatedRole = item.CreatedByRole,
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
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var row = button.DataContext;

            MessageBox.Show("Selected row: " + row?.ToString());
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
    }
    public class WorkflowGridItem
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string FlowType { get; set; }
        public string LastAction { get; set; }
        public string Subject { get; set; }
        public string Customer { get; set; }
        public string Phone { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedRole { get; set; }
        public string StartType { get; set; }
    }
}
