const express = require('express');
const http = require('http');
const socketIo = require('socket.io');
const path = require('path');

const app = express();
const server = http.createServer(app);
const io = socketIo(server);
const port = 5555;

const db = require('./db.config');
app.use(express.static(path.join(__dirname,'testClient')));

app.get('/',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','client.html'));
});

const users = new Map();

io.on('connection', function(socket)
{
    console.log("A user connected");

    socket.on('register',function(userId){
        users.set(userId,socket.id);
        console.log(users.get(userId))
        console.log(`A user with id: ${userId} registered`);
    });

    socket.on('messageFromClient',function(data){
        console.log('Data received: ', data);
        let {message, senderId, receiverId} = data;
        receiverId = parseInt(receiverId, 10);

        if(users.has(receiverId))
        {
            console.log(`Received message: ${message} from ${senderId}. Send to ${receiverId}`);
            recipientSocketId = users.get(receiverId);
            io.to(recipientSocketId).emit('messageFromServer', {message, senderId}); 
        }
        else{
            console.log(`Receiver with id ${receiverId} is not exist`);
        }      
    })
    socket.on('disconnect', function(userId){
        console.log('A user disconnected');
        users.delete(userId);
    });

});

server.listen(port,() =>{
    console.log(`Server is running on http://localhost:${port}`);
})