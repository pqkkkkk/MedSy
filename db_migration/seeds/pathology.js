/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('pathology').del()
  await knex('pathology').insert([
    { name: 'Common Cold' }, // cảm thông thường
    { name: 'Headache' }, // đau đầu
    { name: 'Diarrhea' }, // Tiêu chảy
    { name: 'Pneumonia' }, // Viêm phổi
    { name: 'Gastritis' }, // Dạ dày
    { name: 'Back Pain' }, // Đau lưng
    { name: 'Hypertension' }, // Huyết áp cao
    { name: 'Diabetes' }, // Tiểu đường
    { name: 'Hepatitis' }, // Viêm gan
    { name: 'Cardio D' } // Tim mạch
  ]);
};
