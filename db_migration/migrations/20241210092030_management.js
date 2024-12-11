/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE management (
      id INT IDENTITY(1,1) PRIMARY KEY,
      doctor_id INT,
      patient_id INT,
      patient_new_message BIT DEFAULT 0,
      doctor_new_message BIT DEFAULT 0
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
      DROP TABLE IF EXISTS management;
    `);
};
