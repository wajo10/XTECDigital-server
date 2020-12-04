--Creacion de la base de datos
--create database baseXTecDigital

--Creacion de las tablas de la base de datos
create table Administrador (
	cedula int,
	primary key (cedula)
);

create table Semestre (
	idSemestre int identity(1,1),
	ano int not null,
	periodo int not null,
	cedulaAdmin int not null,
	primary key (idSemestre)
);

create table Curso (
	codigo varchar(10),
	nombre varchar(30) not null unique,
	carrera varchar(30) not null,
	creditos int not null,
	idSemestre int not null,
	primary key (codigo)
);

create table Estudiantes (
	carnet int,
	primary key (carnet)
);

create table Profesor(
	cedula int,
	primary key (cedula)
);

create table ProfesoresCurso(
	codigoCurso varchar(10),
	cedulaProfesor int,
	primary key(codigoCurso, cedulaProfesor)
);

create table Grupo(
	idGrupo int identity(1,1),
	codigoCurso varchar (10) not null,
	numeroGrupo int not null,
	primary key (idGrupo)
);

create table ProfesoresGrupo (
	idGrupo int not null,
	cedulaProfesor int not null,
	primary key (idGrupo, cedulaProfesor)
);

create table EstudiantesGrupo(
	carnetEstudiante int not null,
	idGrupo int not null,
	primary key (carnetEstudiante, idGrupo)
);

create table Carpetas(
	idCarpeta int identity(1,1),
	nombre varchar(30) not null,
	tamano int default 0,
	fecha datetime default getDate(),
	idGrupo int not null,
	primary key (idCarpeta)
);

create table Documentos(
	idDocumento int identity(1,1),
	nombre varchar (30) not null,
	archivo varbinary(max),
	tamano decimal default 0,
	fechaSubido datetime default getDate(),
	idCarpeta int not null,
	primary key (idDocumento)
);

create table Noticias(
	idNoticia int identity (1,1),
	titulo varchar (30) not null,
	mensaje varchar (max) not null,
	fecha datetime default getDate(),
	idGrupo int not null,
	primary key (idNoticia)
);

create table Evaluaciones(
	idEvaluacion int identity (1,1),
	rubro varchar (30) not null,
	porcentaje decimal not null,
	grupal bit default 0,
	fechaInicio datetime not null,
	fechaFin dateTime not null,
	archivo varbinary(max) not null,
	idGrupo int not null,
	primary key (idEvaluacion)
);

create table EvaluacionesEstudiantes (
	carnet int not null,
	idEvaluacion int not null,
	grupo int default 0,
	nota decimal,
	comentario varchar (250),
	archivoRetroalimentacion varbinary(max),
	archivoSolucion varbinary(max),
	primary key (carnet, idEvaluacion)
);

--Modificaciones de tablas
Alter table Semestre
Add constraint FK_Admin
foreign key (cedulaAdmin) references Administrador (cedula);

Alter table Curso
Add constraint FK_Semestre
foreign key (idSemestre) references Semestre (idSemestre);

Alter table Grupo
Add constraint FK_codigoCurso
foreign key (codigoCurso) references Curso (codigo);

Alter table Carpetas
Add constraint FK_idGrupo
foreign key (idGrupo) references Grupo (idGrupo);

Alter table Documentos
Add constraint FK_idCarpeta
foreign key (idCarpeta) references Carpetas (idCarpeta);

Alter table Noticias
Add constraint FK_idGrupoNoticias
foreign key (idGrupo) references Grupo (idGrupo);

Alter table Evaluaciones
Add constraint FK_idGrupoEvaluaciones
foreign key (idGrupo) references Grupo (idGrupo);

--TABLA PARA PROBAR USUARIOS
create table pruebaUsuarios(
	nombre varchar(30),
	usuario varchar(30),
	contrasena varchar(30),
	rol varchar(30),
	primary key (usuario)
);

insert into pruebaUsuarios values ('Mario','mario123', 'mario123', 'administrador');
insert into pruebaUsuarios values ('Wajib','wajo123', 'wajo10', 'profesor');
insert into pruebaUsuarios values ('Mariana','mari123', 'mari12345', 'estudiante');
insert into pruebaUsuarios values ('Fabian','fabian123', 'fabian123', 'estudiante');

select * from pruebaUsuarios;