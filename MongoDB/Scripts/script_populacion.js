function main(){
    const estudiantes = require("./script_estudiantes");
    const profesores = require("./script_profesores");
    const SQL = require("./script_SQL");
    estudiantes.main()    
    .then( function() {
        return profesores.main()
    }).then( function() {
        return SQL.main()
    }) 
    
}


main();