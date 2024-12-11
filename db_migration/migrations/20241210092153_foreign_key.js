/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
  alter table patient_information add foreign key (id) references users(id)
  alter table doctor_information add foreign key (id) references users(id)
  alter table management add foreign key (patient_id) references users(id)
  alter table management add foreign key (doctor_id) references users(id)
  alter table feedback add foreign key (patient_id) references users(id)
  alter table feedback add foreign key (doctor_id) references users(id)
  alter table message add foreign key (sender_id) references users(id)
  alter table message add foreign key (receiver_id) references users(id)
  alter table consultation add foreign key (patient_id) references users(id)
  alter table consultation add foreign key (doctor_id) references users(id)
  alter table prescription add foreign key (consultation_id) references consultation(id)
  alter table prescription_detail add foreign key (prescription_id) references prescription(id)
  alter table prescription_detail add foreign key (drug_id) references drug(id)
  
  `);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = function(knex) {

};
