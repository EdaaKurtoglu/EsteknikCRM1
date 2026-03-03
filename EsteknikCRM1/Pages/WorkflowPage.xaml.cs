using EsteknikCRM1.Pages;
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
        }
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var row = button.DataContext;

            MessageBox.Show("Selected row: " + row?.ToString());
        }
        private void NewWorkflow_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new WorkflowWizardPage());
        }
    }
}
