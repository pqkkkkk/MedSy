/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('management').del();
  //await knex.raw('DBCC CHECKIDENT("management", RESEED, 0)');
  await knex('management').insert([
    {
      doctor_Id : 2,
      patient_Id : 4,
      doctor_New_Message : false,
      patient_New_Message : false,
    },
    {
      doctor_Id : 2,
      patient_Id : 1,
      doctor_New_Message : false,
      patient_New_Message : false,
    },
    {
      doctor_Id : 3,
      patient_Id : 1,
      doctor_New_Message : false,
      patient_New_Message : false,
    },
    {
      doctor_Id : 2,
      patient_Id : 13,
      doctor_New_Message : false,
      patient_New_Message : false,
    },{
      doctor_Id : 2,
      patient_Id : 14,
      doctor_New_Message : false,
      patient_New_Message : false,
    },{
      doctor_Id : 2,
      patient_Id : 15,
      doctor_New_Message : false,
      patient_New_Message : false,
    },{
      doctor_Id : 2,
      patient_Id : 16,
      doctor_New_Message : false,
      patient_New_Message : false,
    },{
      doctor_Id : 2,
      patient_Id : 17,
      doctor_New_Message : false,
      patient_New_Message : false,
    },
  ]);
};
