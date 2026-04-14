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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using Firebase.Auth;
using Firebase.Auth.Providers;
using EsteknikCRM1.DatabaseCon;
using EsteknikCRM1.Services;


namespace EsteknikCRM1
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ContinueButton_Click(null, null);
            }
        }

        private bool isEmailStep = true;

        private async void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEmailStep)
            {
                if (!IsValidEmail(EmailTextBox.Text))
                {
                    MessageBox.Show("Geçerli bir e-posta giriniz.");
                    return;
                }

                FadeTransition(EmailPanel, PasswordPanel);
                ContinueButton.Content = "GİRİŞ";
                isEmailStep = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(PasswordTextBox.Password))
                {
                    MessageBox.Show("Şifre giriniz.");
                    return;
                }

                await FirebaseLogin();
            }
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void FadeTransition(UIElement hideElement, UIElement showElement)
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, e) =>
            {
                hideElement.Visibility = Visibility.Collapsed;

                showElement.Opacity = 0;
                showElement.Visibility = Visibility.Visible;

                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
                showElement.BeginAnimation(OpacityProperty, fadeIn);
            };

            hideElement.BeginAnimation(OpacityProperty, fadeOut);
        }
        private async Task FirebaseLogin()
        {
            try
            {
                var user = await AppServices.ApiAuthService.LoginAsync(EmailTextBox.Text, PasswordTextBox.Password, "admin");
                //var user = await FirebaseService.Instance.LoginAsync(EmailTextBox.Text, PasswordTextBox.Password, "admin");
                /*bool isValid = await firebase.CheckUserAsync(
                    EmailTextBox.Text,
                    PasswordTextBox.Password,
                    "admin"   // burada role belirliyoruz
                );*/

                if (user!=null)
                {
                    MessageBox.Show("Giriş başarılı!");

                    HomePage home = new HomePage(user);
                    home.Show();

                    this.Close(); // login ekranını kapatır;
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!isEmailStep)
            {
                FadeTransition(PasswordPanel, EmailPanel);
                ContinueButton.Content = "DEVAM ET";
                PasswordTextBox.Clear();
                isEmailStep = true;
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

    }
}
