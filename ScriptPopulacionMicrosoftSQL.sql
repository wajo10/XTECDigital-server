Execute agregarProfesor @cedula = '2124';
Execute agregarProfesor @cedula = '1234';
Execute agregarProfesor @cedula = '101013';
Execute agregarProfesor @cedula = '798445';

Execute agregarAdmin @cedula = '11111';
Execute agregarAdmin @cedula = '222222';

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
Execute crearCurso @Codigo = 'CE3101', @nombre = 'BASES DE DATOS', @carrera = 'INGENIERIA EN COMPUTADORES', @creditos = 4, @idSemestre = 1;
Execute crearCurso @Codigo = 'MA0101', @nombre = 'MATEMATICA GENERAL', @carrera = 'INGENIERIA EN COMPUTADORES', @creditos = 2, @idSemestre = 1;
Execute crearCurso @Codigo = 'EL2114', @nombre = 'CIRCUITOS CA', @carrera = 'INGENIERIA ELECTRONICA', @creditos = 4, @idSemestre = 1;

--Creacion y asignacion de grupos
Execute crearGrupo @codigoCurso = 'CE3101', @numeroGrupo = 1;
Execute crearGrupo @codigoCurso = 'CE3101', @numeroGrupo = 2;
Execute crearGrupo @codigoCurso = 'MA0101', @numeroGrupo = 1;
Execute crearGrupo @codigoCurso = 'MA0101', @numeroGrupo = 2;
Execute crearGrupo @codigoCurso = 'MA0101', @numeroGrupo = 3;
Execute crearGrupo @codigoCurso = 'EL2114', @numeroGrupo = 1;

/*
delete from Grupo
select * from Carpetas
DBCC CHECKIDENT ('Grupo', RESEED, 0) 
*/