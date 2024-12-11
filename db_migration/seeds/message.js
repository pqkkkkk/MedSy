/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('message').del();
  //await knex.raw('DBCC CHECKIDENT("message", RESEED, 0)');
  await knex('message').insert([
    {
      content : "Hello",
      sender_Id : 2,
      receiver_Id : 1
    },
    {
      content : "How can I help you?",
      sender_Id : 2,
      receiver_Id : 1
    },
    {
      content : "Thanks",
      sender_Id : 1,
      receiver_Id : 2
    },
    {
      content : "How can I help you?",
      sender_Id : 3,
      receiver_Id : 1
    },
    {
      content : "Hello",
      sender_Id : 3,
      receiver_Id : 1
    },
    {
      content : "Thanks",
      sender_Id : 1,
      receiver_Id : 3
    }
  ]);
};
