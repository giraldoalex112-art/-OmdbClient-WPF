using OmdbClient.Models;
using System;
using System.Windows;

namespace OmdbClient.Views
{
    public partial class FavoritasWindow : Window
    {
        public FavoritasWindow()
        {
            InitializeComponent();
        }

        private async void Listar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var favoritas = await App.Api.GetFavoritasAsync();
                FavoritasGrid.ItemsSource = favoritas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar: {ex.Message}");
            }
        }

        private async void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var query = BuscarBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Escribe un título o ImdbID para buscar.");
                return;
            }

            try
            {
                var resultado = await App.Api.BuscarFavoritaAsync(query);
                FavoritasGrid.ItemsSource = resultado != null ? new[] { resultado } : null;

                if (resultado == null)
                    MessageBox.Show("No se encontró ninguna película con ese criterio.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}");
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            var formulario = new AgregarPeliculaWindow { Owner = this };
            formulario.Show();
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (FavoritasGrid.SelectedItem is FavoritaDto seleccionada)
            {
                var confirm = MessageBox.Show($"¿Eliminar '{seleccionada.Titulo}'?", "Confirmar eliminación", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        var ok = await App.Api.EliminarFavoritaAsync(seleccionada.Id);
                        MessageBox.Show(ok ? "Película eliminada." : "No se pudo eliminar la película.");
                        if (ok) await RefrescarFavoritas();
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

        private async void VerDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (FavoritasGrid.SelectedItem is FavoritaDto seleccionada)
            {
                if (string.IsNullOrWhiteSpace(seleccionada.ImdbID))
                {
                    MessageBox.Show("La película seleccionada no tiene un ImdbID válido.");
                    return;
                }

                try
                {
                    var detalle = await App.Omdb.ObtenerDetalle(seleccionada.ImdbID);
                    if (detalle == null)
                    {
                        MessageBox.Show("No se pudo obtener el detalle de la película.");
                        return;
                    }

                    var ventana = new EditarDetalleWindow(detalle) { Owner = this };
                    ventana.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error obteniendo detalle: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una película para ver el detalle.");
            }
        }

        private void EditarFila_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is FavoritaDto seleccionada)
            {
                var detalle = new DetallePelicula
                {
                    Title = seleccionada.Titulo,
                    Year = seleccionada.Year,
                    Poster = seleccionada.Poster,
                    Director = "",
                    Genre = "",
                    Actors = "",
                    Plot = ""
                };

                var ventana = new EditarDetalleWindow(detalle) { Owner = this };
                ventana.Closed += async (_, __) =>
                {
                    seleccionada.Titulo = detalle.Title;
                    seleccionada.Year = detalle.Year;
                    seleccionada.Poster = detalle.Poster;

                    FavoritasGrid.Items.Refresh();

                    try
                    {
                        await App.Api.GuardarFavoritaAsync(seleccionada);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar cambios: {ex.Message}");
                    }
                };

                ventana.Show();
            }
        }

        private void ToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("Themes/Dark.xaml", UriKind.Relative);
            var darkTheme = new ResourceDictionary { Source = uri };

            // En vez de borrar todos los diccionarios, solo agregamos el tema
            Application.Current.Resources.MergedDictionaries.Add(darkTheme);
        }

        private async System.Threading.Tasks.Task RefrescarFavoritas()
        {
            var favoritas = await App.Api.GetFavoritasAsync();
            FavoritasGrid.ItemsSource = favoritas;
        }
    }
}