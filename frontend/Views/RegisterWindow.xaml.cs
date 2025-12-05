using OmdbClient.Models;
using System.Windows;

namespace OmdbClient.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailBox.Text.Trim();
            var pass = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Email y contraseña son obligatorios.");
                return;
            }

            var success = await App.Api.RegisterAsync(new RegisterDto
            {
                Email = email,
                Password = pass
            });

            if (success)
            {
                MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
                Close();
            }
            else
            {
                MessageBox.Show("Error al registrar. Verifica los datos o intenta más tarde.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}