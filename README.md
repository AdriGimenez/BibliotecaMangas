# BibliotecaMangas - API para Biblioteca Personal de Mangas

BibliotecaMangas es una API en .NET para administrar una coleccion personal de mangas.

El proyecto permite registrar y consultar información sobre autores, editoriales, obras y tomos, con el objetivo de tener una biblioteca organizada y evitar comprar tomos repetidos.

Repositorio: https://github.com/AdriGimenez/BibliotecaMangas

## Descripcion

BibliotecaMangas nace de una situación concreta: al salir a comprar mangas, muchas veces es dificil recordar qué tomos ya están en la biblioteca personal.

La aplicación busca centralizar esa información en una base de datos para poder consultar rápidamente:

* qué obras están cargadas;
* qué tomos hay de cada obra;
* qué tomos faltan;
* qué tomo se puede comprar sin repetir.

El sistema está desarrollado como una API REST utilizando ASP.NET Core y MySQL.

---

## Tecnologias utilizadas

|Tecnología|Uso|
|----------|-----|
|.NET 8|Plataforma principal del backend|
|ASP.NET Core Web API|Creación de la API REST|
|Entity Framework Core|Acceso a datos|
|Pomelo EntityFrameworkCore MySQL|Conexión entre EF Core y MySQL|
|MySQL|Base de Datos|
|Swagger|Documentación y prueba de endpoints|
|Git|Control de versiones|
|GitHub|Repositorio remoto|
|Docker|Contenedorización del backend|
|Docker Compose|Ejecución conjunta de backend y base de datos|

---

## Características

* API REST para administrar una biblioteca de mangas.
* Gestión de autores, editoriales, obras y tomos.
* Base de datos MySQL inicializada mediante Swagger.
* Documentación de endpoints mediante Swagger.
* Backend dockerizado con Dockerfile.
* Ejecución completa mediante Docker Compose.
* Datos iniciales cargados desde un script SQL. 

---

## Requisitos previos

Para ejecutar el proyecto se necesita tener instalado:
* Git
* Docker Desktop

No es necesario instalar MySQL de forma manual, porque Docker Compose crea y ejecuta un contenedor MySQL automáticamente.

No es necesario ejecutar manualmente ```dotnet restore``` ni ```dotnet build``` para probar el proyecto con Docker, ya que la construcción se realiza dentro del contenedor usando las imágenes oficiales de .NET.

--- 

## Arquitectura del proyecto

```text
BibliotecaMangas/
├── BibliotecaMangas.Abstractions.Models/
|    └──DTOs usados por la API y los repositorios
├── BibliotecaMangas.Abstractions.Interfaces/
|   └──Contratos de repositorios
├── BibliotecaMangas.Data/
|   └── EF/
|       └── Entidades y DbContext
|   └── Repositories/
|       └── Implementaciones de acceso a datos
├── BibliotecaMangas.Backend/
|   └── API ASP.NET Core
├── mysql-init/
|   └──Script SQL inicial para crear y cargar la base de datos
├── docs/
├── docker-compose.yml
├── BibliotecaMangas.sln
└── README.md
```

---

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

---

## Endpoints principales

Algunos endpoints disponibles son:

```text
GET /api/Autores/getAllAutores
GET /api/Editoriales/getAllEditoriales
GET /api/Obras/getAllObras
GET /api/Tomos/getAllTomos
```

Tambien se implementan operaciones para consultar, guardar, actualizar y eliminar datos desde los repositorios.

---

## Opción 1 - Ejecutar el proyecto completo con Docker Compose

Esta es la forma recomendada para ejecutar el proyecto, porque levanta automáticamente el backend y la base de datos MySQL.

### 1. Clonar el repositorio
```text
git clone https://github.com/AdriGimenez/BibliotecaMangas.git
cd BibliotecaMangas
```

### 2. Ejecutar Docker Compose
```text
docker compose up --build
```

Este comando:
* construye la imagen backend;
* descarga la imagen de MySQL si no existe localmente;
* crea el contenedor de MySQL;
* crea la base de datos ```BibliotecaMangas```;
* ejecuta el script ```mysql-init/01-init.sql```;
* levanta el backend en el puerto ```8088```.

### 3. Abrir Swagger en el navegador
```text
http://localhost:8088/swagger/index.html
```

### 4. Probar un endpoint
```text
http://localhost:8088/api/Obras/getAllObras
```

Si el endpoint devuelve datos, la aplicación está funcionando correctamente.

---

## Detener los contenedores

Para detener la ejecución:
```text
docker compose down
```

---

## Reiniciar la base de datos desde cero

Si se desea borrar el volumen de MySQL y volver a cargar el script inicial:
```text
docker compose down -v
docjer compose up --build
```

El parámetro ```-v``` elimina el volumen de MySQL. Al ejecutar nuevamente Docker Compose, la base de datos vuelve a crearse desde el archivo:
```text
mysql-init/01-init.sql
```

---

## Opción 2 - Construir manualmente la imagen Docker del backend

También se puede construir la imagen Docker del backend manualmente.

Desde la carpeta raíz del proyecto:
```text
docker build -f BibliotecaMangas.Backend/Dockerfile -t bibliotecamangas-backend:tp .
```

Este comando crea la imagen:
```text
bibliotecamangas-backend:tp1
```

Importante: este comando solo construye la imagen del backend. Para ejecutar el proyecto completo con backend y base de datos MySQL, se recomienda usar Docker Compose.

---

## Cómo funciona el Dockerfile

El archivo ```Dockerfile``` se encuentra en:
```text
BibliotecaMangas.Backend/Dockerfile
```

Utiliza las imágenes oficiales de .NET 8:
```text
FROM mcr.microsoft.com/dotnet/aspnet:8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0
```

El proceso se realiza las siguientes acciones:
* restaura las dependencias del proyecto;
* compila el backend;
* publica la aplicación;
* copia los archivos publicados a la imagen final;
* ejecuta la API dentro del contenedor.

---

## Cómo funciona Docker Compose

El archivo ```docker-compose.yml``` se encuentra en la raíz del proyecto.

Define dos servicios principales:
|Servicios|Función|
|---------|-------|
|mysql|Crea y ejecuta la base de datos MySQL|
|backend|Construye y ejecuta la API ASP.NET Core|

Los contenedores creados son:
```text
bibliotecamangas-mysql-tp
bibliotecamangas-backend-tp
```

El backend se conecta a MySQL usando el nombre del servicio definido en Docker Compose:
```text
Server=mysql;Port=3306;Database=BibliotecaMangas;User=root;Password=biblioteca123;
```

---

## Base de datos inicial

La base de datos se inicializa desde el archivo:
```text
mysql-init/01-init.sql
```

Este script crea la base de datos ```BibliotecaMangas```, genera las tablas principales y carga datos iniciales para probar la API.

Tablas incluidas:
* Autores
* Editoriales
* Obras
* Tomos

---

## Capturas de Pantalla
### Repositorio en GitHub
[Repositorio](docs/evidencias/Github.jpg)

### Construcción manual de la imagen Docker
```text
docker build -f BibliotecaMangas.Backend/Dockerfile -t bibliotecamangas-backend:tp1 .
```
[ImagenDocker](docs/evidencias/Docker%20Build.png)

### Construcción y ejecución con Docker Compose
```text
docker compose up --build
```
[dockerCompose](docs/evidencias/Ejecución%20con%20Docker%20Compose.png)

### Contenedores en ejecución
[contenedores](docs/evidencias/contenedores%20en%20ejecucion.png)

### Swagger disponible
[swagger](docs/evidencias/Swagger.jpg)

### Endpoint devolviendo datos
```text
GET /api/Obras/getAllObras
```
[endpoint](docs/evidencias/endpoint%20devolviendo%20datos.jpg)

---

## Comandos Git utilizados

Algunos comandos utilizados durante el desarrollo fueron:
```text
git init
git add .
git commit -m "Estado estable inicial"
git commit -m "Completar endpoint de la API"
git commit -m "Agregar Docker Compose y base MySQL inicial"
git commit -m "Actualizar README con evidencias"
```

Publicación en GitHub:
```text
git remote add origin https//github.com/AdriGimenez/BibliotecaMangas.git
git push origin main
```

---

## Entregables del trabajo práctico

Este proyecto incluye:
* URL del repositorio publicado en GitHub;
* Código fuente completo.
* Dockerfile funcional.
* README documentado.
* Archivo ```docker-compose.yml```
* Script SQL inicial para crear y cargar la base de datos.
* Imagen Docker construida correctamente.
* Contenedor del backend ejecutado correctamente.
* Contenedor MySQL ejecutado correctamente.
* API verificada mediante Swagger.
* Capturas de evidencia del repositorio, ejecución y funcionamiento.

---

## Desarrollo local opcional

La forma recomendada para ejecuta rel proyecto completo es usando Docker Compose.

Si se desea trabajar localmente sin Docker Compose, se puede usar la CLI de ```dotnet```.

Restaurar dependencias:
```text
dotnet restore BibliotecaMangas.sln
```

Compilar la solución:
```text
dotnet build BibliotecaMangas.sln
```

Ejecutar el backend:
```text
dotnet run --project BibliotecaMangas.Backend/BibliotecaMangas.Backend.csproj
```

Importante: para ejecutar el proyecto localmente sin Docker Compose, se necesita tener MySQL disponible y configurar correctamente la cadena de conexión ```CONNECTIONSTRING```.

---

#### Desarrollado por

Adriana Noemí Gimenez

Trabajo Práctico N° 1 - Git y Docker

Materia: Ingeniería de Software.