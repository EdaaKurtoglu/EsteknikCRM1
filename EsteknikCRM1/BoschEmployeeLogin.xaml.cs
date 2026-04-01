using EsteknikCRM1.DatabaseCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EsteknikCRM1
{
    /// <summary>
    /// Interaction logic for BoschEmployeeLogin.xaml
    /// </summary>
    public partial class BoschEmployeeLogin : Window
    {
        private bool isEmailStep = true;
        private FirebaseService firebaseService;


        public BoschEmployeeLogin()
        {
            InitializeComponent();

        }
        private async void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEmailStep)
            {
                // EMAIL VALIDATION
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
                    var user = await FirebaseService.Instance.LoginAsync(EmailBox.Text.Trim(), PasswordBox.Password.Trim(), "team");
                    

                    if (user!=null)
                    {
                        MessageBox.Show("Giriş başarılı!");

                        HomePage home = new HomePage(user);
                        home.Show();

                        this.Close(); //;
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
            return Regex.IsMatch(email, pattern);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ActionButton_Click(null, null);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!isEmailStep) // Şifre ekranındaysak
            {
                // Şifre panelini kapat
                PasswordPanel.Visibility = Visibility.Collapsed;

                // Email panelini geri göster
                EmailPanel.Visibility = Visibility.Visible;

                // Email preview gizle
                EmailPreview.Visibility = Visibility.Collapsed;

                // Butonu tekrar İleri yap
                ActionButton.Content = "İleri";

                // Şifreyi temizle
                PasswordBox.Clear();

                isEmailStep = true;
            }
            else
            {
                // Email adımındaysak ana sayfaya dön
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
