using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace EsteknikCRM1.Pages
{
    public partial class ProductLaborPricePage : Page
    {
        private ObservableCollection<ProductLaborPriceModel> _prices =
            new ObservableCollection<ProductLaborPriceModel>();

        private ProductLaborPriceModel _selectedPrice;

        public ProductLaborPricePage()
        {
            InitializeComponent();
            PricesGrid.ItemsSource = _prices;
            Loaded += ProductLaborPricePage_Loaded;
        }

        private async void ProductLaborPricePage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPricesAsync();
        }

        private async System.Threading.Tasks.Task LoadPricesAsync()
        {
            try
            {
                _prices.Clear();

                var data = await AppServices.ApiProductLaborPriceService.GetAllAsync();

                foreach (var item in data)
                    _prices.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fiyatlar yüklenemedi:\n" + ex.Message);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TryCreateModel(out ProductLaborPriceModel model))
                    return;

                var created = await AppServices.ApiProductLaborPriceService.AddAsync(model);
                _prices.Add(created);

                ClearForm();
                MessageBox.Show("Fiyat başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fiyat eklenemedi:\n" + ex.Message);
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedPrice == null)
                {
                    MessageBox.Show("Lütfen güncellenecek satırı seçin.");
                    return;
                }

                if (!TryCreateModel(out ProductLaborPriceModel model))
                    return;

                model.Id = _selectedPrice.Id;
                model.CreatedDate = _selectedPrice.CreatedDate;

                await AppServices.ApiProductLaborPriceService.UpdateAsync(model);

                _selectedPrice.ProductCode = model.ProductCode;
                _selectedPrice.StockCode = model.StockCode;
                _selectedPrice.ProductName = model.ProductName;
                _selectedPrice.LaborCode = model.LaborCode;
                _selectedPrice.LaborName = model.LaborName;
                _selectedPrice.Price = model.Price;
                _selectedPrice.Currency = model.Currency;
                _selectedPrice.Status = model.Status;

                PricesGrid.Items.Refresh();
                ClearForm();

                MessageBox.Show("Fiyat başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fiyat güncellenemedi:\n" + ex.Message);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is Button button))
                    return;

                var item = button.DataContext as ProductLaborPriceModel;

                if (item == null)
                    return;

                if (MessageBox.Show("Bu fiyat kaydı silinsin mi?", "Onay",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;

                await AppServices.ApiProductLaborPriceService.DeleteAsync(item.Id);
                _prices.Remove(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fiyat silinemedi:\n" + ex.Message);
            }
        }

        private void PricesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPrice = PricesGrid.SelectedItem as ProductLaborPriceModel;

            if (_selectedPrice == null)
                return;

            ProductCodeBox.Text = _selectedPrice.ProductCode;
            ProductNameBox.Text = _selectedPrice.ProductName;
            LaborCodeBox.Text = _selectedPrice.LaborCode;
            LaborNameBox.Text = _selectedPrice.LaborName;
            PriceBox.Text = _selectedPrice.Price.ToString(CultureInfo.InvariantCulture);
        }

        private bool TryCreateModel(out ProductLaborPriceModel model)
        {
            model = null;

            string productCode = ProductCodeBox.Text.Trim();
            string productName = ProductNameBox.Text.Trim();
            string laborCode = LaborCodeBox.Text.Trim();
            string laborName = LaborNameBox.Text.Trim();
            string priceText = PriceBox.Text.Trim().Replace(",", ".");

            if (string.IsNullOrWhiteSpace(productCode))
            {
                MessageBox.Show("Ürün kodu boş olamaz.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(laborCode))
            {
                MessageBox.Show("İşçilik kodu boş olamaz.");
                return false;
            }

            if (!decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
            {
                MessageBox.Show("Geçerli bir fiyat giriniz.");
                return false;
            }

            model = new ProductLaborPriceModel
            {
                Id = Guid.NewGuid().ToString(),
                ProductCode = productCode,
                StockCode = productCode,
                ProductName = productName,
                LaborCode = laborCode,
                LaborName = laborName,
                SubLaborName = "",
                Price = price,
                Currency = "TRY",
                Status = "active",
                CreatedDate = DateTime.UtcNow
            };

            return true;
        }

        private void ClearForm()
        {
            ProductCodeBox.Text = "";
            ProductNameBox.Text = "";
            LaborCodeBox.Text = "";
            LaborNameBox.Text = "";
            PriceBox.Text = "";
            _selectedPrice = null;
            PricesGrid.SelectedItem = null;
        }
    }
}