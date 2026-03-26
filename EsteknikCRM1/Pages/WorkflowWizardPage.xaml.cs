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
        private AddressModel _selectedAddress;
        private CategoriesModel _selectedCategory;
        private NotificationTypeModel _selectedNotificationType;
        private SubCategoriesModel _selectedSubCategory;
        private DeviceModel _selectedDevice;

        private bool _workflowDataLoaded = false;

        private readonly Brush BlueBrush =
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#005A9E"));

        private readonly Brush GrayBrush =
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BDBDBD"));

        private readonly UserModel _currentUser;

        public WorkflowWizardPage(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            InitializePageState();
        }
        public WorkflowWizardPage()
        {
            InitializeComponent();
           
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
            _selectedAddress = AddressComboBox.SelectedItem as AddressModel;

            if (_selectedAddress != null)
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
            _selectedCategory = CategoryBox.SelectedItem as CategoriesModel;

            if (_selectedCategory != null)
            {
                NotificationsPanel.Visibility = Visibility.Visible;
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
            }
        }

        private void NotificationTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedNotificationType = NotificationTypeBox.SelectedItem as NotificationTypeModel;

            if (_selectedNotificationType != null)
            {
                SubCategoryPanel.Visibility = Visibility.Visible;
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
            }
        }

        private void SubCategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSubCategory = SubCategoryBox.SelectedItem as SubCategoriesModel;

            if (_selectedSubCategory != null)
            {
                DevicePanel.Visibility = Visibility.Visible;
                WorkflowNextButton.Visibility = Visibility.Collapsed;
                WorkflowNextButton.IsEnabled = false;
            }
        }

        private void DeviceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDevice = DeviceBox.SelectedItem as DeviceModel;

            if (_selectedDevice != null)
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

        private async void SaveWorkflowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Lütfen müşteri seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_selectedAddress == null)
                {
                    MessageBox.Show("Lütfen adres seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_selectedCategory == null || _selectedNotificationType == null || _selectedSubCategory == null || _selectedDevice == null)
                {
                    MessageBox.Show("Lütfen kategori, bildirim tipi, alt kategori ve cihaz seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string startType = "";
                ComboBoxItem startTypeItem = StartTypeBox.SelectedItem as ComboBoxItem;
                if (startTypeItem != null)
                    startType = startTypeItem.Content.ToString();

                string arrivalChannel = "";
                ComboBoxItem arrivalItem = ArrivalChannelComboBox.SelectedItem as ComboBoxItem;
                if (arrivalItem != null && arrivalItem.Content != null)
                    arrivalChannel = arrivalItem.Content.ToString();

                WorkflowModel workflow = new WorkflowModel
                {
                    StartType = startType,

                    CustomerId = _selectedCustomer.Id,
                    CustomerName = _selectedCustomer.Name,
                    CustomerSurname = _selectedCustomer.Surname,
                    CustomerFullName = (_selectedCustomer.Name + " " + _selectedCustomer.Surname).Trim(),

                    AddressId = _selectedAddress.Id,
                    AddressLine = _selectedAddress.AddressLine,

                    CategoryId = _selectedCategory.ID,
                    CategoryName = _selectedCategory.Category,

                    NotificationTypeId = _selectedNotificationType.ID,
                    NotificationTypeName = _selectedNotificationType.NotificationType,

                    SubCategoryId = _selectedSubCategory.ID,
                    SubCategoryName = _selectedSubCategory.Name,

                    DeviceId = _selectedDevice.Id,
                    DeviceName = _selectedDevice.DeviceName,

                    Description = DescriptionTextBox.Text,
                    ExtraDescription = ExtraDescriptionTextBox.Text,
                    ArrivalChannel = arrivalChannel,

                    WorkflowStatus = "Devam Ediyor",
                    FlowType = _selectedCategory.Category,
                    Subject = _selectedNotificationType.NotificationType,

                    // Giriş yapan kullanıcıyı burada sen nasıl tutuyorsan ona göre doldur
                    CreatedByUserMail = _currentUser != null ? _currentUser.UserMail : "",
                    CreatedByName = _currentUser != null ? _currentUser.Name : "",
                    CreatedBySurname = _currentUser != null ? _currentUser.Surname : "",
                    CreatedByRole = _currentUser != null ? _currentUser.UserRole : "",

                    CreatedDate = DateTime.UtcNow
                };

                string newId = await FirebaseService.Instance.AddWorkflowAsync(workflow);

                MessageBox.Show("İş akışı başarıyla kaydedildi. ID: " + newId,
                                "Başarılı",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                var home = Window.GetWindow(this) as HomePage;
                if (home != null)
                {
                    home.MainFrame.Navigate(new WorkflowPage());
                }
                // istersen liste sayfasına dön
                // NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu:\n" + ex.Message,
                                "Hata",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void AddCustomerAddresses_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("Önce bir müşteri seçmelisiniz.");
                return;
            }

            NavigationService?.Navigate(new IndividualCustomerAddressesPage(_selectedCustomer));
        }
    }
}