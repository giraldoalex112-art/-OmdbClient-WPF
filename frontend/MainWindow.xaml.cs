using OmdbClient.Models;
using OmdbClient.Views; // Importa FavoritasWindow
using System;
using System.Windows;

namespace OmdbClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Listar favoritas
        private async void Listar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = await App.Api.GetFavoritasAsync();
                FavoritasList.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar: {ex.Message}");
            }
        }

        // Agregar película manualmente
        private async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            var imdb = ImdbBox.Text.Trim();
            var titulo = TituloBox.Text.Trim();
            var anio = AnioBox.Text.Trim();
            var poster = PosterBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(imdb) || string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(anio) || string.IsNullOrWhiteSpace(poster))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (!int.TryParse(anio, out int anioInt))
            {
                MessageBox.Show("El año debe ser un número válido.");
                return;
            }

            var dto = new FavoritaDto
            {
                ImdbID = imdb,
                Titulo = titulo,
                Year = anioInt.ToString(),
                Poster = poster
            };

            try
            {
                var ok = await App.Api.GuardarFavoritaAsync(dto);
                if (ok)
                {
                    MessageBox.Show("Película agregada y guardada en favoritas.");
                    FavoritasList.ItemsSource = await App.Api.GetFavoritasAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la película.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }

        // Eliminar seleccionada
        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (FavoritasList.SelectedItem is FavoritaDto seleccionada)
            {
                var confirm = MessageBox.Show(
                    $"¿Eliminar '{seleccionada.Titulo}'?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo
                );

                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        var ok = await App.Api.EliminarFavoritaAsync(seleccionada.Id);
                        MessageBox.Show(ok ? "Película eliminada correctamente." : "No se pudo eliminar la película.");
                        if (ok) FavoritasList.ItemsSource = await App.Api.GetFavoritasAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una película para eliminar.");
            }
        }

        // Abrir ventana de favoritas
        private void AbrirFavoritas_Click(object sender, RoutedEventArgs e)
        {
            var favoritas = new FavoritasWindow();
            favoritas.Show(); // ✅ No bloquea ni cierra MainWindow
        }
    }
}