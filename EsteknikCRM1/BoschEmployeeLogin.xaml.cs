using EsteknikCRM1.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace EsteknikCRM1
{
    public partial class BoschEmployeeLogin : Window
    {
        private bool isEmailStep = true;

        public BoschEmployeeLogin()
        {
            InitializeComponent();
        }

        private async void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEmailStep)
            {
                if (!IsValidEmail(EmailBox.Text))
                {
                    MessageBox.Show("Geçerli bir email giriniz.");
                    return;
                }

                EmailPreview.Text = EmailBox.Text;
                EmailPreview.Visibility = Visibility.Visible;

                EmailPanel.Visibility = Visibility.Collapsed;
                PasswordPanel.Visibility = Visibility.Visible;

                ActionButton.Content = "Giriş";
                isEmailStep = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(PasswordBox.Password))
                {
                    MessageBox.Show("Şifre giriniz.");
                    return;
                }

                try
                {
                    var user = await AppServices.ApiAuthService.LoginAsync(
                        EmailBox.Text.Trim(),
                        PasswordBox.Password.Trim(),
                        "team");

                    if (user != null)
                    {
                        MessageBox.Show("Giriş başarılı!");

                        HomePage home = new HomePage(user);
                        home.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Email veya şifre hatalı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email ?? "", pattern);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ActionButton_Click(null, null);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!isEmailStep)
            {
                PasswordPanel.Visibility = Visibility.Collapsed;
                EmailPanel.Visibility = Visibility.Visible;
                EmailPreview.Visibility = Visibility.Collapsed;
                ActionButton.Content = "İleri";
                PasswordBox.Clear();
                isEmailStep = true;
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}