using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.Generic;
using EsteknikCRM1.Services.Pdf;
using PdfSharp.Fonts;

namespace EsteknikCRM1.Pages
{
    public partial class HakedisRecordsPage : Page
    {
        private readonly UserModel _user;

        public HakedisRecordsPage(UserModel user)
        {
            _user = user;
            InitializeComponent();
            Loaded += HakedisRecordsPage_Loaded;
        }

        private async void HakedisRecordsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadSetsAsync();
        }

        private async Task LoadSetsAsync()
        {
            try
            {
                var sets = await AppServices.ApiHakedisSetService.GetHakedisSetsAsync();

                foreach (var set in sets)
                {
                    var operations = await AppServices.ApiOperationService
                        .GetOperationsByHakedisSetIdAsync(set.Id);

                    set.GreenCount = operations.Count(x => x.ApprovalStatus == 1);
                    set.RedCount = operations.Count(x => x.ApprovalStatus == 2);
                    set.BlueCount = operations.Count(x => x.ApprovalStatus == 0);
                }

                HakedisGrid.ItemsSource = sets;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hakediş setleri yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        private void AddNewRecord_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HakedisRecordsOperationPage(_user));
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is HakedisSetModel selectedSet)
            {
                NavigationService?.Navigate(new HakedisSetDetailPage(selectedSet, _user));
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomeContentPage(_user));
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HakedisRecords_Click(object sender, RoutedEventArgs e)
        {
        }
        private async void PdfButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GlobalFontSettings.FontResolver == null)
                {
                    GlobalFontSettings.FontResolver = new WindowsFontResolver();
                }

                var sets = HakedisGrid.ItemsSource as IEnumerable<HakedisSetModel>;

                if (sets == null)
                {
                    MessageBox.Show("PDF oluşturulacak hakediş seti bulunamadı.");
                    return;
                }

                var saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Dosyası (*.pdf)|*.pdf",
                    FileName = $"Hakedis_Detay_Kayitlari_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (saveDialog.ShowDialog() != true)
                    return;

                var allOperations = new List<WorkflowTeamOperationSaveModel>();

                foreach (var set in sets)
                {
                    var operations = await AppServices.ApiOperationService
                        .GetOperationsByHakedisSetIdAsync(set.Id);

                    foreach (var operation in operations)
                    {
                        operation.Id = set.Id;
                    }

                    allOperations.AddRange(operations);
                }

                if (allOperations.Count == 0)
                {
                    MessageBox.Show("PDF oluşturulacak hakediş detay kaydı bulunamadı.");
                    return;
                }

                CreateHakedisOperationsPdf(allOperations, saveDialog.FileName);

                MessageBox.Show("Hakediş detay kayıtları PDF olarak oluşturuldu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF oluşturulurken hata oluştu:\n" + ex.Message);
            }
        }
        private void CreateHakedisOperationsPdf(List<WorkflowTeamOperationSaveModel> operations, string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Hakediş Detay Kayıtları";

            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Landscape;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
            XFont headerFont = new XFont("Arial", 8, XFontStyleEx.Bold);
            XFont rowFont = new XFont("Arial", 7, XFontStyleEx.Regular);

            double margin = 20;
            double y = 30;
            double rowHeight = 24;

            gfx.DrawString("Hakediş Detay Kayıtları", titleFont, XBrushes.Black,
                new XRect(margin, y, page.Width - margin * 2, 25),
                XStringFormats.TopCenter);

            y += 35;

            string[] headers =
            {
        "Set Id",
        "Workflow",
        "Seri No",
        "Stok Kodu",
        "Cihaz",
        "İşçilik Kodu",
        "İşçilik",
        "Fiyat",
        "Adet",
        "Toplam",
        "Ödeme",
        "Durum"
    };

            double[] widths =
            {
        70, 70, 85, 70, 105, 70, 105, 50, 40, 60, 70, 55
    };

            DrawPdfRow(gfx, headers, widths, margin, y, rowHeight, headerFont, true);
            y += rowHeight;

            foreach (var item in operations)
            {
                if (y + rowHeight > page.Height - 40)
                {
                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    page.Orientation = PdfSharp.PageOrientation.Landscape;

                    gfx = XGraphics.FromPdfPage(page);
                    y = 30;

                    DrawPdfRow(gfx, headers, widths, margin, y, rowHeight, headerFont, true);
                    y += rowHeight;
                }

                string approvalStatusText =
                    item.ApprovalStatus == 1 ? "Onaylandı" :
                    item.ApprovalStatus == 2 ? "Reddedildi" :
                    "Bekliyor";

                string[] values =
                {
            item.Id ?? "",
            item.WorkflowId ?? "",
            item.SerialNumber ?? "",
            item.StockCode ?? "",
            item.DeviceName ?? "",
            item.LaborCode ?? "",
            item.LaborName ?? "",
            item.Price.ToString(),
            item.Quantity.ToString(),
            item.TotalAmount.ToString(),
            item.CustomerOrCenterPay ?? "",
            approvalStatusText
        };

                DrawPdfRow(gfx, values, widths, margin, y, rowHeight, rowFont, false);
                y += rowHeight;
            }

            document.Save(filePath);
        }
        private void DrawPdfRow(
            XGraphics gfx,
            string[] values,
            double[] widths,
            double x,
            double y,
            double height,
            XFont font,
            bool isHeader)
        {
            double currentX = x;

            for (int i = 0; i < values.Length; i++)
            {
                var rect = new XRect(currentX, y, widths[i], height);

                gfx.DrawRectangle(XPens.Black, rect);

                if (isHeader)
                {
                    gfx.DrawRectangle(XBrushes.LightGray, rect);
                    gfx.DrawRectangle(XPens.Black, rect);
                }

                gfx.DrawString(
                    values[i],
                    font,
                    XBrushes.Black,
                    new XRect(currentX + 3, y + 4, widths[i] - 6, height - 4),
                    XStringFormats.TopLeft);

                currentX += widths[i];
            }
        }
    }
}