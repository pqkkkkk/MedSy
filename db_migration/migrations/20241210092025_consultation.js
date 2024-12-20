/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE consultation (
      id INT IDENTITY(1,1) PRIMARY KEY,
      date DATE NOT NULL,
      start_time TIME NOT NULL,
      end_time TIME NOT NULL,
      type VARCHAR(10) NOT NULL,
      status VARCHAR(10) NOT NULL,
      patient_id INT,
      doctor_id INT,
      consultation_result VARCHAR(100),
      reason VARCHAR(100),
      pathology varchar(100),
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
    DROP TABLE IF EXISTS consultation;
    `);
};
