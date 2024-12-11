/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('drug').del();

  // Reset identity column back to 0
  //await knex.raw('DBCC CHECKIDENT("consultation", RESEED, 0)');

  await knex('drug').insert([
    {
      name: 'Paracetamol',
      unit: 'Tablet',
      price: 5000,
      quantity: 180,
      manufacturing_date: '2024-01-01',
      expiry_date: '2025-01-01',
      drug_type: 'Painkiller',
    },
    {
      name: 'Amoxicillin',
      unit: 'Capsule',
      price: 10000,
      quantity: 140,
      manufacturing_date: '2023-12-01',
      expiry_date: '2025-06-01',
      drug_type: 'Antibiotic',
    },
    {
      name: 'Ibuprofen',
      unit: 'Tablet',
      price: 7000,
      quantity: 300,
      manufacturing_date: '2024-03-01',
      expiry_date: '2025-03-01',
      drug_type: 'Painkiller',
    },
    {
      name: 'Ciprofloxacin',
      unit: 'Capsule',
      price: 15000,
      quantity: 100,
      manufacturing_date: '2023-11-01',
      expiry_date: '2025-04-01',
      drug_type: 'Antibiotic',
    },
    {
      name: 'Aspirin',
      unit: 'Tablet',
      price: 6000,
      quantity: 250,
      manufacturing_date: '2024-02-01',
      expiry_date: '2025-02-01',
      drug_type: 'Painkiller',
    },
    {
      name: 'Fluconazole',
      unit: 'Injection',
      price: 20000,
      quantity: 50,
      manufacturing_date: '2023-10-01',
      expiry_date: '2025-01-01',
      drug_type: 'Antifungal',
    },
    {
      name: 'Oseltamivir',
      unit: 'Syrup',
      price: 30000,
      quantity: 80,
      manufacturing_date: '2023-09-01',
      expiry_date: '2024-09-01',
      drug_type: 'Antiviral',
    },
    {
      name: 'Metformin',
      unit: 'Tablet',
      price: 8000,
      quantity: 400,
      manufacturing_date: '2024-01-15',
      expiry_date: '2026-01-15',
      drug_type: 'Diabetes',
    },
    {
      name: 'Atorvastatin',
      unit: 'Tablet',
      price: 12000,
      quantity: 350,
      manufacturing_date: '2023-08-01',
      expiry_date: '2025-08-01',
      drug_type: 'Cholesterol',
    },
    {
      name: 'Insulin',
      unit: 'Injection',
      price: 25000,
      quantity: 60,
      manufacturing_date: '2023-07-01',
      expiry_date: '2025-07-01',
      drug_type: 'Diabetes',
    },
  ]);
};
