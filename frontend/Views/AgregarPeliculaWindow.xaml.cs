using OmdbClient.Models;
using System;
using System.Windows;
using Microsoft.Win32;

namespace OmdbClient.Views
{
    public partial class AgregarPeliculaWindow : Window
    {
        public AgregarPeliculaWindow()
        {
            InitializeComponent();
        }

        // Seleccionar imagen local
        private void SeleccionarPoster_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Seleccionar póster"
            };

            if (dialog.ShowDialog() == true)
            {
                // Guardamos la ruta absoluta del archivo en PosterBox
                PosterBox.Text = dialog.FileName;
            }
        }

        // Guardar película
        private async void Guardar_Click(object sender, RoutedEventArgs e)
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
                Poster = poster // Puede ser URL o ruta local
            };

            try
            {
                var ok = await App.Api.GuardarFavoritaAsync(dto);
                if (ok)
                {
                    MessageBox.Show("Película agregada con éxito.");
                    this.Close();
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
    }
}