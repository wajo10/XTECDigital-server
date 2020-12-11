--Validar que al crear una evaluacion el rubro no exista ya en ese grupo y que los porcentajes de todas sumen 100
--Validacion de profesores, administradores y estudiantes en el Log In
--Validar que al crear una carpeta no exista una carpeta con el mismo nombre en el mismo grupo.
--Validar que al crear un documento no exista una carpeta con el mismo nombre en el mismo grupo.

CREATE OR ALTER PROCEDURE agregarProfesor @cedula int
AS
Begin
INSERT INTO Profesor values (@cedula);
End;
Go

CREATE OR ALTER PROCEDURE agregarEstudiante @carnet int
AS
Begin
INSERT INTO Estudiantes values (@carnet);
End;
Go

CREATE OR ALTER PROCEDURE agregarAdmin @cedula int
AS
Begin
INSERT INTO Administrador values (@cedula);
End;
Go

--*******************************ADMINISTRADOR******************************************

--Crear curso
CREATE OR ALTER PROCEDURE crearCurso @Codigo varchar(10), @nombre varchar(30), @carrera varchar(30), @creditos int, @habilitado bit, @cedulaAdmin int
AS
BEGIN
	INSERT INTO Curso values (@Codigo, @nombre, @carrera, @creditos, @habilitado, @cedulaAdmin);
END;
GO
Select * from Curso;
--Eliminar curso
CREATE OR ALTER PROCEDURE eliminarCurso @Codigo varchar (10)
AS
BEGIN
	DELETE FROM Curso where codigo = @Codigo;
END;
GO

--Ver todos los cursos que hay en la base de datos
CREATE OR ALTER PROCEDURE verCursos
AS
BEGIN
	SELECT * FROM Curso;
END;
GO

--Ver todos los cursos disponibles
CREATE OR ALTER PROCEDURE verCursosDisponibles
AS
BEGIN
	SELECT * FROM Curso where habilitado = 1;
END;
GO

--Habilitar o deshabilitar un curso
CREATE OR ALTER PROCEDURE habilitar_deshabilitarCurso @nombre varchar(30)
AS
BEGIN
	update Curso set habilitado = habilitado ^ 1 where nombre = @nombre;
END;
GO

--Crea un semestre (1 para el primer semestre, 2 para el segundo semestre y V para el periodo de verano).  
CREATE OR ALTER PROCEDURE crearSemestre @ano int, @periodo int, @cedulaAdmin int
AS
Begin
INSERT INTO Semestre (ano, periodo, cedulaAdmin) values (@ano, @periodo, @cedulaAdmin);
End;
Go

--Creacion de Documentos (Presentaciones, Quizes, Examenes, Proyectos) y Evaluaciones (Examenes 30%, Proyectos 40%, Quizes 30%) al crear el grupo
--Crear carpetas
CREATE OR ALTER PROCEDURE crearCarpeta @nombre varchar(30), @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	insert into Carpetas (nombre, idGrupo) values (@nombre, @idGrupo);
END;
GO


--Eliminar carpetas
CREATE OR ALTER PROCEDURE eliminarCarpeta @nombre varchar(30), @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	delete from Carpetas where nombre = @nombre and idGrupo = @idGrupo;
	delete from Documentos where idCarpeta = (select idCarpeta from Carpetas where nombre = @nombre);
END;
GO


--Crear Documentos
CREATE OR ALTER PROCEDURE crearDocumentos @nombreDocumento varchar(30), @archivo varchar(MAX),@tamano decimal, @nombreCarpeta varchar(30), @idGrupo int, @tipoArchivo varchar (10)
AS
BEGIN
	Declare @idCarpeta int = (select idCarpeta from Carpetas where nombre = @nombreCarpeta and idGrupo = @idGrupo);
	Declare @cantDocu int = (select count(*) from Documentos where idCarpeta = @idCarpeta);
	insert into Documentos(nombre, archivo, tamano, idCarpeta, tipoArchivo) values (@nombreDocumento, Convert(varbinary(MAX), @archivo), @tamano, @idCarpeta, @tipoArchivo);
	update Carpetas set tamano = @cantDocu where idCarpeta =@idCarpeta;
END;
GO


--Eliminar Documentos
CREATE OR ALTER PROCEDURE eliminarDocumentos @nombre varchar(30), @nombreCarpeta varchar(30), @idGrupo int
AS
BEGIN
	delete from Documentos where idCarpeta = (select idCarpeta from Carpetas where nombre = @nombre) and nombre = @nombre;
END;
GO

--Crear un rubro
CREATE OR ALTER PROCEDURE crearRubro @rubro varchar(20), @porcentaje decimal, @codigoCurso varchar (30), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	insert into Rubros(rubro, porcentaje, idGrupo) values (@rubro, @porcentaje, @idGrupo);
END;
GO

--Eliminar un rubro
CREATE OR ALTER PROCEDURE eliminarRubro @rubro varchar(20), @porcentaje decimal, @codigoCurso varchar (30), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	delete from Rubros where rubro = @rubro and porcentaje = @porcentaje and idGrupo = @idGrupo;
END;
GO

--Crear Evaluacion
CREATE OR ALTER PROCEDURE crearEvaluacion @grupal int, @nombre varchar(30), @porcentaje decimal, @fechaInicio datetime, @fechaFin datetime,
@archivo varchar(MAX), @rubro varchar(20), @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	DECLARE @idRubro int = (select idRubro from Rubros where rubro = @rubro and idGrupo = @idGrupo);
	insert into Evaluaciones (grupal, nombre, porcentaje, fechaInicio, fechaFin, archivo, idRubro)
	values (@grupal, @nombre, @porcentaje, @fechaInicio, @fechaFin, Convert(varbinary(MAX), @archivo), @idRubro);
END;
GO

--Eliminar Evaluacion
CREATE OR ALTER PROCEDURE eliminarEvaluacion @nombre varchar (30), @rubro varchar(50), @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	DECLARE @idRubro int = (select idRubro from Rubros where idGrupo = @idGrupo and rubro = @rubro);
	Delete from EvaluacionesEstudiantes where idEvaluacion = (select idEvaluacion from Evaluaciones where idRubro = @idRubro and nombre = @nombre);
	Delete from Evaluaciones where idRubro = @idRubro and nombre = @nombre;
END;
GO

--Establecer los grupos del curso y le crea las carpetas predeterminadas
CREATE OR ALTER PROCEDURE crearGrupo @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	Declare @fechaDefault datetime = getDate();
	insert into Grupo (codigoCurso, numeroGrupo) values (@codigoCurso, @numeroGrupo);
	Execute crearCarpeta @nombre = 'Presentaciones', @codigoCurso = @codigoCurso , @numeroGrupo = @numeroGrupo
	Execute crearCarpeta @nombre = 'Quices', @codigoCurso = @codigoCurso , @numeroGrupo = @numeroGrupo
	Execute crearCarpeta @nombre = 'Examenes', @codigoCurso = @codigoCurso , @numeroGrupo = @numeroGrupo
	Execute crearCarpeta @nombre = 'Proyectos', @codigoCurso = @codigoCurso , @numeroGrupo = @numeroGrupo
	Execute crearRubro @rubro = 'Quices', @porcentaje = 30, @codigoCurso = @codigoCurso, @numeroGrupo = @numeroGrupo;
	Execute crearRubro @rubro = 'Examenes', @porcentaje = 30, @codigoCurso = @codigoCurso, @numeroGrupo = @numeroGrupo;
	Execute crearRubro @rubro = 'Proyectos', @porcentaje = 40, @codigoCurso = @codigoCurso, @numeroGrupo = @numeroGrupo;
END;
GO


--Establecer los profesores del grupo.
CREATE OR ALTER PROCEDURE asignarProfesorGrupo @codigoCurso varchar(10), @numeroGrupo int, @cedulaProfesor int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	insert into ProfesoresGrupo (idGrupo, cedulaProfesor) values (@idGrupo, @cedulaProfesor)
END;
GO

--Eliminar profesor del grupo
CREATE OR ALTER PROCEDURE eliminarProfesorGrupo @codigoCurso varchar(10), @numeroGrupo int, @cedulaProfesor int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	delete from ProfesoresGrupo where idGrupo = @idGrupo and cedulaProfesor = @cedulaProfesor;
END;
GO

--Establecer estudiantes del grupo
CREATE OR ALTER PROCEDURE agregarEstudiantesGrupo @carnet varchar(15), @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	insert into EstudiantesGrupo values (@carnet, @idGrupo);
END;
GO
--Crear semestre cargando tablas de excel a una tabla temporal y luego ejecutar un procedimiento almacenado

--........................................................TRIGGERS........................................................

--Valida que al crear un semestre no exista ya uno con el mismo ano y periodo
Create or Alter Trigger tr_verificarSemestre on Semestre
for Insert
As
IF Exists (select * from Semestre as s join inserted as i on s.ano = i.ano and s.periodo = i.periodo having COUNT(*)>1)
BEGIN
	RAISERROR ('El semestre que intenta crear ya existe en la base de datos.',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que al crear un curso no exista ya
Create or Alter Trigger tr_verificarCurso on Curso
for Insert
As
IF Exists (select * from Curso as c join inserted as i on c.codigo = i.codigo having COUNT(*)>1)
BEGIN
	RAISERROR ('El curso que intenta crear ya existe en la base de datos.',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que no se le agregue el mismo curso al mismo grupo
Create or Alter Trigger tr_verificarGrupo on Grupo
for Insert
As
IF Exists (select * from Grupo as g join inserted as i on g.codigoCurso = i.codigoCurso and g.numeroGrupo = i.numeroGrupo having COUNT(*)>1)
BEGIN
	RAISERROR ('El grupo que intenta crear ya existe en la base de datos.',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go


--Valida que la carpeta que se crea no exista en el mismo grupo
Create or Alter Trigger tr_verificarCarpeta on Carpetas
for Insert
As
IF Exists (select * from Carpetas as c join inserted as i on c.nombre = i.nombre and c.idGrupo = i.idGrupo having COUNT(*)>1)
BEGIN
	RAISERROR ('La carpeta que intenta crear ya existe para ese grupo en la base de datos.',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que el documento que se quiere crear no exista ya en la carpeta
Create or Alter Trigger tr_verificarDocumento on Documentos
for Insert
As
IF Exists (select * from Documentos as d join inserted as i on d.nombre = i.nombre and d.idCarpeta = i.idCarpeta having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe un documento con ese nombre en esta carpeta',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que existan 2 evaluaciones con el mismo rubro en el mismo grupo
Create or Alter Trigger tr_verificarEvaluacion on Evaluaciones
for Insert
As
IF Exists (select * from Evaluaciones as e join inserted as i on e.idRubro = i.idRubro and e.nombre = i.nombre having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe una evaluacion con ese nombre en este grupo',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que no se pueda crear el mismo rubro para un grupo
Create or Alter Trigger tr_verificarRubro on Rubros
for Insert
As
IF Exists (select * from Rubros as r join inserted as i on r.idGrupo = i.idGrupo and r.rubro = i.rubro having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe un rubro con ese nombre en este grupo',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que no se puedan eliminar los rubros creados al inicializar el semestre
Create or Alter Trigger tr_EliminarRubro on Rubros
for delete
As
Declare @rubro varchar(30) = (select rubro from deleted);
If (@rubro = 'Quices' or @rubro = 'Examenes' or @rubro = 'Proyectos')
BEGIN
	RAISERROR ('No se pueden eliminar los rubros principales',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que no se puedan eliminar las carpetas creadas al inicializar el semestre
Create or Alter Trigger tr_EliminarCarpetas on Carpetas
for delete
As
Declare @nombreCarpeta varchar(30) = (select nombre from deleted);
If (@nombreCarpeta = 'Quices' or @nombreCarpeta = 'Examenes' or @nombreCarpeta = 'Proyectos' or @nombreCarpeta = 'Presentaciones')
BEGIN
	RAISERROR ('No se pueden eliminar las carpetas principales',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go


--........................................................TRIGGERS........................................................

--*******************************ADMINISTRADOR******************************************



--*******************************PROFESOR******************************************

--Ver las carpetas de un grupo
CREATE OR ALTER PROCEDURE verCarpetasGrupo @codigoCurso varchar(30), @numeroGrupo int
AS
BEGIN
DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
Select * from Carpetas where idGrupo = @idGrupo;
END;
GO

--Editar carpetas de un grupo
CREATE OR ALTER PROCEDURE editarCarpetaGrupo @nombreCarpeta varchar(30), @nuevoNombre varchar(30), @codigoCurso varchar(30), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	UPDATE Carpetas set nombre = @nuevoNombre where nombre = @nombreCarpeta and idGrupo = @idGrupo;
END;
GO

--Editar documentos (cambiar nombre)
CREATE OR ALTER PROCEDURE editarDocumentos @nombreDocumento varchar (30), @nombreCarpeta varchar(30), @codigoCurso varchar (10), @numeroGrupo int,
@nuevoNombre varchar(30)
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo );
	DECLARE @idCarpeta int = (select idCarpeta from Carpetas where nombre = @nombreCarpeta and idGrupo = @idGrupo);
	Update Documentos set nombre = @nuevoNombre,  fechaSubido = getDate() where nombre = @nombreDocumento;
END;
GO

--Ver los documentos de un grupo de una carpeta especifica
CREATE OR ALTER PROCEDURE verDocumentosGrupo @nombreCarpeta varchar (20), @codigoCurso varchar (10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo );
	DECLARE @idCarpeta int = (select idCarpeta from Carpetas where nombre = @nombreCarpeta and idGrupo = @idGrupo);
	Select * from Documentos where idCarpeta = @idCarpeta
END;
GO

--Ver rubros de un grupo
CREATE OR ALTER PROCEDURE verRubrosGrupo @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo );
	select * from Rubros where idGrupo = @idGrupo;
END;
GO

--Editar los rubros de un grupo
CREATE OR ALTER PROCEDURE editarRubrosGrupo @codigoCurso varchar(10), @numeroGrupo int, @rubro varchar(20), @nuevoRubro varchar(20), @nuevoPorcentaje decimal
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo );
	Update Rubros set rubro = @nuevoRubro, porcentaje = @nuevoPorcentaje where idGrupo = @idGrupo and rubro = @rubro;
END;
GO

--Verificar que los rubros de un grupo sumen 100 en total
CREATE OR ALTER PROCEDURE verificarRubros @codigoCurso varchar(10), @numeroGrupo int
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	DECLARE @total decimal = (Select SUM(porcentaje) from Rubros where idGrupo = @idGrupo);
	If (@total != 100)
	print (0)
	--return 0
	Else
	--return 1
	print (1)
END;
GO

--*SI ES GRUPAL DEBE ASIGNAR LOS GRUPOS DE TRABAJO
--Editar evaluaciones de un grupo
CREATE OR ALTER PROCEDURE editarEvaluacion @nombreEvaluacion varchar (20), @codigoCurso varchar(10), @numeroGrupo int, @rubro varchar(20), 
@nuevoNombre varchar (20), @nuevaFechaInicio datetime, @nuevaFechaFin datetime, @nuevoPorcentaje decimal
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	DECLARE @idRubro int = (select idRubro from Rubros where idGrupo = @idGrupo and rubro = @rubro);
	Update Evaluaciones set nombre = @nuevoNombre, fechaInicio = @nuevaFechaInicio, fechaFin = @nuevaFechaFin, porcentaje = @nuevoPorcentaje
	where nombre = @nombreEvaluacion and idRubro = @idRubro;
END;
GO

--Ver las evaluaciones de un grupo segun su rubro
CREATE OR ALTER PROCEDURE verEvaluacionesPorRubro @codigoCurso varchar(10), @numeroGrupo int, @rubro varchar(20)
AS
BEGIN
	DECLARE @idGrupo int = (select idGrupo from Grupo where codigoCurso = @codigoCurso and numeroGrupo = @numeroGrupo);
	Select e.nombre nombreEvaluacion, e.porcentaje porcentajeEvaluacion, r.porcentaje porcentajeRubro, e.grupal, e.fechaInicio,
	e.fechaFin, e.archivo from Evaluaciones as e
	inner join Rubros as r on e.idRubro = r.idRubro 
	where r.idGrupo = @idGrupo and rubro = @rubro;
END;
GO

--Asignar grupos de trabajo
CREATE OR ALTER PROCEDURE crearGrupoEvaluacion 


/*
execute verEvaluacionesPorRubro @codigoCurso = 'CE1010', @numeroGrupo = 8, @rubro = 'Quices'

execute editarEvaluacion @nombreEvaluacion = 'evaluacion2', @codigoCurso = 'CE1010', @numeroGrupo = 8, @rubro = 'Quices',
@nuevoNombre = 'evaluacion2', @nuevaFechaInicio = '2020-12-09 20:00:00' , @nuevaFechaFin = '2020-12-08 22:00:00', @nuevoporcentaje = 25.50

execute crearEvaluacion @grupal = 0, @nombre = 'examenPrueba', @porcentaje = 20, @fechaInicio = '2020-12-08 21:28:00.000',
@fechaFin = '2020-12-08 21:28:00.000', @archivo = 'pruebaaasdasdagda', @rubro = 'Examenes', @codigoCurso = 'CE1010', @numeroGrupo = 8
select * from Evaluaciones
*/


--Gestion de noticias (visualizar, crear, modificar y eliminar noticias)
--Revisar las evaluaciones (descargar respuesta estudiante, subir comentario, poner nota, subir archivo retroalimentacion)
--Guardar o publicar notas *TRIGGER QUE CREA UNA NOTICIA CUANDO SE PUBLICAN LAS NOTAS
--Reporte de notas *VISTA QUE DETALLE TODAS LAS NOTAS Y CALCULE EL VALOR OBTENIDO PARA CADA RUBRO, ASÍ COMO LA NOTA FINAL CURSO Y CREAR PDF
--Reporte de estudiantes *VISTA CON TODA LA INFORMACION DE LOS ESTUDIANTES DE UN GRUPO Y CREAR PDF

--........................................................TRIGGERS........................................................
--Valida que no se puedan modificar carpetas principales o que se trate de cambiar el nombre por una ya existente
Create or Alter Trigger tr_ActualizarCarpetas on Carpetas
for update
As
Declare @nombreCarpeta varchar(30) = (select nombre from deleted);
If (@nombreCarpeta = 'Quices' or @nombreCarpeta = 'Examenes' or @nombreCarpeta = 'Proyectos' or @nombreCarpeta = 'Presentaciones')
BEGIN
	RAISERROR ('No se pueden alterar las carpetas principales',16,1);
	ROLLBACK TRANSACTION;
	Return;
END;
Else if Exists (select * from Carpetas as c join inserted as i on c.nombre = i.nombre and c.idGrupo = i.idGrupo having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe una carpeta con ese nombre en el grupo',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--Valida que no se pueda cambiar el nombre de un documento por uno que ya existe
Create or Alter Trigger tr_ActualizarDocumentos on Documentos
for update
As
If Exists (select * from Documentos as d join inserted as i on d.nombre = i.nombre and d.idCarpeta = i.idCarpeta having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe un documento con ese nombre en el grupo',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

Create or Alter Trigger tr_ActualizarRubros on Rubros
for update
As
DECLARE @nombRubro varchar (20) = (select rubro from deleted);
DECLARE @nombNuev varchar (20) = (select rubro from inserted);
If Exists (select * from Rubros as r join inserted as i on r.rubro = i.rubro and r.idGrupo = i.idGrupo having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe un rubro con ese nombre en el grupo',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
ELSE IF ((@nombRubro = 'Quices' and @nombNuev != 'Quices') or (@nombRubro = 'Examenes' and @nombNuev != 'Examenes') 
or (@nombRubro = 'Proyectos' and @nombNuev != 'Proyectos'))
BEGIN
	RAISERROR ('No se puede cambiar el nombre de los rubros principales, solo su porcentaje',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

Create or Alter Trigger tr_ActualizarEvaluaciones on Evaluaciones
for update
As
DECLARE @fechaFin datetime = (select fechaFin from inserted);
DECLARE @fechaInicio datetime = (select fechaInicio from inserted);
If Exists (select * from Evaluaciones as e join inserted as i on e.nombre = i.nombre and e.idRubro = i.idRubro having COUNT(*)>1)
BEGIN
	RAISERROR ('Ya existe una evaluacion con ese nombre en el grupo',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
ELSE IF (@fechaFin <= @fechaInicio) 
BEGIN
	RAISERROR ('La fecha de finalizacion debe de ser despues de la fecha de inicio de la prueba',16,1);
	ROLLBACK TRANSACTION;
	Return
END;
Go

--........................................................TRIGGERS........................................................

--*******************************PROFESOR******************************************



--********************************ESTUDIANTE***************************************************

--Ver los cursos a los que pertenece
--Visualizar Carpetas del grupo
--Ver archivos de las carpetas y poder descargarlos
--Enviar evaluaciones *CARGAR ARCHIVO, SI ES GRUPAL CON QUE UNO LO SUBA SE LE SUBE A TODOS LOS DEL GRUPO, PODER DESCARGAR EL ARCHIVO QUE SUBE EL ESTUDIANTE
--Reporte de las notas del curso *SOLO PERMITE VISUALIZAR LA NOTA OBTENIDA POR EL ESTUDIANTE EN ESE GRUPO
--Visualizar noticias *VER LA LISTA DE NOTICIAS DEL GRUPO ORDENADAS POR FECHA