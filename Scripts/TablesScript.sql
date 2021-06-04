create database UnaPintaDB
go

use UnaPintaDB

create table TiposUsuario(
	Id int primary key identity,
	Descripcion varchar(20) not null
	)
	go;

create table TiposSanguineos(
	Id int primary key identity,
	Descripcion char(3))
	go

create table Usuarios(
	Id int primary key identity,
	Nombre varchar(25) not null,
	Apellido varchar(25) not null,
	Direccion varchar(500),
	Ubicacion varchar(500),
	Sexo bit not null,
	FechaNacimiento datetime not null,
	TipoSanguineo int references TiposSanguineos(Id),
	Email varchar(50) not null,
	Peso float,
	Telefono char(11),
	Usuario varchar(60),
	Contraseña varchar(200),
	PuedeDonar bit not null default '0',
	TipoUsuario int references TiposUsuario(Id))
	go

create table Hemocomponentes(
	Id int primary key,
	Descripcion varchar(50)
	)
	go


create table Solicitudes(
	Id int primary key identity,
	IdSolicitante int references Usuarios(Id),
	IdHemocomponente int references Hemocomponentes(Id),
	Cantidad float,
	Ubicacion varchar(500)
	)
	go

create table Condiciones(
	Id int primary key identity,
	Descripcion varchar(500)
	)
	go


create table ListaEspera(
	Id int primary key identity,
	IdCondicion int references Condiciones(Id),
	IdUsuario int references Usuarios(Id),
	FechaDisponibilidad datetime not null
)
