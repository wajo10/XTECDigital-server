Execute agregarProfesor @cedula = '2124';
Execute agregarProfesor @cedula = '1234';
Execute agregarProfesor @cedula = '101013';
Execute agregarProfesor @cedula = '798445';

Execute agregarAdmin @cedula = '0';
Execute agregarAdmin @cedula = '222222';
Execute agregarAdmin @cedula = '3333334';


Execute agregarEstudiante @carnet = '1010212';
Execute agregarEstudiante @carnet = '7524523';
Execute agregarEstudiante @carnet = '535434';
Execute agregarEstudiante @carnet = '453343';
Execute agregarEstudiante @carnet = '696969';
Execute agregarEstudiante @carnet = '34312';
Execute agregarEstudiante @carnet = '674536453';
Execute agregarEstudiante @carnet = '645679896';
Execute agregarEstudiante @carnet = '14453544';

--Creacion semestre
Execute crearSemestre @ano = '2021', @periodo = 1, @cedulaAdmin = '11111';

--Creacion de cursos
Execute crearCurso @Codigo = 'CE3101', @nombre = 'BASES DE DATOS', @carrera = 'INGENIERIA EN COMPUTADORES', @creditos = 4, @habilitado = 1, @cedulaAdmin = 11111;
Execute crearCurso @Codigo = 'MA0101', @nombre = 'MATEMATICA GENERAL', @carrera = 'INGENIERIA EN COMPUTADORES', @creditos = 2, @habilitado = 1, @cedulaAdmin = 11111;
Execute crearCurso @Codigo = 'EL2114', @nombre = 'CIRCUITOS CA', @carrera = 'INGENIERIA ELECTRONICA', @creditos = 4, @habilitado = 1, @cedulaAdmin = 11111;
Execute crearCurso @Codigo = 'CE1010', @nombre = 'intro y taller', @carrera = 'INGENIERIA EN COMPUTADORES', @creditos = 4, @habilitado = 1, @cedulaAdmin = 11111;

--Creacion y asignacion de grupos
Execute crearGrupo @codigoCurso = 'CE3101', @numeroGrupo = 1;
Execute crearGrupo @codigoCurso = 'CE3101', @numeroGrupo = 2;
Execute crearGrupo @codigoCurso = 'MA0101', @numeroGrupo = 1;
Execute crearGrupo @codigoCurso = 'MA0101', @numeroGrupo = 2;
Execute crearGrupo @codigoCurso = 'MA0101', @numeroGrupo = 3;
Execute crearGrupo @codigoCurso = 'EL2114', @numeroGrupo = 1;

--Asignacion de profesores a los grupos
Execute asignarProfesorGrupo @codigoCurso = 'CE3101', @numeroGrupo = 1, @cedulaProfesor = '1234'; --Hay que cambiar la cedula por una que exista

--Asignacion de estudiantes a los grupos
Execute agregarEstudiantesGrupo @carnet = '1010212', @codigoCurso = 'CE3101', @numeroGrupo = 1;
Execute agregarEstudiantesGrupo @carnet = '14453544', @codigoCurso = 'CE1010', @numeroGrupo = 8;
Execute agregarEstudiantesGrupo @carnet = '34312', @codigoCurso = 'CE1010', @numeroGrupo = 8;
Execute agregarEstudiantesGrupo @carnet = '453343', @codigoCurso = 'CE1010', @numeroGrupo = 8;
Execute agregarEstudiantesGrupo @carnet = '696969', @codigoCurso = 'CE1010', @numeroGrupo = 8;
Execute agregarEstudiantesGrupo @carnet = '7524523', @codigoCurso = 'CE1010', @numeroGrupo = 8;


--Agregar estudiantes a una evaluacion

--Creacion de una evaluacion grupal
Execute crearEvaluacion @grupal = 1, @nombre = 'Evaluacion grupal', @porcentaje = 15, @fechaInicio = '2020-12-09 22:55:13.653', 
@fechaFin = '2020-12-09 22:55:13.653', @archivo = 'archivo prueba', @rubro = 'Quices', @codigoCurso = 'CE1010', @numeroGrupo = 8;

--Evaluacion para probar el trigger de publicar notas * FALTA AGREGAR ESTUDIANTES A ESTA EVALUACION PARA PROBAR
Execute crearEvaluacion @grupal = 0, @nombre = 'Evaluacion trigger', @porcentaje = 25, @fechaInicio = '2020-12-09 22:55:13.653', 
@fechaFin = '2020-12-09 22:56:13.653', @archivo = 'archivo prueba', @rubro = 'Quices', @codigoCurso = 'CE1010', @numeroGrupo = 8;


select * from Evaluaciones

delete Evaluaciones where idEvaluacion = 8

--Agregar estudiantes a una evaluacion grupal
execute agregarEstudianteEvaluacionGrupal @carnetEstudiante = '14453544', @idEvaluacion = 6, @numeroGrupoEvaluacion = 1;
execute agregarEstudianteEvaluacionGrupal @carnetEstudiante = '34312', @idEvaluacion = 6, @numeroGrupoEvaluacion = 1;
execute agregarEstudianteEvaluacionGrupal @carnetEstudiante = '453343', @idEvaluacion = 6, @numeroGrupoEvaluacion = 1;
execute agregarEstudianteEvaluacionGrupal @carnetEstudiante = '674536453', @idEvaluacion = 6, @numeroGrupoEvaluacion = 2;
execute agregarEstudianteEvaluacionGrupal @carnetEstudiante = '645679896', @idEvaluacion = 6, @numeroGrupoEvaluacion = 2;

--Revisar evaluacion grupal
execute revisarEvaluacion @carnet = '674536453', @idEvaluacion = 6, @nota = 100, @comentario = 'excelente trabajo muchachos', 
@archivoRetroalimentacion = 'archivo de prueba'


/*
select * from Evaluaciones
DBCC CHECKIDENT ('Evaluaciones', RESEED, 0) 
select * from Carpetas
*/