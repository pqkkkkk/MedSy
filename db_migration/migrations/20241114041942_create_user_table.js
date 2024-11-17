exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE Users( 
            id int PRIMARY KEY,
            Username NVARCHAR(50),
            Password NVARCHAR(50),
            Email NVARCHAR(50),
            FullName NVARCHAR(100), 
            PhoneNumber NVARCHAR(15),
            Address NVARCHAR(255),
            Birthday DATETIME,
            userRole NVARCHAR(20)
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE Users;
    `);
};
