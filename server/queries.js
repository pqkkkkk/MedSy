const db = require("./db.config")
const sql = require('mssql');

const getPatientIdByPrescriptionId = async (prescriptionId) => {
    const query = 'SELECT c.patient_id as userid' +
        ' FROM prescription p JOIN consultation c on p.consultation_id = c.id' +
        ' WHERE p.id = @prescriptionId';
    try {
        const pool = await db.connectDB();
        const result = await pool.request()
            .input('prescriptionId', sql.Int, prescriptionId)
            .query(query);
        return result.recordset[0].userid;
    } catch (error) {
        console.log('Error getting user id by prescription id:', error.message);
    }
}
module.exports = {
    getPatientIdByPrescriptionId
};