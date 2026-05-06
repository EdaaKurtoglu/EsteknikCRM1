using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class ProductDefinitionOperationPage : Page
    {
        public ProductDefinitionOperationPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ProductCodeTextBox.Text))
                {
                    MessageBox.Show("Ürün kodu zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(ProductNameTrTextBox.Text))
                {
                    MessageBox.Show("Ürün Adı(TR) zorunludur.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ProductModel product = new ProductModel
                {
                    ProductCode = ProductCodeTextBox.Text?.Trim() ?? "",
                    ProductNameTr = ProductNameTrTextBox.Text?.Trim() ?? "",
                    ProductNameEn = ProductNameEnTextBox.Text?.Trim() ?? "",

                    CostCenter = CostCenterTextBox.Text?.Trim() ?? "",
                    BCode = BCodeTextBox.Text?.Trim() ?? "",
                    ProductManager = ProductManagerTextBox.Text?.Trim() ?? "",

                    WarrantyMonth1 = int.TryParse(WarrantyMonth1TextBox.Text, out int wm1) ? wm1 : 0,
                    WarrantyMonth2 = int.TryParse(WarrantyMonth2TextBox.Text, out int wm2) ? wm2 : 0,

                    Ewl = EwlCheckBox.IsChecked == true,
                    EwlStartDate = EwlStartDatePicker.SelectedDate.HasValue
                        ? (DateTime?)DateTime.SpecifyKind(EwlStartDatePicker.SelectedDate.Value, DateTimeKind.Utc)
                        : null,
                    EwlEndDate = EwlEndDatePicker.SelectedDate.HasValue
                        ? (DateTime?)DateTime.SpecifyKind(EwlEndDatePicker.SelectedDate.Value, DateTimeKind.Utc)
                        : null,

                    Brand = BrandTextBox.Text?.Trim() ?? "",
                    TopGroup = TopGroupTextBox.Text?.Trim() ?? "",
                    SubGroup = SubGroupTextBox.Text?.Trim() ?? "",
                    SpecialGroup = SpecialGroupTextBox.Text?.Trim() ?? "",

                    Country = CountryTextBox.Text?.Trim() ?? "",

                    CascadeSystem = CascadeSystemCheckBox.IsChecked == true,
                    PhaseOut = PhaseOutCheckBox.IsChecked == true,
                    PhaseOutDate = PhaseOutDatePicker.SelectedDate.HasValue
                        ? (DateTime?)DateTime.SpecifyKind(PhaseOutDatePicker.SelectedDate.Value, DateTimeKind.Utc)
                        : null,

                    SapPhaseOut = SapPhaseOutCheckBox.IsChecked == true,
                    SapPhaseOutDate = SapPhaseOutDatePicker.SelectedDate.HasValue
                        ? (DateTime?)DateTime.SpecifyKind(SapPhaseOutDatePicker.SelectedDate.Value, DateTimeKind.Utc)
                        : null,

                    NoSerialNumber = NoSerialNumberCheckBox.IsChecked == true,
                    ProductionPlace = ProductionPlaceTextBox.Text?.Trim() ?? "",
                    SalesInfo = SalesInfoTextBox.Text?.Trim() ?? "",

                    CreatedDate = DateTime.UtcNow,
                    Status = "active"
                };

                string newId = await AppServices.ApiProductService.AddProductAsync(product);

                MessageBox.Show(
                    "Ürün başarıyla eklendi.\nKayıt ID: " + newId,
                    "Başarılı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kayıt sırasında hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SearchCostCenterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Masraf merkezi arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchBCodeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("B kodu arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchProductManagerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ürün yöneticisi arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchBrandButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Marka arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchTopGroupButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Üst ürün grubu arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchSubGroupButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Alt ürün grubu arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchSpecialGroupButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Özel grup arama popup'ı daha sonra bağlanacak.");
        }

        private void SearchCountryButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ülke arama popup'ı daha sonra bağlanacak.");
        }
    }
}