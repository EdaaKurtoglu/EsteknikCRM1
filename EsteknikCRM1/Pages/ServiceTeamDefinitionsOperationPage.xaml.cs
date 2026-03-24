using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class ServiceTeamDefinitionsOperationPage : Page
    {
        public ServiceTeamDefinitionsOperationPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Takım kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}