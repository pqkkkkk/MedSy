/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('Users').del()
  await knex('Users').insert([
    {
      id: 1,
      Username: "pqkiet854",
      Password: "pqkiet854",
      Email: 'user1@example.com',
      FullName: 'User One',
      PhoneNumber: '123456789',
      Address: '123 Main St',
      Birthday: new Date(1990, 0, 1),
      userRole: "patient",
    },
    {
      id: 2,
      Username: 'user2',
      Password: 'password2',
      Email: 'user2@example.com',
      FullName: 'User Two',
      PhoneNumber: '987654321',
      Address: '456 Main St',
      Birthday: new Date(1992, 1, 2),
      userRole: 'patient'
    },
    {
      id: 3,
      Username: 'user3',
      Password: 'password3',
      Email: 'user3@example.com',
      FullName: 'User Three',
      PhoneNumber: '555555555',
      Address: '789 Main St',
      Birthday: new Date(1994, 2, 3),
      userRole: 'patient'
    },
  ]);
};
