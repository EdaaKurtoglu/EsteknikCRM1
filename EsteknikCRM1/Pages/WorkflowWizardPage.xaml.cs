using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using EsteknikCRM1.Popups;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EsteknikCRM1.Pages
{
    public partial class WorkflowWizardPage : Page
    {
        private CustomerModel _selectedCustomer;
        private bool _workflowDataLoaded = false;

        private readonly Brush BlueBrush =
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#005A9E"));

        private readonly Brush GrayBrush =
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BDBDBD"));

        public WorkflowWizardPage()
        {
            InitializeComponent();
            InitializePageState();
        }

        private void InitializePageState()
        {
            StartPanel.Visibility = Visibility.Visible;
            CustomerSelectionPanel.Visibility = Visibility.Collapsed;
            AddressSelectionPanel.Visibility = Visibility.Collapsed;

            WorkflowPanel.Visibility = Visibility.Collapsed;
            NotificationsPanel.Visibility = Visibility.Collapsed;
            SubCategoryPanel.Visibility = Visibility.Collapsed;
            DevicePanel.Visibility = Visibility.Collapsed;

            FinalReportPanel.Visibility = Visibility.Collapsed;

            NextButton.IsEnabled = false;
            NextButton.Background = GrayBrush;
            NextButton.BorderBrush = GrayBrush;

            WorkflowNextButton.IsEnabled = false;
            WorkflowNextButton.Visibility = Visibility.Collapsed;
            WorkflowNextButton.Background = GrayBrush;
            WorkflowNextButton.BorderBrush = GrayBrush;

            if (AddCustomerButton != null)
            {
                AddCustomerButton.Background = GrayBrush;
                AddCustomerButton.BorderBrush = GrayBrush;
            }

            if (AddAddressButton != null)
            {
                AddAddressButton.Background = GrayBrush;
                AddAddressButton.BorderBrush = GrayBrush;
            }
        }

        private void StartTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(StartTypeBox.SelectedItem is ComboBoxItem selectedItem))
                return;

            string selectedText = selectedItem.Content?.ToString() ?? string.Empty;

            if (selectedText == "Lütfen seçiniz...")
            {
                CustomerSelectionPanel.Visibility = Visibility.Collapsed;
                AddressSelectionPanel.Visibility = Visibility.Collapsed;
                CustomerNameBox.Text = string.Empty;
                AddressComboBox.ItemsSource = null;
                NextButton.IsEnabled = false;
                NextButton.Background = GrayBrush;
                NextButton.BorderBrush = GrayBrush;
                return;
            }

            CustomerSelectionPanel.Visibility = Visibility.Visible;
            AddressSelectionPanel.Visibility = Visibility.Collapsed;

            CustomerNameBox.Text = string.Empty;
            AddressComboBox.ItemsSource = null;
            AddressComboBox.SelectedItem = null;

            NextButton.IsEnabled = false;
            NextButton.Background = GrayBrush;
            NextButton.BorderBrush = GrayBrush;

            AddCustomerButton.Background = BlueBrush;
            AddCustomerButton.BorderBrush = BlueBrush;

            AddAddressButton.Background = GrayBrush;
            AddAddressButton.BorderBrush = GrayBrush;
        }

        private async void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearchWindow window = new CustomerSearchWindow
            {
                Owner = Window.GetWindow(this)
            };

            if (window.ShowDialog() == true)
            {
                var selectedCustomer = window.SelectedCustomer;

                if (selectedCustomer != null)
                {
                    _selectedCustomer = selectedCustomer;

                    CustomerNameBox.Text = $"{selectedCustomer.Name} {selectedCustomer.Surname}";
                    AddressSelectionPanel.Visibility = Visibility.Visible;

                    var addresses = await FirebaseService.Instance.GetCustomerAddressesAsync(selectedCustomer.Id);
                    AddressComboBox.ItemsSource = addresses;

                    AddAddressButton.Background = BlueBrush;
                    AddAddressButton.BorderBrush = BlueBrush;

                    NextButton.IsEnabled = false;
                    NextButton.Background = GrayBrush;
                    NextButton.BorderBrush = GrayBrush;
                }
            }
        }

        private void AddressComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddressComboBox.SelectedItem is AddressModel)
            {
                NextButton.IsEnabled = true;
                NextButton.Background = BlueBrush;
                NextButton.BorderBrush = BlueBrush;
            }
            else
            {
                NextButton.IsEnabled = false;
                NextButton.Background = GrayBrush;
                NextButton.BorderBrush = GrayBrush;
            }
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is HomePage home)
            {
                home.MainFrame.Navigate(new CustomerAddPage());
            }
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            StartPanel.Visibility = Visibility.Collapsed;
            WorkflowPanel.Visibility = Visibility.Visible;

            if (!_workflowDataLoaded)
            {
                await LoadWorkflowDataAsync();
                _workflowDataLoaded = true;
            }

            NotificationsPanel.Visibility = Visibility.Collapsed;
            SubCategoryPanel.Visibility = Visibility.Collapsed;
            DevicePanel.Visibility = Visibility.Collapsed;

            WorkflowNextButton.Visibility = Visibility.Collapsed;
            WorkflowNextButton.IsEnabled = false;
            WorkflowNextButton.Background = GrayBrush;
            WorkflowNextButton.BorderBrush = GrayBrush;
        }

        private async Task LoadWorkflowDataAsync()
        {
            var categories = await FirebaseService.Instance.GetCategoriesAsync();
            CategoryBox.ItemsSource = categories;

            var notifyTypes = await FirebaseService.Instance.GetNotificationTypeAsync();
            NotificationTypeBox.ItemsSource = notifyTypes;

            var subCategories = await FirebaseService.Instance.GetSubCategoriesAsync();
            SubCategoryBox.ItemsSource = subCategories;

            var devices = await FirebaseService.Instance.GetDevicesAsync();
            DeviceBox.ItemsSource = devices;
        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryBox.SelectedItem != null)
            {
                NotificationsPanel.Visibility = Visibility.Visible;
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
            }
        }

        private void NotificationTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotificationTypeBox.SelectedItem != null)
            {
                SubCategoryPanel.Visibility = Visibility.Visible;
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
            }
        }

        private void SubCategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubCategoryBox.SelectedItem != null)
            {
                DevicePanel.Visibility = Visibility.Visible;
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
            }
        }

        private void DeviceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeviceBox.SelectedItem != null)
            {
                WorkflowNextButton.Visibility = Visibility.Visible;
                WorkflowNextButton.IsEnabled = true;
                WorkflowNextButton.Background = BlueBrush;
                WorkflowNextButton.BorderBrush = BlueBrush;
            }
            else
            {
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
                WorkflowNextButton.Background = GrayBrush;
                WorkflowNextButton.BorderBrush = GrayBrush;
            }
        }

        private void WorkflowNextButton_Click(object sender, RoutedEventArgs e)
        {
            FinalReportPanel.Visibility = Visibility.Visible;
            WorkflowPanel.Visibility = Visibility.Collapsed;
        }
    }
}