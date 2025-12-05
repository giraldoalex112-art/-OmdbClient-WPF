# -OmdbClient-WPF

Aplicación de escritorio en WPF para buscar películas en la API de OMDb y gestionar una lista de favoritas. Incluye modo oscuro, ventanas dedicadas y conexión con backend propio.

# Características principales

- Búsqueda de películas por título usando la OMDb API.
- Visualización de detalles como año, género, director y calificación.
- Gestión de películas favoritas con persistencia local.
- Modo oscuro y claro con estilos personalizados.
- Ventanas dedicadas para login, registro, edición y detalles.
- Conexión con backend propio desarrollado en ASP.NET Core.

# Estructura del repositorio
OmdbClient-WPF/backend/ # API REST en ASP.NET Core frontend/ # Aplicación WPF con arquitectura MVVM .gitignore # Exclusión de binarios y archivos temporales  README.md # Documentación principal del proyecto  LICENSE # Licencia MIT

# Tecnologías utilizadas

- **Frontend:** WPF, C#, MVVM
- **Backend:** ASP.NET Core, C#
- **API externa:** OMDb API
- **IDE:** Visual Studio 2022

#  Cómo ejecutar el frontend (WPF)

1. Abrir `frontend/OmdbClient.sln` en Visual Studio.
2. Compilar en modo `Debug` o `Release`.
3. Ejecutar la aplicación desde `MainWindow.xaml`.

#  Cómo ejecutar el backend
1. Ir a la carpeta `backend/`:
   ```bash, cd backend
2. Ir a la carpeta `backend/`:
   ```bash
   cd backend
3. Restaurar y ejecutar: dotnet restore, dotnet run
Requisitos- .NET SDK 6.0 o superior   
- Visual Studio con soporte para WPF y ASP.NET Core
- Clave de API de OMDb
Capturas de pantalla
<img width="776" height="637" alt="image" src="https://github.com/user-attachments/assets/ee940a31-f44d-420c-a172-a519fd8770a8" />
<img width="477" height="371" alt="image" src="https://github.com/user-attachments/assets/5d9ae2ba-cc53-4b68-9e6c-8808b2dd787d" />
<img width="546" height="312" alt="image" src="https://github.com/user-attachments/assets/9c91d99b-d71d-4d6b-ad34-92e9f5b9dbed" />
<img width="502" height="441" alt="image" src="https://github.com/user-attachments/assets/ecbe0f93-41d7-43c6-a64c-f01e09ffa011" />
<img width="1919" height="991" alt="image" src="https://github.com/user-attachments/assets/93fdade6-878c-4fcc-840b-7649ddb5c18e" />
<img width="725" height="737" alt="image" src="https://github.com/user-attachments/assets/428610bc-01d6-41a4-9313-6250edf69e16" />
<img width="730" height="729" alt="image" src="https://github.com/user-attachments/assets/5496898f-7b9a-4404-8e75-58c1159cf0eb" />
<img width="1919" height="1011" alt="image" src="https://github.com/user-attachments/assets/47cddfcc-fb2a-4bff-b320-6547da612a89" />
<img width="766" height="640" alt="image" src="https://github.com/user-attachments/assets/c517df90-f9a2-41f3-a129-51e5f4c2ae54" />
<img width="776" height="637" alt="image" src="https://github.com/user-attachments/assets/1df62b18-61e5-4ad3-a2a1-8ecccc122aa9" />
