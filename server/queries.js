const db = require("./db.config")

function addMessage(senderId, receiverId, content)
{
    const sql = "INSERT INTO message(senderId,receiverId,content) values(?,?,?)";
    const values = [senderId,receiverId,content];

    db.query(sql,values,(err, result) =>{
        if(err){
            console.log('Cannot add message into db: ', err);
            return;
        }
        console.log("Message inserted successfully", result);
    })
}
module.exports = {
    addMessage
};