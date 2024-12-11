/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE prescription_detail (
      id INT IDENTITY(1,1) PRIMARY KEY,
      quantity INT NOT NULL,
      usage NVARCHAR(100) NOT NULL,
      prescription_id INT,
      drug_id INT,
      price int
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
      DROP TABLE IF EXISTS prescription_detail;
    `);
};
