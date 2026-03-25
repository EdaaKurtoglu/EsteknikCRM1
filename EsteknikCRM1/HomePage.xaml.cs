using EsteknikCRM1.Models;
using EsteknikCRM1.Pages;
using Firebase.Auth;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EsteknikCRM1
{
    public partial class HomePage : Window
    {
        private UserModel _loggedUser;

        public HomePage(UserModel user)
        {
            InitializeComponent();
            MainFrame.Navigate(new HomeContentPage()); // default page
            _loggedUser = user;

            UserNameText.Text = $"{user.Name} {user.Surname}";
        }
        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ResetMenu();

            Button clicked = sender as Button;
            clicked.Tag = "Active"; // aktif yap

            if (clicked == HomeBtn)
                MainFrame.Navigate(new HomeContentPage());

            else if (clicked == WorkflowBtn)
                MainFrame.Navigate(new WorkflowPage());

            else if (clicked == ReportsBtn)
                MainFrame.Navigate(new ReportsPage());
           
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
            MainFrame.Navigate (new TeamDefinitionsPage());
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
            MainFrame.Navigate(new Pages.DeviceCardsPage());
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
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin login = new AdminLogin();
            login.Show();
            this.Close();
        }
    }
}
