using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EsteknikCRM1
{
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomeContentPage()); // default page
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
    }
}
