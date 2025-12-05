using OmdbClient.Models;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace OmdbClient.Views
{
    public partial class EditarDetalleWindow : Window
    {
        private DetallePelicula _detalle;

        public EditarDetalleWindow(DetallePelicula detalle)
        {
            InitializeComponent();
            _detalle = detalle;

            // Cargar datos en los campos
            TitleBox.Text = _detalle.Title;
            YearBox.Text = _detalle.Year;
            GenreBox.Text = _detalle.Genre;
            DirectorBox.Text = _detalle.Director;
            ActorsBox.Text = _detalle.Actors;
            PlotBox.Text = _detalle.Plot;
            PosterBox.Text = _detalle.Poster;

            // Mostrar imagen inicial si existe
            if (!string.IsNullOrWhiteSpace(_detalle.Poster))
            {
                try
                {
                    PosterImage.Source = new BitmapImage(new Uri(_detalle.Poster, UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    // Si falla, no rompe la ventana
                }
            }
        }

        private void SeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Seleccionar imagen de póster"
            };

            if (dialog.ShowDialog() == true)
            {
                PosterBox.Text = dialog.FileName;
                try
                {
                    PosterImage.Source = new BitmapImage(new Uri(dialog.FileName));
                }
                catch
                {
                    MessageBox.Show("No se pudo cargar la imagen seleccionada.");
                }
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Actualizar el objeto con los nuevos valores
            _detalle.Title = TitleBox.Text.Trim();
            _detalle.Year = YearBox.Text.Trim();
            _detalle.Genre = GenreBox.Text.Trim();
            _detalle.Director = DirectorBox.Text.Trim();
            _detalle.Actors = ActorsBox.Text.Trim();
            _detalle.Plot = PlotBox.Text.Trim();
            _detalle.Poster = PosterBox.Text.Trim();

            // Aquí podrías llamar a un endpoint para actualizar en el backend
            MessageBox.Show("Cambios guardados (simulado).");
            this.Close();
        }
    }
}