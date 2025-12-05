using System;
using System.Windows;
using System.Windows.Media.Imaging;
using OmdbClient.Models;

namespace OmdbClient.Views
{
    public partial class BuscarPeliculasWindow : Window
    {
        public BuscarPeliculasWindow()
        {
            InitializeComponent();
        }

        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            var query = TxtBusqueda.Text;
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Ingrese un título para buscar.");
                return;
            }

            var pelicula = await App.Api.BuscarEnOmdbAsync(query);

            if (pelicula != null)
            {
                TituloTextBlock.Text = $"{pelicula.Title} ({pelicula.Year})";
                SinopsisTextBlock.Text = pelicula.Plot;

                if (!string.IsNullOrEmpty(pelicula.Poster))
                {
                    try
                    {
                        PosterImage.Source = new BitmapImage(new Uri(pelicula.Poster));
                    }
                    catch
                    {
                        PosterImage.Source = null;
                    }
                }
                else
                {
                    PosterImage.Source = null;
                }
            }
            else
            {
                MessageBox.Show("No se encontró la película.");
                TituloTextBlock.Text = string.Empty;
                SinopsisTextBlock.Text = string.Empty;
                PosterImage.Source = null;
            }
        }

        private void BtnModoOscuro_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
            var darkTheme = new ResourceDictionary { Source = uri };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(darkTheme);
        }
    }
}