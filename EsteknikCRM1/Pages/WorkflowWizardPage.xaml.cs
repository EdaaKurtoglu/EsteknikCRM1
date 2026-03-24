using EsteknikCRM1.DatabaseCon;
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
using Firebase;

namespace EsteknikCRM1.Pages
{
    /// <summary>
    /// Interaction logic for WorkflowWizardPage.xaml
    /// </summary>
    public partial class WorkflowWizardPage : Page
    {
        public WorkflowWizardPage()
        {
            InitializeComponent();
        }

        private void StartTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartTypeBox.SelectedItem != null)
            {
                // Combobox kilitle
                StartTypeBox.IsEnabled = false;

                // Müşteri alanını göster
                CustomerSelectionPanel.Visibility = Visibility.Visible;
            }
        }
        private async void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearchWindow window = new CustomerSearchWindow();
            window.Owner = Window.GetWindow(this);

            if(window.ShowDialog() == true)
            {
                var selectedCustomer = window.SelectedCustomer;

                if (selectedCustomer != null)
                {
                    CustomerNameBox.Text = selectedCustomer.Name+ " " + selectedCustomer.Surname;

                    // adres panelini göster

                    AddressSelectionPanel.Visibility = Visibility.Visible;


                    var addresses = await FirebaseService.Instance.GetCustomerAddressesAsync(selectedCustomer.Id);

                    AddressComboBox.ItemsSource = addresses;
                }
            }
        }

        private void AddressComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAddress = AddressComboBox.SelectedItem as AddressModel;

            if (selectedAddress != null)
            {
                // Seçilen adresi kullan
                NextButton.IsEnabled = true;
                NextButton.Visibility = Visibility.Visible;
            }
        }
        
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = (HomePage)Window.GetWindow(this);
            home.MainFrame.Navigate(new CustomerAddPage());
        }
        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //CustomerSelectionPanel.Visibility = Visibility.Collapsed;
            StartPanel.Visibility = Visibility.Collapsed;
            WorkflowPanel.Visibility = Visibility.Visible;

            var categories =  await FirebaseService.Instance.GetCategoriesAsync();
            CategoryBox.ItemsSource = categories;

            var notifytype = await FirebaseService.Instance.GetNotificationTypeAsync();
            NotificationTypeBox.ItemsSource = notifytype;

            var subcategory = await FirebaseService.Instance.GetSubCategoriesAsync();
            SubCategoryBox.ItemsSource = subcategory;

            var device = await FirebaseService.Instance.GetDevicesAsync();
            DeviceBox.ItemsSource = device;
        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotificationsPanel.Visibility = Visibility.Visible;
        }

        private void NotificationTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubCategoryPanel.Visibility = Visibility.Visible;
        }

        private void SubCategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DevicePanel.Visibility = Visibility.Visible;
        }
        private void DeviceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorkflowNextButton.Visibility = Visibility.Visible;
            WorkflowNextButton.IsEnabled = true;
        }
        private void WorkflowNextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationsPanel.Visibility == Visibility.Collapsed)
            {
                NotificationsPanel.Visibility = Visibility.Visible;
                WorkflowNextButton.IsEnabled = false;
                return;
            }

            if (SubCategoryPanel.Visibility == Visibility.Collapsed)
            {
                SubCategoryPanel.Visibility = Visibility.Visible;
                WorkflowNextButton.IsEnabled = false;
                return;
            }

            if (DevicePanel.Visibility == Visibility.Collapsed)
            {
                DevicePanel.Visibility = Visibility.Visible;
                WorkflowNextButton.IsEnabled = false;
                return;
            }
            FinalReportPanel.Visibility = Visibility.Visible;
            WorkflowPanel.Visibility = Visibility.Collapsed;
        }

    }

}
