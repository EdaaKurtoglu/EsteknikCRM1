using EsteknikCRM1.Models;
using EsteknikCRM1.Services;
using System;
using System.Windows;

namespace EsteknikCRM1.Popups
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly UserModel _currentUser;

        public ChangePasswordWindow(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentPassword = CurrentPasswordBox.Password?.Trim() ?? "";
                string newPassword = NewPasswordBox.Password?.Trim() ?? "";
                string confirmPassword = ConfirmPasswordBox.Password?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(currentPassword))
                {
                    MessageBox.Show("Mevcut şifre zorunludur.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Yeni şifre zorunludur.");
                    return;
                }

                if (newPassword.Length < 3)
                {
                    MessageBox.Show("Yeni şifre en az 3 karakter olmalıdır.");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Yeni şifreler eşleşmiyor.");
                    return;
                }

                await AppServices.ApiAuthService.ChangePasswordAsync(
                    _currentUser.Id,
                    currentPassword,
                    newPassword);

                MessageBox.Show("Şifre başarıyla değiştirildi.",
                    "Başarılı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Şifre değiştirilirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}