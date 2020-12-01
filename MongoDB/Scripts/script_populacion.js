async function main(){
    const estudiantes = require("./script_estudiantes");
    const profesores = require("./script_profesores");
    await estudiantes.main().catch(console.error);;
    await profesores.main().catch(console.error);;
    
}


main().catch(console.error);