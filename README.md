# BibliotecaMangas

Repositorio: https://github.com/AdriGimenez/BibliotecaMangas

BibliotecaMangas es una API en .NET para administrar una coleccion personal de mangas.

El proyecto nace de una situacion muy concreta: al salir a comprar mangas, es facil olvidarse que tomos ya estan en la biblioteca y terminar revisando fotos del celular para evitar compras repetidas. La solucion busca centralizar esa informacion en una base de datos, permitiendo consultar obras y tomos disponibles, registrar nuevas compras y mantener la coleccion actualizada.

## Descripcion

La aplicacion permite administrar informacion relacionada con una biblioteca de mangas. Actualmente incluye entidades como autores, editoriales, obras y tomos.

El sistema esta desarrollado como una API REST utilizando ASP.NET Core y MySQL como base de datos.

## Tecnologias utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Pomelo EntityFrameworkCore MySQL
- MySQL
- Swagger
- Git
- Docker

## Arquitectura del proyecto

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

- Autor
- Editorial
- Obra
- Tomo

Relaciones principales:

```text
Autor 1 ---- * Obras
Editorial 1 ---- * Obras
Obra 1 ---- * Tomos
```

## Endpoints principales

Algunos endpoints disponibles son:

```text
GET /api/Autores/getAllAutores
GET /api/Editoriales/getAllAutores
GET /api/Obras/getAllObras
GET /api/Tomos/getAllTomos
```

Tambien se implementan operaciones para consultar, guardar, actualizar y eliminar datos desde los repositorios.

## Requisitos previos

Para ejecutar el proyecto se necesita tener instalado:

- Git
- Docker Desktop
- .NET 8 SDK
- MySQL

## Instalacion local

Clonar el repositorio:

```powershell
git clone https://github.com/AdriGimenez/BibliotecaMangas.git
cd BibliotecaMangas
```

Restaurar dependencias del proyecto:

```powershell
dotnet restore
```

Compilar la solucion:

```powershell
dotnet build
```

## Configuracion

El proyecto utiliza una cadena de conexion llamada `CONNECTIONSTRING`.

Para desarrollo local, la configuracion sensible puede ir en:

```text
BibliotecaMangas.Backend/appsettings.Development.json
```

Ejemplo:

```json
{
  "ConnectionStrings": {
    "CONNECTIONSTRING": "Server=localhost; Port=3306; Database=BibliotecaMangas; User=root; Password=tu_password; TreatTinyAsBoolean=True"
  }
}
```

El archivo `appsettings.Development.json` esta ignorado por Git para evitar subir credenciales reales al repositorio.

## Dockerizacion

El proyecto incluye un archivo `Dockerfile` ubicado en:

```text
BibliotecaMangas.Backend/Dockerfile
```

La imagen base utilizada es la imagen oficial de .NET 8:

```text
mcr.microsoft.com/dotnet/aspnet:8.0
mcr.microsoft.com/dotnet/sdk:8.0
```

## Construccion de la imagen Docker

Desde la carpeta raiz del proyecto, ejecutar:

```powershell
docker build -f BibliotecaMangas.Backend/Dockerfile -t bibliotecamangas-backend:tp1 .
```

Este comando construye una imagen personalizada llamada:

```text
bibliotecamangas-backend:tp1
```

## Ejecucion del contenedor

Primero iniciar el contenedor de MySQL:

```powershell
docker start mysql-BibliotecaMangas
```

Luego ejecutar el backend dentro de Docker:

```powershell
docker run --rm -d --name BibliotecaMangas.Backend.TP1 -p 8088:8080 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS=http://+:8080 -e ConnectionStrings__CONNECTIONSTRING="Server=host.docker.internal; Port=3308; Database=BibliotecaMangas; User=root; Password=tu_password; TreatTinyAsBoolean=True" bibliotecamangas-backend:tp1
```

Importante: reemplazar `tu_password` por la contrasena correspondiente de MySQL en el entorno local.

## Verificacion de funcionamiento

Abrir Swagger en el navegador:

```text
http://localhost:8088/swagger/index.html
```

Endpoint de prueba:

```text
http://localhost:8088/api/Autores/getAllAutores
```

Si la API responde correctamente, significa que la aplicacion esta funcionando dentro del contenedor Docker.

## Comandos Git utilizados

Inicializacion y versionado del proyecto:

```powershell
git init
git add .
git commit -m "Estado estable inicial"
git commit -m "Agregar README del proyecto"
git commit -m "Documentar ejecucion con Docker"
```

Publicacion en GitHub:

```powershell
git remote add origin https://github.com/AdriGimenez/BibliotecaMangas.git
git push origin main
```

## Entregables del TP

Este proyecto incluye:

- Codigo fuente completo.
- Repositorio publicado en GitHub.
- Dockerfile funcional.
- README documentado.
- Imagen Docker construida localmente.
- Contenedor ejecutado correctamente.
- API verificada mediante Swagger y endpoints de prueba.

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
