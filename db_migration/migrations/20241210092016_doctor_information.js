/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE doctor_information (
      id INT PRIMARY KEY,
      experience_year INT NOT NULL,
      speciality VARCHAR(20) NOT NULL,
      consultation_price INT NOT NULL,
      rating float default 0
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
    DROP TABLE IF EXISTS doctor_information;
    `);
};
