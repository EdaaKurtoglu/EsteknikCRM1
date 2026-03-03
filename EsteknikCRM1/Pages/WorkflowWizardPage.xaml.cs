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

        private int _currentStep = 1;
        private WorkflowModel _workflow = new WorkflowModel();

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStep == 1)
            {
                _workflow.StartType = (StartTypeBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                GoToStep(2);
            }
            else if (_currentStep == 2)
            {
                _workflow.CustomerName = CustomerNameBox.Text;
                GoToStep(3);
            }
            else if (_currentStep == 3)
            {
                _workflow.Description = DescriptionBox.Text;

                MessageBox.Show("Workflow Kaydedildi");
                // Burada Firestore save yapılabilir
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStep > 1)
                GoToStep(_currentStep - 1);
        }

        private void GoToStep(int step)
        {
            _currentStep = step;

            Step1Panel.Visibility = step == 1 ? Visibility.Visible : Visibility.Collapsed;
            Step2Panel.Visibility = step == 2 ? Visibility.Visible : Visibility.Collapsed;
            Step3Panel.Visibility = step == 3 ? Visibility.Visible : Visibility.Collapsed;

            Step1Indicator.Fill = step >= 1 ? Brushes.Blue : Brushes.Gray;
            Step2Indicator.Fill = step >= 2 ? Brushes.Blue : Brushes.Gray;
            Step3Indicator.Fill = step >= 3 ? Brushes.Blue : Brushes.Gray;
        }


    }

}
