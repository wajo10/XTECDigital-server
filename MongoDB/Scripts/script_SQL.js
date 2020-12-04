const axios = require('axios')




const url = 'mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority';
var MongoClient = require('mongodb').MongoClient;
module.exports.main = function mainEstudiantes(){
    return llenarEstudiantes().then( function() {
      return llenarProfesores()
    });

}


function llenarEstudiantes(){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.collection("estudiantes").find({}).toArray(function(err, result) {
            if (err) throw err;
            for(document in result){
              axios
                .post('https://localhost:5001/Usuario/agregarEstudianteSQL', {
                  carnet: result[document].carnet
                })
                .then(res => {
                  console.log(`statusCode: ${res.statusCode}`)
                  console.log(res)
                })
                .catch(error => {
                  console.error(error)
                 })

            }
            db.close();
          });
    })
}

function llenarProfesores(){
  return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
  .then(db =>{
      var dbo = db.db("Usuarios");
      dbo.collection("profesores").find({}).toArray(function(err, result) {
          if (err) throw err;
          for(document in result){
            axios
              .post('https://localhost:5001/Usuario/agregarProfesorSQL', {
                cedula: result[document].cedula
              })
              .then(res => {
                console.log(`statusCode: ${res.statusCode}`)
                console.log(res)
              })
              .catch(error => {
                console.error(error)
               })
          }
          db.close();
        });
  })
}