const mysql = require('mysql2');

const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password : 'pqkiet854',
    database : 'medsy'
});

db.connect((error) =>{
    if(error)
    {
        console.log('Error connecting to MedSy database', error.message);
        return;
    }
});

console.log('Connect to MedSy database successfully');

module.exports = db;