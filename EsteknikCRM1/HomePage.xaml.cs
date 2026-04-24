using EsteknikCRM1.Models;
using EsteknikCRM1.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EsteknikCRM1.Popups;

namespace EsteknikCRM1
{
    public partial class HomePage : Window
    {
        private UserModel _loggedUser;

        public UserModel CurrentUser { get; private set; }

        public HomePage(UserModel user)
        {
            InitializeComponent();

            _loggedUser = user;
            CurrentUser = user;
            UserInitialText.Text = string.IsNullOrWhiteSpace(CurrentUser.Name)
                ? ""
                : user.Name.Substring(0, 1).ToUpper();
            ApplyRolePermissions();
            SetLoggedUserInfo();
            MainFrame.Navigate(new HomeContentPage(user));
        }
        private void ApplyRolePermissions()
        {
            if (_loggedUser == null)
                return;

            // admin kontrol (küçük/büyük harf farkını kaldırıyoruz)
            bool isAdmin = (_loggedUser.UserRole ?? "").ToLower() == "admin";

            if (!isAdmin)
            {
                // ilk 2 buton hariç hepsini kapat
                ReportsBtn.IsEnabled = false;
                ServiceLocationBtn.IsEnabled = false;
                FreeMaterialBtn.IsEnabled = false;
                ReturnOperationBtn.IsEnabled = false;
                StockActionBtn.IsEnabled = false;
                AppointmentBtn.IsEnabled = false;
                IndividualCustomerCardsBtn.IsEnabled = false;
                CorporateCustomerCardsBtn.IsEnabled = false;
                TeamDefinitionsBtn.IsEnabled = false;
                DutyDefinitionsBtn.IsEnabled = false;
                ProductDefinitionsBtn.IsEnabled = false;
                DeviceCardsBtn.IsEnabled = false;
                SparePartDefinitionsBtn.IsEnabled = false;
                EMessageBtn.IsEnabled = false;
                NotificationsBtn.IsEnabled = false;
                MagazineBtn.IsEnabled = false;
                HakedisActionBtn.IsEnabled = true;
                PriceProductButton.IsEnabled = false;
            }
        }
        private void SetLoggedUserInfo()
        {
            if (_loggedUser == null)
            {
                UserNameText.Text = "Kullanıcı";
                UserRoleText.Text = "Rol Tanımsız";
                PopupUserNameText.Text = "Kullanıcı";
                PopupUserRoleText.Text = "Rol Tanımsız";
                UserInitialText.Text = "?";
                return;
            }

            string fullName = string.Format("{0} {1}",
                _loggedUser.Name ?? "",
                _loggedUser.Surname ?? "").Trim();

            if (string.IsNullOrWhiteSpace(fullName))
                fullName = "Kullanıcı";

            string role = string.IsNullOrWhiteSpace(_loggedUser.UserRole)
                ? "Rol Tanımsız"
                : _loggedUser.UserRole;

            UserNameText.Text = fullName;
            UserRoleText.Text = role;

            PopupUserNameText.Text = fullName;
            PopupUserRoleText.Text = role;

            if (!string.IsNullOrWhiteSpace(_loggedUser.Name))
                UserInitialText.Text = _loggedUser.Name.Substring(0, 1).ToUpper();
            else if (!string.IsNullOrWhiteSpace(_loggedUser.Surname))
                UserInitialText.Text = _loggedUser.Surname.Substring(0, 1).ToUpper();
            else
                UserInitialText.Text = "K";
        }

        public void OpenWorkflowWizardPage()
        {
            MainFrame.Navigate(new WorkflowWizardPage(CurrentUser));
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ResetMenu();

            Button clicked = sender as Button;
            if (clicked != null)
                clicked.Tag = "Active";

            if (clicked == HomeBtn)
            {
                MainFrame.Navigate(new HomeContentPage(CurrentUser));
            }
            else if (clicked == WorkflowBtn)
            {
                MainFrame.Navigate(new WorkflowPage(CurrentUser));
            }
            else if (clicked == ReportsBtn)
            {
                MainFrame.Navigate(new ProductLaborPricePage());
            }
        }

        private void ServiceLocation_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServiceLocationPage());
        }

        private void FreeMaterial_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FreeMaterialRequestsPage());
        }

        private void ReturnOperation_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReturnSetPage());
        }

        private void StockAction_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StockActionPage());
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AppointmentPage());
        }

        private void IndividualCustomerCards_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new IndividualCustomerCardsPage());
        }

        private void CorporateCustomerCards_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CorporateCustomerCardsPage());
        }

        private void TeamDefinitions_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeamDefinitionsPage());
        }

        private void DutyDefinitions_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DutyDefinitionsPage());
        }

        private void ProductDefinitions_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductDefinitionsPage());
        }

        private void DeviceCards_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DeviceCardsPage());
        }

        private void SparePartDefinitions_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SparePartDefinitionsPage());
        }

        private void EMessage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EMessagePage());
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NotificationsPage());
        }

        private void Magazine_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MagazinePage());
        }

        private void HakedisAction_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HakedisRecordsPage());
        }

        private void ResetMenu()
        {
            HomeBtn.Tag = null;
            WorkflowBtn.Tag = null;
            ReportsBtn.Tag = null;
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin login = new AdminLogin();
            login.Show();
            Close();
        }
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("Kullanıcı bilgisi bulunamadı.",
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var window = new ChangePasswordWindow(CurrentUser)
            {
                Owner = this
            };

            window.ShowDialog();
        }
    }
}