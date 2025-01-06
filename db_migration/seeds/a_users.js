/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('prescription_detail').del();
  await knex('prescription').del();
  await knex('consultation').del();
  await knex('patient_information').del();
  await knex('doctor_information').del();
  await knex('management').del();
  await knex('message').del();
  await knex('feedback').del();
  await knex('users').del();
  //  await knex.raw('DBCC CHECKIDENT("users", RESEED, 0)');
  await knex('users').insert([
    {
      fullName : "Pham Quoc Kiet",
      username : "pqkiet854",
      password : "pqkiet854",
      phoneNumber : "0123456789",
      email : "pqkiet854@gmail.com",
      address : "Hanoi",
      birthday : "1999-08-05",
      gender :"Male",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      role : "patient"
    },
    {
      fullName :"John Doe",
      username : "johndoe",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "pqkiet854@gmail.com",
      address : "New York",
      birthday : "1977-08-05",
      gender :"Male",
      role : "doctor"
    },
    {
      fullName : "Lionel Messi",
      username : "lionelmessi",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0123456789",
      email : "pqkiet854@gmail.com",
      address : "Argentina",
      birthday : "1985-04-12",
      gender :"Male",
      role : "doctor"
    },
    {
      fullName : "Nguyen Van A",
      username : "nva",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      phoneNumber : "0123456789",
      email : "vna@gmail.com",
      address : "Hanoi",
      birthday : "1999-08-05",
      gender :"Male",
      role : "patient"
    },
    {
      fullName :"Le Van An",
      username : "lva",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "pqkiet854@gmail.com",
      address : "Tien Giang",
      birthday : "1977-08-05",
      gender :"Male",
      role : "doctor"
    },
    {
      fullName :"Nguyen Thi Binh",
      username : "ntb",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "ntb123@gmail.com",
      address : "Ho Chi Minh",
      birthday : "1977-08-05",
      gender :"Female",
      role : "doctor"
    },
    {
      fullName :"Nguyen Van Cuong",
      username : "nvc",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "nvc@gmail.com",
      address : "New York",
      birthday : "1977-08-05",
      gender :"Male",
      role : "doctor"
    },
    {
      fullName :"Dinh Thi Dung",
      username : "dtd",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "dtd@gmail.com",
      address : "New York",
      birthday : "1977-08-05",
      gender :"Female",
      role : "doctor"
    },
    {
      fullName :"Bui Hoang Hai",
      username : "bhh",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "bhh@gmail.com",
      address : "Gia Lai",
      birthday : "1977-08-05",
      gender :"Female",
      role : "doctor"
    },
    {
      fullName :"Pham Van Hung",
      username : "pvh",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "pvh@gmail.com",
      address : "Nam Dinh",
      birthday : "1977-08-05",
      gender :"Male",
      role : "doctor"
    },
    {
      fullName :"Bui Thi Kim",
      username : "btk",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0343523255",
      email : "btk@gmail.com",
      address : "New York",
      birthday : "1977-08-05",
      gender :"Female",
      role : "doctor"
    },
    {
      fullName :"admin",
      username : "admin",
      password : "admin",
      avatar_Path : "ms-appx:///Assets/doctoravt.jpg",
      phoneNumber : "0354747227",
      email : "qthong2004@gmail.com",
      address : "New York",
      birthday : "2004-08-04",
      gender :"Male",
      role : "admin"
    },
    {
      fullName : "Bui Duy An",
      username : "buiduyan",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      phoneNumber : "0123456789",
      email : "bda@gmail.com",
      address : "Ho Chi Minh",
      birthday : "1999-08-05",
      gender :"Male",
      role : "patient"
    },
    {
      fullName : "Le Bao",
      username : "lebao",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      phoneNumber : "0123456789",
      email : "lebao@gmail.com",
      address : "Ho Chi Minh",
      birthday : "1999-08-12",
      gender :"Male",
      role : "patient"
    },
    {
      fullName : "Nguyen Quang Thong",
      username : "qthong2004",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      phoneNumber : "0123456789",
      email : "qthong2004@gmail.com",
      address : "Ho Chi Minh",
      birthday : "1999-04-08",
      gender :"Male",
      role : "patient"
    },
    {
      fullName : "Le Van Thanh Dat",
      username : "thanhdat",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      phoneNumber : "0123456789",
      email : "thanhdat@gmail.com",
      address : "Ho Chi Minh",
      birthday : "1999-08-05",
      gender :"Male",
      role : "patient"
    },
    {
      fullName : "Nguyen Xuan Son",
      username : "xuanson",
      password : "pqkiet854",
      avatar_Path : "ms-appx:///Assets/defaultavt.jpg",
      phoneNumber : "0123456789",
      email : "xuanson@gmail.com",
      address : "Ho Chi Minh",
      birthday : "1999-04-08",
      gender :"Male",
      role : "patient"
    },
  ]);
};
