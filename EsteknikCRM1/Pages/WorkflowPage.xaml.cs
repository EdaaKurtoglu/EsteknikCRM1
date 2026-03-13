using EsteknikCRM1.Pages;
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
            /*CustomerSearchWindow window = new CustomerSearchWindow();
            window.Owner = Window.GetWindow(this);

            if (window.ShowDialog() == true)
            {
                var selectedCustomer = window.SelectedCustomer;

               /* if (selectedCustomer != null)
                {
                    CustomerNameText.Text = selectedCustomer.Name;
                }*/
            WorkflowWizardPage workflowWizardPage = new WorkflowWizardPage();
            HomePage home = (HomePage)Window.GetWindow(this);
            home.MainFrame.Navigate(workflowWizardPage);

        }
    }
}
