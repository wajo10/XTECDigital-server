const url = 'mongodb+srv://admin:admin@usuarios.ozlkz.mongodb.net/Usuarios?retryWrites=true&w=majority';
var MongoClient = require('mongodb').MongoClient;
module.exports.main = function mainEstudiantes(){
    return llenarEstudiantes();

}


function llenarEstudiantes(){
    return MongoClient.connect(url, {useNewUrlParser: true, useUnifiedTopology: true})
    .then(db =>{
        var dbo = db.db("Usuarios");
        dbo.collection("estudiantes").find({}, {'nombre':1,'_id':0}).toArray(function(err, result) {
            if (err) throw err;
            for(document in result){
                console.log(result[document].carnet);
            }
            db.close();
          });
    }).catch(function (err) {})
}

function llenarProfesores(){

}