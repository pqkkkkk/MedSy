/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  CREATE TABLE message (
      id INT IDENTITY(1,1) PRIMARY KEY,
      sender_id INT,
      receiver_id INT,
      content NVARCHAR(100) NOT NULL
    );
`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
      DROP TABLE IF EXISTS message;
    `);
};
