--Validar que al crear una carpeta no exista una carpeta con el mismo nombre en el mismo grupo.
--Validar que al crear un documento no exista una carpeta con el mismo nombre en el mismo grupo.
--Validar que al crear una evaluacion el rubro no exista ya en ese grupo y que los porcentajes de todas sumen 100
--Validacion de profesores, administradores y estudiantes en el Log In

CREATE OR ALTER PROCEDURE verUsuarios @rol varchar(30)
AS
Begin
SELECT * FROM pruebaUsuarios where rol = @rol
End
Go


CREATE OR ALTER PROCEDURE crearUsuario @nombre varchar(30), @nombreusuario varchar(30), @contra varchar(30), @rol varchar(30)
AS
Begin
INSERT INTO pruebaUsuarios values (@nombre, @nombreusuario, @contra, @rol);
End;
Go

CREATE OR ALTER PROCEDURE agregarProfesor @cedula int
AS
Begin
INSERT INTO Profesor values (@cedula);
End;
Go

--*******************************ADMINISTRADOR******************************************

--gestionar (visualizar, crear o deshabilitar) la lista de cursos genérica que utiliza el sistema.
--Crear semestre (1 para el primer semestre, 2 para el segundo semestre y V para el periodo de verano).  
--Establecerse los cursos que serán impartidos en el semestre.
--Establecer los grupos del curso.
--Creacion de Documentos (Presentaciones, Quizes, Examenes, Proyectos) y Evaluaciones (Examenes 30%, Proyectos 40%, Quizes 30%) al crear el grupo
--Establecer los profesores del grupo.
--Establecer estudiantes del grupo
--Crear semestre cargando tablas de excel a una tabla temporal y luego ejecutar un procedimiento almacenado

--*******************************ADMINISTRADOR******************************************



--*******************************PROFESOR******************************************

--Gestion de carpetas (visualizar, editar, agregar o eliminar carpetas en el grupo) * NO PUEDE ELIMINAR LAS CREADAS POR EL SEMESTRE
--Gestion de documentos (visualizar, editar, agregar o eliminar documentos en el grupo)
--Gestion de rubros (visualizar, editar, agregar o eliminar rubros en el grupo) *LA SUMA DE TODOS DEBE DAR 100%
--Gestion de evaluaciones (visualizar, editar, agregar o eliminar evaluaciones en el grupo)*SI ES GRUPAL DEBE ASIGNAR LOS GRUPOS DE TRABAJO
--Gestion de noticias (visualizar, crear, modificar y eliminar noticias)
--Revisar las evaluaciones (descargar respuesta estudiante, subir comentario, poner nota, subir archivo retroalimentacion)
--Guardar o publicar notas *TRIGGER QUE CREA UNA NOTICIA CUANDO SE PUBLICAN LAS NOTAS
--Reporte de notas *VISTA QUE DETALLE TODAS LAS NOTAS Y CALCULE EL VALOR OBTENIDO PARA CADA RUBRO, ASÍ COMO LA NOTA FINAL CURSO Y CREAR PDF
--Reporte de estudiantes *VISTA CON TODA LA INFORMACION DE LOS ESTUDIANTES DE UN GRUPO Y CREAR PDF

--*******************************PROFESOR******************************************



--********************************ESTUDIANTE***************************************************

--Ver los cursos a los que pertenece
--Visualizar Carpetas del grupo
--Ver archivos de las carpetas y poder descargarlos
--Enviar evaluaciones *CARGAR ARCHIVO, SI ES GRUPAL CON QUE UNO LO SUBA SE LE SUBE A TODOS LOS DEL GRUPO, PODER DESCARGAR EL ARCHIVO QUE SUBE EL ESTUDIANTE
--Reporte de las notas del curso *SOLO PERMITE VISUALIZAR LA NOTA OBTENIDA POR EL ESTUDIANTE EN ESE GRUPO
--Visualizar noticias *VER LA LISTA DE NOTICIAS DEL GRUPO ORDENADAS POR FECHA
