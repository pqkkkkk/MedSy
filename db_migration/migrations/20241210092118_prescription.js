/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE prescription (
      id INT IDENTITY(1,1) PRIMARY KEY,
      total_price INT NOT NULL,
      created_day DATE NOT NULL,
      consultation_id INT,
      status varchar(10) not null
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
      DROP TABLE IF EXISTS prescription;
    `);
};
