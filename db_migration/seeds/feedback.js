/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('feedback').del();
  //await knex.raw('DBCC CHECKIDENT("feedback", RESEED, 0)');
  await knex('feedback').insert([
    {
      Doctor_ID: 2,
      Patient_ID : 1,
      Content : "Good!",
      Rating : 5,
    },
    {
      Doctor_ID : 2,
      Patient_ID : 1,
      Content : "So Good!",
      Rating : 4,
    },
    {
      Doctor_ID : 2,
      Patient_ID : 4,
      Content : "Excellent service, highly recommend!",
      Rating : 5,
    },
    {
      Doctor_ID : 2,
      Patient_ID : 4,
      Content : "Very knowledgeable and caring.",
      Rating : 4,
    },
    {
      Doctor_ID : 3,
      Patient_ID : 1,
      Content : "Wait time was a bit long, but the doctor was great.",
      Rating : 3,
    },
    {
      Doctor_ID : 3,
      Patient_ID : 1,
      Content : "I felt very comfortable and well taken care of.",
      Rating : 5,
    },
    {
      Doctor_ID : 3,
      Patient_ID : 4,
      Content : "Not satisfied with the treatment.",
      Rating : 2,
    },
  ]);
};
