/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('doctor_information').del();
  await knex('doctor_information').insert([
    {
      ID: 2,
      Experience_Year: 10,
      Rating: 4.5,
      consultation_price: 500000,
      Speciality: 'Cardiology'
    },
    {
      ID: 3,
      Experience_Year: 5,
      Rating: 5.0,
      consultation_price: 5100000,
      Speciality: 'Cardiology'
    },
    {
      ID: 5,
      Experience_Year: 6,
      Rating: 4.7,
      consultation_price: 550000,
      Speciality: 'Denstistry'
    },
    {
      ID: 6,
      Experience_Year: 2,
      Rating: 4.0,
      consultation_price: 100000,
      Speciality: 'General'
    },
    {
      ID: 7,
      Experience_Year: 7,
      Rating: 5.0,
      consultation_price: 5100000,
      Speciality: 'Neurology'
    },
    {
      ID: 8,
      Experience_Year: 5,
      Rating: 5.0,
      consultation_price: 5100000,
      Speciality: 'Pediatrics'
    },
    {
      ID: 9,
      Experience_Year: 5,
      Rating: 5.0,
      consultation_price: 5100000,
      Speciality: 'Dermatology'
    },
    {
      ID: 10,
      Experience_Year: 5,
      Rating: 5.0,
      consultation_price: 5100000,
      Speciality: 'Pediatrics'
    },
    {
      ID: 11,
      Experience_Year: 5,
      Rating: 5.0,
      consultation_price: 5100000,
      Speciality: 'Dermatology'
    }
  ]);
};
