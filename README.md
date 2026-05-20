# BibliotecaMangas

BibliotecaMangas es una API en .NET para llevar el control personal de una coleccion de mangas.

La idea nace de un problema cotidiano: al salir a comprar mangas, es facil olvidarse que tomos ya estan en la biblioteca y terminar revisando fotos del celular para no repetir compras. Este proyecto busca resolver eso con una base de datos consultable y actualizable, donde se puedan registrar obras, autores, editoriales y tomos comprados.

En una siguiente etapa, el proyecto apunta a incorporar scraping de novedades editoriales y notificaciones cuando salgan nuevos tomos.

## Estado actual

Actualmente la solucion incluye:

- API backend con ASP.NET Core.
- Acceso a datos con Entity Framework Core.
- Base de datos MySQL.
- Separacion en proyectos para modelos, interfaces, datos y API.
- Endpoints iniciales para consultar autores, editoriales, obras y tomos.
- Swagger habilitado en entorno de desarrollo.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Pomelo EntityFrameworkCore MySQL
- MySQL
- Swagger / Swashbuckle

## Estructura de la solucion

```text
BibliotecaMangas/
+-- BibliotecaMangas.Abstractions.Models/
|   +-- DTOs usados por la API y los repositorios
+-- BibliotecaMangas.Abstractions.Interfaces/
|   +-- Contratos de repositorios
+-- BibliotecaMangas.Data/
|   +-- EF/
|   |   +-- Entidades y DbContext
|   +-- Repositories/
|       +-- Implementaciones de acceso a datos
+-- BibliotecaMangas.Backend/
|   +-- API ASP.NET Core
+-- BibliotecaMangas.sln
```

## Modelo de dominio

El sistema se organiza alrededor de cuatro conceptos principales:

- `Autor`: persona autora de una obra.
- `Editorial`: editorial que publica una obra.
- `Obra`: serie o titulo de manga.
- `Tomo`: volumen individual asociado a una obra.

Relaciones principales:

```text
Autor 1 ---- * Obras
Editorial 1 ---- * Obras
Obra 1 ---- * Tomos
```

DTOs actuales:

```csharp
public class AutorDTO
{
    public string Nombre { get; set; }
}

public class EditorialDTO
{
    public string Nombre { get; set; }
    public string Pais { get; set; }
}

public class ObraDTO
{
    public string Titulo { get; set; }
    public int? AutorId { get; set; }
    public int? EditorialId { get; set; }
}

public class TomoDTO
{
    public int? ObraId { get; set; }
    public int? NumeroTomo { get; set; }
}
```

## Requisitos

Para ejecutar el proyecto localmente se necesita:

- .NET SDK 8 o superior.
- MySQL disponible localmente o en Docker.
- Visual Studio 2022, Rider, VS Code o una terminal con `dotnet`.

## Configuracion local

El archivo `appsettings.json` contiene una cadena de conexion de ejemplo. Para desarrollo local, crear o modificar:

```text
BibliotecaMangas.Backend/appsettings.Development.json
```

Ejemplo:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "CONNECTIONSTRING": "Server=localhost; Port=3306; Database=BibliotecaMangas; User=root; Password=tu_password; TreatTinyAsBoolean=True"
  }
}
```

> `appsettings.Development.json` esta ignorado por Git para evitar subir credenciales reales al repositorio.

## Ejecutar el proyecto

Restaurar dependencias:

```powershell
dotnet restore BibliotecaMangas.sln
```

Compilar la solucion:

```powershell
dotnet build BibliotecaMangas.sln
```

Ejecutar la API:

```powershell
dotnet run --project BibliotecaMangas.Backend
```

Luego abrir Swagger:

```text
http://localhost:5192/swagger
```

## Endpoints iniciales

Los endpoints disponibles actualmente son:

```text
GET /api/Autores/getAllAutores
GET /api/Editoriales/getAllAutores
GET /api/Obras/getAllObras
GET /api/Tomos/getAllTomos
```

Las interfaces de repositorio ya contemplan operaciones de lectura, guardado, actualizacion y borrado:

```csharp
Task<List<T>> GetAll();
Task<T?> GetById(int id);
Task<bool> Save(T item);
Task<bool> Update(int id, T item);
Task<bool> Delete(int id);
```

## Proximos pasos

- Completar endpoints CRUD para autores, editoriales, obras y tomos.
- Agregar busqueda rapida por titulo de obra.
- Consultar tomos faltantes de una obra.
- Crear una interfaz visual para usar desde el celular al comprar.
- Incorporar scraping de novedades editoriales.
- Agregar notificaciones cuando se anuncien nuevos tomos.
- Sumar autenticacion si el proyecto pasa a usarse por mas de una persona.

## Objetivo del proyecto

El objetivo principal es tener una herramienta simple y confiable para responder rapidamente:

- Que obras tengo cargadas?
- Que tomos tengo de cada obra?
- Que tomos me faltan?
- Puedo comprar este tomo sin repetirlo?

La meta es que comprar mangas sea mas facil, con menos dudas y menos dependencia de revisar fotos manualmente.
