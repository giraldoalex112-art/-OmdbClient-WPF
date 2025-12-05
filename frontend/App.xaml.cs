using OmdbClient.Services;
using System;
using System.Net.Http;
using System.Windows;

namespace OmdbClient
{
    public partial class App : Application
    {
        public static ApiClient Api { get; private set; } = null!;
        public static OmdbService Omdb { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnLastWindowClose;

            // Dirección base de la API
            var baseUri = new Uri("http://localhost:5259");

            // HttpClient compartido para todos los servicios
            var http = new HttpClient { BaseAddress = baseUri };

            // Inicialización de clientes
            Api = new ApiClient(baseUri.ToString());
            Omdb = new OmdbService(http);

            // ✅ Abre LoginWindow al iniciar
            new Views.LoginWindow().Show();
        }

        public static void CambiarTema(bool oscuro)
        {
            // Implementación de tema si usas diccionarios Light.xaml / Dark.xaml
            // Ejemplo:
            // var dict = new ResourceDictionary();
            // dict.Source = new Uri(oscuro ? "Dark.xaml" : "Light.xaml", UriKind.Relative);
            // Current.Resources.MergedDictionaries.Clear();
            // Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}