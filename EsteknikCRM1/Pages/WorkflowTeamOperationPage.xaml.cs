using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Models;
using EsteknikCRM1.Models.EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class WorkflowTeamOperationPage : Page
    {
        private readonly WorkflowModel _workflow;
        private readonly UserModel _loggedUser;
        private DeviceModel _selectedDevice;

        private ObservableCollection<WorkflowTeamOperationItem> _items =
            new ObservableCollection<WorkflowTeamOperationItem>();

        public WorkflowTeamOperationPage(WorkflowModel workflow, UserModel loggedUser)
        {
            InitializeComponent();
            _workflow = workflow;
            _loggedUser = loggedUser;
            OperationItemsGrid.ItemsSource = _items;
            OperationTypeComboBox.SelectedIndex = 0;
        }

        private async void DeviceLookupTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string serialNo = SerialNumberTextBox.Text?.Trim();
                string stockCode = StockCodeTextBox.Text?.Trim();

                if (string.IsNullOrWhiteSpace(serialNo) && string.IsNullOrWhiteSpace(stockCode))
                {
                    _selectedDevice = null;
                    DeviceNameTextBox.Text = "";
                    return;
                }

                _selectedDevice = await FirebaseService.Instance.GetDeviceBySerialOrStockCodeAsync(serialNo, stockCode);

                if (_selectedDevice != null)
                {
                    DeviceNameTextBox.Text = _selectedDevice.DeviceName ?? "";
                }
                else
                {
                    DeviceNameTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cihaz aranırken hata oluştu:\n" + ex.Message);
            }
        }

        private async void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedDevice == null)
                {
                    MessageBox.Show("Lütfen geçerli bir cihaz seçiniz.");
                    return;
                }

                if (!(OperationTypeComboBox.SelectedItem is ComboBoxItem selectedOperation) ||
                    selectedOperation.Content?.ToString() == "Lütfen seçiniz...")
                {
                    MessageBox.Show("Lütfen işlem tipi seçiniz.");
                    return;
                }

                string operationType = selectedOperation.Content.ToString();

                decimal price = await FirebaseService.Instance.GetOperationPriceAsync(
                    _selectedDevice.DeviceCode,
                    operationType);

                _items.Add(new WorkflowTeamOperationItem
                {
                    DeviceId = _selectedDevice.Id,
                    SerialNumber = _selectedDevice.SerialNumber,
                    StockCode = _selectedDevice.DeviceCode,
                    DeviceName = _selectedDevice.DeviceName,
                    OperationType = operationType,
                    Price = price,
                    Quantity = 1
                });

                SerialNumberTextBox.Text = "";
                StockCodeTextBox.Text = "";
                DeviceNameTextBox.Text = "";
                OperationTypeComboBox.SelectedIndex = 0;
                _selectedDevice = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem eklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            WorkflowTeamOperationItem selectedItem = button?.DataContext as WorkflowTeamOperationItem;

            if (selectedItem != null)
            {
                _items.Remove(selectedItem);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_items.Count == 0)
                {
                    MessageBox.Show("Kaydedilecek işlem bulunamadı.");
                    return;
                }

                foreach (var item in _items)
                {
                    await FirebaseService.Instance.AddWorkflowTeamOperationAsync(new WorkflowTeamOperationSaveModel
                    {
                        WorkflowId = _workflow.Id,
                        CustomerId = _workflow.CustomerId,
                        DeviceId = item.DeviceId,
                        SerialNumber = item.SerialNumber,
                        StockCode = item.StockCode,
                        DeviceName = item.DeviceName,
                        OperationType = item.OperationType,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        TotalAmount = item.TotalAmount,
                        CreatedByUserMail = _loggedUser?.UserMail,
                        CreatedByName = _loggedUser?.Name,
                        CreatedBySurname = _loggedUser?.Surname,
                        CreatedByRole = _loggedUser?.UserRole,
                        CreatedDate = DateTime.UtcNow
                    });
                }

                MessageBox.Show("İşlemler başarıyla kaydedildi.");
                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında hata oluştu:\n" + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}