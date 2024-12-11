/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE feedback (
      id INT IDENTITY(1,1) PRIMARY KEY,
      patient_id INT,
      doctor_id INT,
      content NVARCHAR(100),
      rating float
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
      DROP TABLE IF EXISTS feedback;
    `);
};
