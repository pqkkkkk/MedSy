/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('prescription').del()
  await knex('prescription').insert([
    // Tháng 1
    {
      total_price: 120000,
      created_day: '2024-01-02',
      consultation_id: 21,
      status: 'paid'
    },
    {
      total_price: 250000,
      created_day: '2024-01-05',
      consultation_id: 22,
      status: 'paid'
    },
    {
      total_price: 95000, 
      created_day: '2024-01-10',
      consultation_id: 23,
      status: 'paid'
    },
    {
      total_price: 350000, 
      created_day: '2024-01-15',
      consultation_id: 24,
      status: 'paid'
    },
    {
      total_price: 180000,
      created_day: '2024-01-20',
      consultation_id: 25,
      status: 'paid'
    },
    // Tháng 2
    {
      total_price: 180000, 
      created_day: '2024-02-02',
      consultation_id: 26,
      status: 'paid'
    },
    {
      total_price: 220000, 
      created_day: '2024-02-05',
      consultation_id: 27,
      status: 'paid'
    },
    {
      total_price: 300000,  
      created_day: '2024-02-10',
      consultation_id: 28,
      status: 'paid'
    },
    {
      total_price: 150000, 
      created_day: '2024-02-12',
      consultation_id: 29,
      status: 'paid'
    },
    {
      total_price: 270000,  
      created_day: '2024-02-18',
      consultation_id: 30,
      status: 'paid'
    },
    // Tháng 3
    {
      total_price: 250000,  // giá trị đã chọn
      created_day: '2024-03-01',
      consultation_id: 31,
      status: 'paid'
    },
    {
      total_price: 200000,  // giá trị đã chọn
      created_day: '2024-03-05',
      consultation_id: 32,
      status: 'paid'
    },
    {
      total_price: 300000,  // giá trị đã chọn
      created_day: '2024-03-08',
      consultation_id: 33,
      status: 'paid'
    },
    {
      total_price: 180000,  // giá trị đã chọn
      created_day: '2024-03-12',
      consultation_id: 34,
      status: 'paid'
    },
    {
      total_price: 220000,  // giá trị đã chọn
      created_day: '2024-03-15',
      consultation_id: 35,
      status: 'paid'
    },
    // Tháng 4
    {
      total_price: 230000,  // giá trị đã chọn
      created_day: '2024-04-02',
      consultation_id: 36,
      status: 'paid'
    },
    {
      total_price: 310000,  // giá trị đã chọn
      created_day: '2024-04-05',
      consultation_id: 37,
      status: 'paid'
    },
    {
      total_price: 150000,  // giá trị đã chọn
      created_day: '2024-04-10',
      consultation_id: 38,
      status: 'paid'
    },
    {
      total_price: 200000,  // giá trị đã chọn
      created_day: '2024-04-12',
      consultation_id: 39,
      status: 'paid'
    },
    {
      total_price: 270000,  // giá trị đã chọn
      created_day: '2024-04-20',
      consultation_id: 40,
      status: 'paid'
    },
    //Tháng 5
    {
      total_price: 180000,  // giá trị đã chọn
      created_day: '2024-05-01',
      consultation_id: 41,
      status: 'paid'
    },
    {
      total_price: 260000,  // giá trị đã chọn
      created_day: '2024-05-05',
      consultation_id: 42,
      status: 'paid'
    },
    {
      total_price: 150000,  // giá trị đã chọn
      created_day: '2024-05-08',
      consultation_id: 43,
      status: 'paid'
    },
    {
      total_price: 300000,  // giá trị đã chọn
      created_day: '2024-05-12',
      consultation_id: 44,
      status: 'paid'
    },
    {
      total_price: 250000,  // giá trị đã chọn
      created_day: '2024-05-15',
      consultation_id: 45,
      status: 'paid'
    },
    // Tháng 6
    {
      total_price: 220000,  // giá trị đã chọn
      created_day: '2024-06-02',
      consultation_id: 46,
      status: 'paid'
    },
    {
      total_price: 310000,  // giá trị đã chọn
      created_day: '2024-06-05',
      consultation_id: 47,
      status: 'paid'
    },
    {
      total_price: 170000,  // giá trị đã chọn
      created_day: '2024-06-10',
      consultation_id: 48,
      status: 'paid'
    },
    {
      total_price: 280000,  // giá trị đã chọn
      created_day: '2024-06-12',
      consultation_id: 49,
      status: 'paid'
    },
    {
      total_price: 150000,  // giá trị đã chọn
      created_day: '2024-06-15',
      consultation_id: 50,
      status: 'paid'
    },
    // Tháng 7
    {
      total_price: 200000,  // giá trị đã chọn
      created_day: '2024-07-02',
      consultation_id: 51,
      status: 'paid'
    },
    {
      total_price: 180000,  // giá trị đã chọn
      created_day: '2024-07-05',
      consultation_id: 52,
      status: 'paid'
    },
    {
      total_price: 300000,  // giá trị đã chọn
      created_day: '2024-07-10',
      consultation_id: 53,
      status: 'paid'
    },
    {
      total_price: 250000,  // giá trị đã chọn
      created_day: '2024-07-15',
      consultation_id: 54,
      status: 'paid'
    },
    {
      total_price: 320000,  // giá trị đã chọn
      created_day: '2024-07-18',
      consultation_id: 55,
      status: 'paid'
    },
    // Tháng 8
    {
      total_price: 170000,  // giá trị đã chọn
      created_day: '2024-08-02',
      consultation_id: 56,
      status: 'paid'
    },
    {
      total_price: 250000,  // giá trị đã chọn
      created_day: '2024-08-05',
      consultation_id: 57,
      status: 'paid'
    },
    {
      total_price: 220000,  // giá trị đã chọn
      created_day: '2024-08-10',
      consultation_id: 58,
      status: 'paid'
    },
    {
      total_price: 140000,  // giá trị đã chọn
      created_day: '2024-08-15',
      consultation_id: 59,
      status: 'paid'
    },
    {
      total_price: 300000,  // giá trị đã chọn
      created_day: '2024-08-18',
      consultation_id: 60,
      status: 'paid'
    },
    // Tháng 9
    {
      total_price: 190000,  // giá trị đã chọn
      created_day: '2024-09-02',
      consultation_id: 61,
      status: 'paid'
    },
    {
      total_price: 230000,  // giá trị đã chọn
      created_day: '2024-09-05',
      consultation_id: 62,
      status: 'paid'
    },
    {
      total_price: 280000,  // giá trị đã chọn
      created_day: '2024-09-10',
      consultation_id: 63,
      status: 'paid'
    },
    {
      total_price: 320000,  // giá trị đã chọn
      created_day: '2024-09-12',
      consultation_id: 64,
      status: 'paid'
    },
    {
      total_price: 250000,  // giá trị đã chọn
      created_day: '2024-09-15',
      consultation_id: 65,
      status: 'paid'
    },
    // Tháng 10
    {
      total_price: 220000,  // giá trị đã chọn
      created_day: '2024-10-05',
      consultation_id: 66,
      status: 'paid'
    },
    {
      total_price: 310000,  // giá trị đã chọn
      created_day: '2024-10-07',
      consultation_id: 67,
      status: 'paid'
    },
    {
      total_price: 170000,  // giá trị đã chọn
      created_day: '2024-10-12',
      consultation_id: 68,
      status: 'paid'
    },
    {
      total_price: 280000,  // giá trị đã chọn
      created_day: '2024-10-14',
      consultation_id: 69,
      status: 'paid'
    },
    {
      total_price: 150000,  // giá trị đã chọn
      created_day: '2024-10-20',
      consultation_id: 70,
      status: 'paid'
    },
    // Tháng 11
    {
      total_price: 120000,  // giá trị đã chọn
      created_day: '2024-11-02',
      consultation_id: 71,
      status: 'paid'
    },
    {
      total_price: 190000,  // giá trị đã chọn
      created_day: '2024-11-05',
      consultation_id: 72,
      status: 'paid'
    },
    {
      total_price: 250000,  // giá trị đã chọn
      created_day: '2024-11-10',
      consultation_id: 73,
      status: 'paid'
    },
    {
      total_price: 220000,  // giá trị đã chọn
      created_day: '2024-11-12',
      consultation_id: 74,
      status: 'paid'
    },
    {
      total_price: 300000,  // giá trị đã chọn
      created_day: '2024-11-18',
      consultation_id: 75,
      status: 'paid'
    },
    {
      total_price: 200000,  // giá trị đã chọn
      created_day: '2024-11-16',
      consultation_id: 4,
      status: 'paid'
    },
    {
      total_price: 210000,  // giá trị đã chọn
      created_day: '2024-11-16',
      consultation_id: 7,
      status: 'paid'
    },
    {
      total_price: 230000,  // giá trị đã chọn
      created_day: '2024-11-27',
      consultation_id: 9,
      status: 'paid'
    },
    {
      total_price: 240000, 
      created_day: '2024-11-27',
      consultation_id: 10,
      status: 'paid'
    },
    // Tháng 12
    {
      total_price: 250000, 
      created_day: '2024-12-02',
      consultation_id: 76,
      status: 'paid'
    },
    {
      total_price: 180000,
      created_day: '2024-12-05',
      consultation_id: 77,
      status: 'paid'
    },
    {
      total_price: 230000, 
      created_day: '2024-12-08',
      consultation_id: 78,
      status: 'paid'
    },
    {
      total_price: 200000, 
      created_day: '2024-12-10',
      consultation_id: 13,
      status: 'paid'
    },
    {
      total_price: 300000, 
      created_day: '2024-12-12',
      consultation_id: 79,
      status: 'paid'
    },
    {
      total_price: 270000, 
      created_day: '2024-12-13',
      consultation_id: 16,
      status: 'paid'
    },
    {
      total_price: 210000, 
      created_day: '2024-12-15',
      consultation_id: 80,
      status: 'paid'
    },
    {
      total_price: 320000, 
      created_day: '2024-12-17',
      consultation_id: 20,
      status: 'paid'
    }
    
  ]);
};
