--Creacion de la base de datos
create database baseXTecDigital


--Creacion de las tablas de la base de datos
create table Administrador (
	cedula int,
	primary key (cedula)
);


create table Semestre (
	idSemestre int identity(1,1),
	ano int not null,
	periodo varchar(1) not null,
	cedulaAdmin int not null,
	primary key (idSemestre)
);


create table Curso (
	codigo varchar(10) not null,
	nombre varchar(50) not null unique,
	carrera varchar(50) not null,
	creditos int not null,
	habilitado bit default 1,
	idAdministrador int not null,
	primary key (codigo)
);


create table CursosPorSemestre (
	idSemestre int not null,
	codigoCurso varchar(10) not null,
	primary key (idSemestre, codigoCurso)
);


create table Estudiantes (
	carnet varchar(15),
	primary key (carnet)
);


create table Profesor(
	cedula int,
	primary key (cedula)
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
	carnetEstudiante varchar(15) not null,
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
	tipoArchivo varchar (10),
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

create table Rubros(
	idRubro int identity (1,1),
	rubro varchar (20),
	porcentaje decimal,
	idGrupo int,
	primary key (idRubro)
);

create table Evaluaciones(
	idEvaluacion int identity (1,1),
	grupal bit default 0,
	nombre varchar(20),
	porcentaje decimal not null,
	fechaInicio datetime not null,
	fechaFin dateTime not null,
	archivo varbinary(max),
	idRubro int not null,
	primary key (idEvaluacion)
);


create table EvaluacionesEstudiantes (
	carnet varchar(15) not null,
	idEvaluacion int not null,
	grupo int default 0,
	nota decimal,
	comentario varchar (300),
	archivoRetroalimentacion varbinary(max),
	archivoSolucion varbinary(max),
	primary key (carnet, idEvaluacion)
);

--Modificaciones de tablas
Alter table Semestre
Add constraint FK_Admin
foreign key (cedulaAdmin) references Administrador (cedula);

Alter table Curso
Add constraint FK_IdAdministrador
foreign key (idAdministrador) references Administrador (cedula);


Alter table CursosPorSemestre
Add constraint FK_idSemestrePorCurso
foreign key (idsemestre) references Semestre (idSemestre);

Alter table CursosPorSemestre
Add constraint FK_codigoCursoPorSemestre
foreign key (codigoCurso) references Curso (codigo);

Alter table ProfesoresGrupo
Add constraint FK_idGrupoProfesores
foreign key (idGrupo) references Grupo (idGrupo);

Alter table ProfesoresGrupo
Add constraint FK_cedulaProfesores
foreign key (cedulaProfesor) references Profesor (cedula);

Alter table EstudiantesGrupo
Add constraint FK_carnetEstudianteGrupo
foreign key (carnetEstudiante) references Estudiantes (carnet);

Alter table Carpetas
Add constraint FK_idGrupo
foreign key (idGrupo) references Grupo (idGrupo);

Alter table Documentos
Add constraint FK_idCarpeta
foreign key (idCarpeta) references Carpetas (idCarpeta);

Alter table Noticias
Add constraint FK_idGrupoNoticias
foreign key (idGrupo) references Grupo (idGrupo);

Alter table Rubros
Add constraint FK_idGrupoRubro
foreign key (idGrupo) references Grupo (idGrupo);

Alter table Evaluaciones
Add constraint FK_idRubro
foreign key (idRubro) references Rubros (idRubro);

Alter table EvaluacionesEstudiantes
Add constraint FK_idEvaluacion
foreign key (idEvaluacion) references Evaluaciones (idEvaluacion);

Alter table EvaluacionesEstudiantes
Add constraint FK_idCarnetEvaluacion
foreign key (carnet) references Estudiantes (carnet);



