using EsteknikCRM1.Models;
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
using static Google.LongRunning.Operations;

namespace EsteknikCRM1.Pages
{
    /// <summary>
    /// Interaction logic for AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        UserModel currentUser;
        public AppointmentPage(UserModel user)
        {
            currentUser = user;
            InitializeComponent();
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Randevu verileri yenilendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(currentUser));
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            // şu an bulunduğun sayfa → boş bırakılabilir
        }
    }
}
