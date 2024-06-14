CREATE DATABASE Colegio;

USE Colegio;

-- Tabla de profesores
CREATE TABLE Profesor (
    id_profesor INT PRIMARY KEY IDENTITY(1, 1),
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    genero VARCHAR(100) NOT NULL
);

-- Tabla de alumnos
CREATE TABLE Alumno (
    id_alumno INT PRIMARY KEY IDENTITY(1, 1),
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    genero VARCHAR(25)
);

-- Tabla de grados
CREATE TABLE Grado (
    id_grado INT PRIMARY KEY IDENTITY(1, 1),
    nombre_grado VARCHAR(50) NOT NULL,
    id_profesor INT,
    CONSTRAINT fk_profesor
        FOREIGN KEY (id_profesor) 
        REFERENCES Profesor(id_profesor)
);

-- Tabla de matrículas
CREATE TABLE Matricula (
    id_matricula INT PRIMARY KEY IDENTITY(1, 1),
    id_alumno INT,
    id_grado INT,
    fecha_matricula DATE NOT NULL,
	seccion varchar(5),
    CONSTRAINT fk_alumno
        FOREIGN KEY (id_alumno) 
        REFERENCES Alumno(id_alumno),
    CONSTRAINT fk_grado
        FOREIGN KEY (id_grado) 
        REFERENCES Grado(id_grado)
);