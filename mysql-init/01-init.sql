-- 1. Crear la base de datos si no existe
CREATE DATABASE IF NOT EXISTS BibliotecaMangas;
USE BibliotecaMangas;

-- 2. Crear la tabla Autores
CREATE TABLE Autores (
	AutorId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(80) NOT NULL
);

-- 3. Crear la tabla Editoriales
CREATE TABLE Editoriales (
	EditorialId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(80) NOT NULL,
    Pais VARCHAR(80) NOT NULL
);

-- 4. Crear la tabla Obras
CREATE TABLE Obras (
	ObraId INT PRIMARY KEY AUTO_INCREMENT,
    Titulo VARCHAR(80) NOT NULL,
    AutorId INT,
    EditorialId INT,
    FOREIGN KEY (AutorId) REFERENCES Autores(AutorId),
    FOREIGN KEY (EditorialId) REFERENCES Editoriales(EditorialId)
);

-- 5. Crear la tabla Tomos
CREATE TABLE Tomos (
	TomoId INT PRIMARY KEY AUTO_INCREMENT,
    ObraId INT, 
    NumeroTomo INT NOT NULL,
    FOREIGN KEY (ObraId) REFERENCES Obras(ObraId)
);

-- 6. Insertar Autores
INSERT INTO Autores (Nombre) VALUES
('Takehiko Inoue'),
('Hiro Fujiwara'),
('Tite Kubo'),
('Mon/AntStudio Kim Roah'),
('SIU'),
('Akira Himekara'),
('Dubu/Chugong'),
('Tomohito Oda'),
('Ryoko Kui'),
('Kyosuke Kuromaru'),
('Reki Kawahara'),
('Clamp'),
('Tsugumi Ohba'),
('Natsu Hyuuga');

-- 7. Insertar Editoriales
INSERT INTO Editoriales (Nombre, Pais) VALUES
('IVREA', 'Argentina'),
('Panini', 'Argentina'),
('Panini', 'Mexico');

-- 8. Insertar Obras
INSERT INTO Obras (Titulo, AutorId, EditorialId) VALUES
('Slam Dunk', 1, 1),
('Slam Dunk re:SOURCE', 1, 1),
('Maid Sama', 2, 3),
('Bleach Remix', 3, 1),
('Yo voy a ser la nueva jefa del clan', 4, 1),
('Tower of God', 5, 1),
('The Legend of Zelda: Ocarine of Time', 6, 1),
('Solo Leveling', 7, 1),
('Komi-san no puede comunicarse', 8, 1),
('Dungeon Meshi', 9, 1),
('Amor de Gata', 10, 1),
('Sword Art Online -Progressive-', 11, 1),
('Sword Art Online Mothers of Rosaria', 11, 1),
('Sword Art Online Aincrad', 11, 1),
('Magic Knight Rayearth', 12, 1),
('Magic Knight Rayearth II', 12, 1),
('Bakuman', 13, 1),
('Los diarios de la Boticaria', 14, 2);

-- 9. Insertar Tomos
INSERT INTO Tomos (ObraId, NumeroTomo) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8), (1, 9), (1, 10), (1, 12), (1, 20),
(2, 1),
(3, 1), (3, 2), (3, 3), (3, 4), (3, 6), (3, 7), (3, 8), (3, 9), (3, 10),
(4, 1), (4, 2), (4, 3), (4, 4), (4, 5), (4, 6), (4, 7), (4, 8),
(5, 1), (5, 2), (5, 3), (5, 4),
(6, 1),
(7, 1),
(8, 1), (8, 2), (8, 3), (8, 4), (8, 5), (8, 6), (8, 7), (8, 8), (8, 9), (8, 10), (8, 11),
(9, 1), (9, 2), (9, 3), (9, 4), (9, 5), (9, 6), (9, 7), (9, 8), (9, 9), (9, 10), (9, 11), (9, 12),
(10, 1), (10, 2), (10, 3), (10, 4), (10, 5), (10, 6),
(11, 1), (11, 2), (11, 3),
(12, 1), (12, 2), (12, 3), (12, 4), (12, 5), (12, 6), (12, 7),
(13, 1), (13, 2), (13, 3),
(14, 1), (14, 2),
(15, 1), (15, 2), (15, 3),
(16, 1), (16, 2), (16, 3),
(17, 1), (17, 2), (17, 3), (17, 4), (17, 5), (17, 6), (17, 7), (17, 8), (17, 9), (17, 10),
(18, 1), (18, 2), (18, 3), (18, 4), (18, 5), (18, 6), (18, 7), (18, 8), (18, 9), (18, 10), (18, 11);