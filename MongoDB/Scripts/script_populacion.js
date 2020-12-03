function main(){
    const estudiantes = require("./script_estudiantes");
    const profesores = require("./script_profesores");
    estudiantes.main()    
    .then( function() {
        profesores.main();
    }) 
    
}


main();