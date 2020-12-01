module.exports.main = function mainProfesores(){

    const url = 'mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority';
 
    var profesores = [
        {
            "carnet": 411110758,
            "nombre":"Fabian Ramirez Arrieta",
            "email":"fabian03@estudiantec.cr",
            "telefono":"6062-5648",
            "password":"1234"
        },
        {
            "carnet": 400225348,
            "nombre":"Mariana Vargas Ramirez",
            "email":"marianaVargas@estudiantec.cr",
            "telefono":"8461-7340",
            "password":"1234"
        },
        {
            "carnet": 245248713,
            "nombre":"Mario Alexis Araya Chacón",
            "email":"mario@estudiantec.cr",
            "telefono":"8979-7226",
            "password":"1234"
        },
        {
            "carnet": 278456519,
            "nombre":"Wajib Zaglul Chinchilla",
            "email":"wajo@estudiantec.cr",
            "telefono":"7076-2737",
            "password":"1234"
        }

    ]

    var MongoClient = require('mongodb').MongoClient;
    
    return dropCollection(MongoClient, url)
    .then( function() {
        return createCollection(MongoClient, url);
    }) 
    .then( function() {
        return fillSteachers(MongoClient, url, profesores);
    });

}

// mainProfesores().catch(console.error);

function dropCollection(MongoClient, url){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.dropCollection("profesores", function(err, delOK) {
          if (err) throw err;
          if (delOK) console.log("Collection 'Profesores' deleted");
          db.close();
        });
    }).catch(function (err) {})
}

function createCollection(MongoClient, url){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.createCollection("profesores", function(err, res) {
          if (err) throw err;
          console.log("Collection 'Profesores' created!");
          db.close();
        })
    }) .catch(function (err) {})  
}

function fillSteachers(MongoClient, url, profesores){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.collection("profesores").insertMany(profesores, function(err, res) {
            if (err) throw err;
            console.log("Number of documents inserted: " + res.insertedCount + " to 'Profesores'");
            db.close();
          });
    }).catch(function (err) {})
}