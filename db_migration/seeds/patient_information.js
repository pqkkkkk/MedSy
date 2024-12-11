/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('patient_information').del();
  await knex('patient_information').insert([
    {id: 1,health_insurance: true},
    {id: 4, health_insurance: true},
  ]);
};
