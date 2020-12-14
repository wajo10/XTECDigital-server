module.exports.main = function mainEstudiantes(){

    const url = 'mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority';
 
    var estudiantes = [
        {
            "carnet":2018099536,
            "nombre":"Fabian Ramirez Arrieta",
            "email":"fabian03@estudiantec.cr",
            "telefono":"6062-5648",
            "password":"1234",
            "rol": "estudiante"
        },
        {
            "carnet":2018086985,
            "nombre":"Mariana Vargas Ramirez",
            "email":"marianaVargas@estudiantec.cr",
            "telefono":"8461-7340",
            "password":"1234",
            "rol": "estudiante"
        },
        {
            "carnet":2018319178,
            "nombre":"Mario Alexis Araya Chacón",
            "email":"mario@estudiantec.cr",
            "telefono":"8979-7226",
            "password":"1234",
            "rol": "estudiante"
        },
        {
            "carnet":2018099304,
            "nombre":"Wajib Zaglul Chinchilla",
            "email":"wajo@estudiantec.cr",
            "telefono":"7076-2737",
            "password":"1234",
            "rol": "estudiante"
        }

    ]

    var MongoClient = require('mongodb').MongoClient;
    return dropCollection(MongoClient, url)
    .then( function() {
        return createCollection(MongoClient, url);
    }) 
    .then( function() {
        return fillStudents(MongoClient, url, estudiantes);
    });

    

    /*
    dropCollection(MongoClient, url, estudiantes)
    .then(MongoClient, url => createCollection(MongoClient, url))
    .then(MongoClient, url, estudiantes => fillStudents(MongoClient, url, estudiantes))
    */
}

// mainEstudiantes().catch(console.error);

function dropCollection(MongoClient, url){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.dropCollection("estudiantes", function(err, delOK) {
          if (err) throw err;
          if (delOK) console.log("Collection 'Estudiantes' deleted");
          db.close();
        });
    }).catch(function (err) {}) 
}

function createCollection(MongoClient, url){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.createCollection("estudiantes", function(err, res) {
          if (err) throw err;
          console.log("Collection 'Estudiantes' created!");
          db.close();
        });
    })  
}

function fillStudents(MongoClient, url, students){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.collection("estudiantes").insertMany(students, function(err, res) {
            if (err) throw err;
            console.log("Number of documents inserted: " + res.insertedCount + " to 'Estudiantes'");
            db.close();
          });
    }).catch(function (err) {})
        
}