using System.Windows;
using System.Windows.Controls;

namespace OmdbClient.Views
{
    public partial class DarkModeToggle : UserControl
    {
        public DarkModeToggle()
        {
            InitializeComponent();
        }

        private void ToggleButtonTheme_Checked(object sender, RoutedEventArgs e)
        {
            App.CambiarTema(true); // Activa modo oscuro
            ToggleButtonTheme.Content = "☀️ Light Mode";
        }

        private void ToggleButtonTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            App.CambiarTema(false); // Activa modo claro
            ToggleButtonTheme.Content = "🌙 Dark Mode";
        }
    }
}