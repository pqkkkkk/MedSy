/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE drug (
      id INT IDENTITY(1,1) PRIMARY KEY,
      name NVARCHAR(50) NOT NULL,
      unit NVARCHAR(10) NOT NULL,
      price INT NOT NULL,
      quantity INT NOT NULL,
      manufacturing_date DATE NOT NULL,
      expiry_date DATE NOT NULL,
      drug_type NVARCHAR(30) NOT NULL
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
      DROP TABLE IF EXISTS drug;
    `);
};
