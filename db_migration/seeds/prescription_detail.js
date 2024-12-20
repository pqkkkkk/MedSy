/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('prescription_detail').del()
  await knex('prescription_detail').insert([
    {
      quantity: 24,
      usage: 'Take 2 tablets per day',
      prescription_id: 1,
      drug_id: 1,
      price: 120000,
    },
    {
      quantity: 25,
      usage: 'Take 1 tablet per day',
      prescription_id: 2,
      drug_id: 2,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 19,
      usage: 'Take 2 tablets per day',
      prescription_id: 3,
      drug_id: 3,
      price: 95000, // 19 * 5000
    },
    {
      quantity: 23,
      usage: 'Take 2 tablets per day',
      prescription_id: 4,
      drug_id: 4,
      price: 350000, // 23 * 15000
    },
    {
      quantity: 30,
      usage: 'Take 3 tablets per day',
      prescription_id: 5,
      drug_id: 6,
      price: 180000, // 30 * 6000
    },
    {
      quantity: 36,
      usage: 'Take 3 tablets per day',
      prescription_id: 6,
      drug_id: 6,
      price: 180000, // 36 * 5000
    },
    {
      quantity: 22,
      usage: 'Take 2 tablets per day',
      prescription_id: 7,
      drug_id: 7,
      price: 220000, // 22 * 10000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 8,
      drug_id: 8,
      price: 300000, // 20 * 15000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 9,
      drug_id: 9,
      price: 150000, // 25 * 6000
    },
    {
      quantity: 27,
      usage: 'Take 3 tablets per day',
      prescription_id: 10,
      drug_id: 10,
      price: 270000, // 27 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 1 tablet per day',
      prescription_id: 11,
      drug_id: 2,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 12,
      drug_id: 6,
      price: 200000, // 20 * 10000
    },
    {
      quantity: 30,
      usage: 'Take 2 tablets per day',
      prescription_id: 13,
      drug_id: 8,
      price: 300000, // 30 * 10000
    },
    {
      quantity: 30,
      usage: 'Take 3 tablets per day',
      prescription_id: 14,
      drug_id: 6,
      price: 180000, // 30 * 6000
    },
    {
      quantity: 22,
      usage: 'Take 2 tablets per day',
      prescription_id: 15,
      drug_id: 7,
      price: 220000, // 22 * 10000
    },
    {
      quantity: 23,
      usage: 'Take 2 tablets per day',
      prescription_id: 16,
      drug_id: 3,
      price: 230000, // 23 * 10000
    },
    {
      quantity: 31,
      usage: 'Take 2 tablets per day',
      prescription_id: 17,
      drug_id: 10,
      price: 310000, // 31 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 18,
      drug_id: 9,
      price: 150000, // 25 * 6000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 19,
      drug_id: 6,
      price: 200000, // 20 * 10000
    },
    {
      quantity: 27,
      usage: 'Take 3 tablets per day',
      prescription_id: 20,
      drug_id: 10,
      price: 270000, // 27 * 10000
    },
    {
      quantity: 30,
      usage: 'Take 3 tablets per day',
      prescription_id: 21,
      drug_id: 6,
      price: 180000, // 30 * 6000
    },
    {
      quantity: 26,
      usage: 'Take 2 tablets per day',
      prescription_id: 22,
      drug_id: 10,
      price: 260000, // 26 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 23,
      drug_id: 9,
      price: 150000, // 25 * 6000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 24,
      drug_id: 8,
      price: 300000, // 20 * 15000
    },
    {
      quantity: 25,
      usage: 'Take 1 tablet per day',
      prescription_id: 25,
      drug_id: 2,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 22,
      usage: 'Take 2 tablets per day',
      prescription_id: 26,
      drug_id: 7,
      price: 220000, // 22 * 10000
    },
    {
      quantity: 31,
      usage: 'Take 2 tablets per day',
      prescription_id: 27,
      drug_id: 10,
      price: 310000, // 31 * 10000
    },
    {
      quantity: 34,
      usage: 'Take 2 tablets per day',
      prescription_id: 28,
      drug_id: 5,
      price: 170000, // 34 * 5000
    },
    {
      quantity: 28,
      usage: 'Take 2 tablets per day',
      prescription_id: 29,
      drug_id: 4,
      price: 280000, // 28 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 30,
      drug_id: 9,
      price: 150000, // 25 * 6000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 31,
      drug_id: 10,
      price: 200000, // 20 * 10000
    },
    {
      quantity: 30,
      usage: 'Take 3 tablets per day',
      prescription_id: 32,
      drug_id: 6,
      price: 180000, // 30 * 6000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 33,
      drug_id: 8,
      price: 300000, // 20 * 15000
    },
    {
      quantity: 25,
      usage: 'Take 1 tablet per day',
      prescription_id: 34,
      drug_id: 2,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 32,
      usage: 'Take 2 tablets per day',
      prescription_id: 35,
      drug_id: 10,
      price: 320000, // 32 * 10000
    },
    {
      quantity: 34,
      usage: 'Take 2 tablets per day',
      prescription_id: 36,
      drug_id: 5,
      price: 170000, // 34 * 5000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 37,
      drug_id: 9,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 22,
      usage: 'Take 2 tablets per day',
      prescription_id: 38,
      drug_id: 7,
      price: 220000, // 22 * 10000
    },
    {
      quantity: 28,
      usage: 'Take 2 tablets per day',
      prescription_id: 39,
      drug_id: 5,
      price: 140000, // 28 * 5000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 40,
      drug_id: 8,
      price: 300000, // 20 * 15000
    },
    {
      quantity: 19,
      usage: 'Take 3 tablets per day',
      prescription_id: 41,
      drug_id: 10,
      price: 190000, // 19 * 10000
    },
    {
      quantity: 23,
      usage: 'Take 2 tablets per day',
      prescription_id: 42,
      drug_id: 3,
      price: 230000, // 23 * 10000
    },
    {
      quantity: 28,
      usage: 'Take 2 tablets per day',
      prescription_id: 43,
      drug_id: 4,
      price: 280000, // 28 * 10000
    },
    {
      quantity: 32,
      usage: 'Take 2 tablets per day',
      prescription_id: 44,
      drug_id: 10,
      price: 320000, // 32 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 45,
      drug_id: 2,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 22,
      usage: 'Take 2 tablets per day',
      prescription_id: 46,
      drug_id: 7,
      price: 220000, // 22 * 10000
    },
    {
      quantity: 31,
      usage: 'Take 2 tablets per day',
      prescription_id: 47,
      drug_id: 10,
      price: 310000, // 31 * 10000
    },
    {
      quantity: 34,
      usage: 'Take 2 tablets per day',
      prescription_id: 48,
      drug_id: 5,
      price: 170000, // 34 * 5000
    },
    {
      quantity: 28,
      usage: 'Take 2 tablets per day',
      prescription_id: 49,
      drug_id: 4,
      price: 280000, // 28 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 50,
      drug_id: 9,
      price: 150000, // 25 * 6000
    },
    {
      quantity: 24,
      usage: 'Take 2 tablets per day',
      prescription_id: 51,
      drug_id: 5,
      price: 120000, // 24 * 5000
    },
    {
      quantity: 19,
      usage: 'Take 2 tablets per day',
      prescription_id: 52,
      drug_id: 10,
      price: 190000, // 19 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 53,
      drug_id: 2,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 22,
      usage: 'Take 2 tablets per day',
      prescription_id: 54,
      drug_id: 7,
      price: 220000, // 22 * 10000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 55,
      drug_id: 8,
      price: 300000, // 20 * 15000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 56,
      drug_id: 8,
      price: 200000, // 25 * 8000
    },
    {
      quantity: 21,
      usage: 'Take 2 tablets per day',
      prescription_id: 57,
      drug_id: 10,
      price: 210000, // 21 * 10000
    },
    {
      quantity: 23,
      usage: 'Take 2 tablets per day',
      prescription_id: 58,
      drug_id: 10,
      price: 230000, // 23 * 10000
    },
    {
      quantity: 24,
      usage: 'Take 2 tablets per day',
      prescription_id: 59,
      drug_id: 10,
      price: 240000, // 24 * 10000
    },
    {
      quantity: 25,
      usage: 'Take 2 tablets per day',
      prescription_id: 60,
      drug_id: 9,
      price: 250000, // 25 * 10000
    },
    {
      quantity: 30,
      usage: 'Take 3 tablets per day',
      prescription_id: 61,
      drug_id: 6,
      price: 180000, // 30 * 6000
    },
    {
      quantity: 23,
      usage: 'Take 2 tablets per day',
      prescription_id: 62,
      drug_id: 10,
      price: 230000, // 23 * 10000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 63,
      drug_id: 10,
      price: 200000, // 20 * 10000
    },
    {
      quantity: 20,
      usage: 'Take 2 tablets per day',
      prescription_id: 64,
      drug_id: 8,
      price: 300000, // 20 * 15000
    },
    {
      quantity: 27,
      usage: 'Take 3 tablets per day',
      prescription_id: 65,
      drug_id: 10,
      price: 270000, // 27 * 10000
    },
    {
      quantity: 21,
      usage: 'Take 2 tablets per day',
      prescription_id: 66,
      drug_id: 10,
      price: 210000, // 21 * 10000
    },
    {
      quantity: 32,
      usage: 'Take 2 tablets per day',
      prescription_id: 67,
      drug_id: 10,
      price: 320000, // 32 * 10000
    },
  ]);
};
