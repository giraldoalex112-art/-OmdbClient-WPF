using System.Windows;
using OmdbClient.Models;

namespace OmdbClient.Views
{
    public partial class DetallePeliculaWindow : Window
    {
        public DetallePeliculaWindow(DetallePelicula detalle)
        {
            InitializeComponent();
            DataContext = detalle; // ✅ Vincula el objeto DetallePelicula a la vista
        }
    }
}