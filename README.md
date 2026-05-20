# BibliotecaMangas

BibliotecaMangas es una API en .NET para administrar una coleccion personal de mangas.

El proyecto nace de una situacion muy concreta: al salir a comprar mangas, es facil olvidarse que tomos ya estan en la biblioteca y terminar revisando fotos del celular para evitar compras repetidas. La solucion busca centralizar esa informacion en una base de datos, permitiendo consultar obras y tomos disponibles, registrar nuevas compras y mantener la coleccion actualizada.

La primera etapa esta enfocada en construir una base solida para la API y el acceso a datos. Mas adelante, la idea es escalar el proyecto con una interfaz visual, scraping de novedades editoriales y notificaciones cuando se anuncien nuevos tomos.

## Alcance actual

La solucion incluye:

- API backend desarrollada con ASP.NET Core.
- Persistencia en MySQL mediante Entity Framework Core.
- Separacion por capas para modelos, contratos, acceso a datos y API.
- Repositorios para autores, editoriales, obras y tomos.
- Endpoints iniciales de consulta.
- Swagger disponible en entorno de desarrollo.

## Stack tecnico

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Pomelo EntityFrameworkCore MySQL
- MySQL
- Swagger / Swashbuckle

## Arquitectura

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

La solucion esta separada en proyectos para mantener responsabilidades claras:

- `BibliotecaMangas.Abstractions.Models`: DTOs compartidos por la API y los repositorios.
- `BibliotecaMangas.Abstractions.Interfaces`: contratos de repositorio.
- `BibliotecaMangas.Data`: entidades, `DbContext` e implementaciones de acceso a datos.
- `BibliotecaMangas.Backend`: API ASP.NET Core y configuracion de servicios.

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
    public string Nombre { get; set; } = null!;
}

public class EditorialDTO
{
    public string Nombre { get; set; } = null!;
    public string Pais { get; set; } = null!;
}

public class ObraDTO
{
    public string Titulo { get; set; } = null!;
    public int? AutorId { get; set; }
    public int? EditorialId { get; set; }
}

public class TomoDTO
{
    public int? ObraId { get; set; }
    public int? NumeroTomo { get; set; }
}
```

## Configuracion

El proyecto utiliza una cadena de conexion llamada `CONNECTIONSTRING`.

Para desarrollo local, la configuracion sensible debe ir en:

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

`appsettings.Development.json` esta ignorado por Git para evitar subir credenciales reales al repositorio.

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

## Roadmap

- Completar endpoints CRUD para autores, editoriales, obras y tomos.
- Agregar busqueda rapida por titulo de obra.
- Consultar tomos faltantes de una obra.
- Crear una interfaz visual para usar desde el celular al comprar.
- Incorporar scraping de novedades editoriales.
- Agregar notificaciones cuando se anuncien nuevos tomos.
- Sumar autenticacion si el proyecto pasa a usarse por mas de una persona.

## Objetivo

El objetivo principal es tener una herramienta simple y confiable para responder rapidamente:

- Que obras tengo cargadas?
- Que tomos tengo de cada obra?
- Que tomos me faltan?
- Puedo comprar este tomo sin repetirlo?

La meta es que comprar mangas sea mas facil, con menos dudas y menos dependencia de revisar fotos manualmente.
