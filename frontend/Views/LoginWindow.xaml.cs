using OmdbClient.Models;
using System;
using System.Windows;

namespace OmdbClient.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailBox.Text.Trim();
            var pass = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Email y contraseña son obligatorios.");
                return;
            }

            try
            {
                var response = await App.Api.LoginAsync(new LoginDto
                {
                    Email = email,
                    Password = pass
                });

                if (response == null || string.IsNullOrWhiteSpace(response.Token))
                {
                    MessageBox.Show("Login inválido.");
                    return;
                }

                App.Api.SetToken(response.Token);

                MessageBox.Show($"Login exitoso.\nToken: {response.Token}\nEmail: {response.Email}");

                var main = new MainWindow();
                main.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de login: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var register = new RegisterWindow();
            register.ShowDialog();
        }
    }
}