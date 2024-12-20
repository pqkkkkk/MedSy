const sql = require('mssql');

const dbConfig = {
    user: 'sa',
    password: 'SqlServer@123',
    server: 'localhost',
    database: 'medsytest',
    options: {
        trustServerCertificate: true
    },
};

const connectDB = async () => {
    try {
        const pool = await sql.connect(dbConfig);
        console.log('Connected to MedSy database successfully');
        return pool;
    } catch (error) {
        console.log('Error connecting to MedSy database:', error.message);
    }
};

module.exports ={
    connectDB
};