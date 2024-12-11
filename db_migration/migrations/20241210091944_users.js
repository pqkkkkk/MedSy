/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
    CREATE TABLE users (
      id INT IDENTITY(1,1) PRIMARY KEY,
      username VARCHAR(20) NOT NULL,
      password VARCHAR(50) NOT NULL,
      email VARCHAR(50) NOT NULL,
      fullname NVARCHAR(30) NOT NULL,
      phonenumber VARCHAR(15) NOT NULL,
      avatar_path varchar(100) default  '',
      address NVARCHAR(50),
      birthday DATE,
      role VARCHAR(10),
      gender VARCHAR(10)
    );
    `);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
  DROP TABLE IF EXISTS users;
`);
};
