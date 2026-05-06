using EsteknikCRM1.Models;
using EsteknikCRM1.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EsteknikCRM1.Popups;
using System.Collections.Generic;

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
            bool isAdmin = (_loggedUser?.UserRole ?? "").ToLower() == "admin";

            
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
                if (!HasAccess("Home")) return;
                MainFrame.Navigate(new HomeContentPage(CurrentUser));
            }
            else if (clicked == WorkflowBtn)
            {
                if (!HasAccess("Workflow")) return;
                MainFrame.Navigate(new WorkflowPage(CurrentUser));
            }
            else if (clicked == ReportsBtn)
            {
                if (!HasAccess("Reports")) return;
                MainFrame.Navigate(new ProductLaborPricePage());
            }
        }

        private void ServiceLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("ServiceLocation")) return;
            MainFrame.Navigate(new ServiceLocationPage());
        }

        private void FreeMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("FreeMaterial")) return;
            MainFrame.Navigate(new FreeMaterialRequestsPage());
        }

        private void ReturnOperation_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("ReturnOperation")) return;
            MainFrame.Navigate(new ReturnSetPage());
        }

        private void StockAction_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Stock")) return;
            MainFrame.Navigate(new StockActionPage());
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Appointment")) return;
            MainFrame.Navigate(new AppointmentPage(CurrentUser));
        }

        private void IndividualCustomerCards_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("IndividualCustomer")) return;
            MainFrame.Navigate(new IndividualCustomerCardsPage(CurrentUser));
        }

        private void CorporateCustomerCards_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("CorporateCustomer")) return;
            MainFrame.Navigate(new CorporateCustomerCardsPage(CurrentUser));
        }

        private void TeamDefinitions_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Team")) return;
            MainFrame.Navigate(new TeamDefinitionsPage(CurrentUser));
        }

        private void DutyDefinitions_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Duty")) return;
            MainFrame.Navigate(new DutyDefinitionsPage());
        }

        private void ProductDefinitions_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Product")) return;
            MainFrame.Navigate(new ProductDefinitionsPage());
        }

        private void DeviceCards_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Device")) return;
            MainFrame.Navigate(new DeviceCardsPage(CurrentUser));
        }

        private void SparePartDefinitions_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("SparePart")) return;
            MainFrame.Navigate(new SparePartDefinitionsPage());
        }

        private void EMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("EMessage")) return;
            MainFrame.Navigate(new EMessagePage());
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Notifications")) return;
            MainFrame.Navigate(new NotificationsPage());
        }

        private void Magazine_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Magazine")) return;
            MainFrame.Navigate(new MagazinePage());
        }

        private void HakedisAction_Click(object sender, RoutedEventArgs e)
        {
            if (!HasAccess("Hakedis")) return;
            MainFrame.Navigate(new HakedisRecordsPage(CurrentUser));
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

        private bool HasAccess(string pageKey)
        {
            if (_loggedUser == null)
                return false;

            bool isAdmin = (_loggedUser.UserRole ?? "").ToLower() == "admin";

            if (isAdmin)
                return true;

            var allowed = new List<string>
                {
                    "Home",
                    "Workflow",
                    "Hakedis"
                };

            if (!allowed.Contains(pageKey))
            {
                MessageBox.Show("Bu işlemi yapmak için yetkiniz yok.",
                    "Yetki Hatası",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            return true;
        }
    }
}