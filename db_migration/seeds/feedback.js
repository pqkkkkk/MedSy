/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('feedback').del()
  await knex.raw('DBCC CHECKIDENT("feedback", RESEED, 0)');

  await knex('feedback').insert([
    {
      patient_id: 1,
      doctor_id: 2,
      content: "Very professional doctor",
      rating: 4.8,
    },
    {
      patient_id: 1,
      doctor_id: 3,
      content: "Helpful and attentive",
      rating: 4.5,
    },
    {
      patient_id: 4,
      doctor_id: 2,
      content: "Quick diagnosis",
      rating: 4.7,
    },
    {
      patient_id: 4,
      doctor_id: 3,
      content: "Kind and patient",
      rating: 5.0,
    },
    {
      patient_id: 1,
      doctor_id: 2,
      content: "Explained everything well",
      rating: 4.6,
    },
    {
      patient_id: 4,
      doctor_id: 3,
      content: "Good!",
      rating: 5.0,
    },
    {
      patient_id: 1,
      doctor_id: 3,
      content: "Excellent!",
      rating: 4.9,
    },
  ]);
};
