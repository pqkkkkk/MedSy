const express = require('express');
const http = require('http');
const fs = require('fs');
const socketIo = require('socket.io');
const path = require('path');

const app = express();

const server = http.createServer(app);
const io = socketIo(server,{
    cors: {
        origin: '*',
    }
});
const port = 5555;

// const db = require('./db.config'); // add this line when connecting the application with database
// const queries = require('./queries'); // add this line when connecting the application with database

app.use(express.static(path.join(__dirname,'testClient')));

app.get('/chat',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','client.html'));
});
app.get('/videocall',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','videocall.html'));
});
app.get('/newCRTest',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','sendCRMessageTest.html'));
});
const users = new Map();

io.on('connection', function(socket)
{
    console.log("A user connected");

    socket.on('register',function(data){
        let {userId,role} = data;
        const id = role + userId;
        users.set(id,socket.id);
        console.log(users.get(id))
        console.log(`A user with id: ${id} registered`);
    });

    socket.on('messageFromClient',function(data){
        console.log('Data received: ', data);
        let {message, senderId, receiverId} = data;
        receiverId = parseInt(receiverId, 10);
        const actualReceiverId = "user" + receiverId;
        const actualSenderId = "user" + senderId;
        if(users.has(actualReceiverId))
        {
            console.log(`Received message: ${message} from ${actualSenderId}. Send to ${actualReceiverId}`);
            // queries.addMessage(senderId,receiverId,message); // Add this line when connecting application with database
            recipientSocketId = users.get(actualReceiverId);
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

    // Signaling server for video call
    socket.on('offer',(data) =>{
        const {offer,senderId, receiverId} = data;
        const receiverVideoCallId = 'videocall' + receiverId;

        if(users.has(receiverVideoCallId))
        {
            io.to(receiverVideoCallId).emit('offer',data);
        }
        else{
            console.log(`Receiver video call client with id ${receiverVideoCallId} is not exist`);
        }
    });
    socket.on('answer', (data)=>{
        const {answer,senderId,receiverId} = data;
        const receiverVideoCallId = 'videocall' + receiverId;

        if(users.has(receiverVideoCallId))
        {
            io.to(receiverVideoCallId).emit('answer',data);
        }
        else{
            console.log(`Receiver video call client with id ${receiverVideoCallId} is not exist`);
        }

    });
    socket.on('candidate', (data) =>{
       console.log('ICE Candidate received: ',data);
       socket.broadcast.emit('candidate',data);
    });
    socket.on('endCallMessage',(data) =>{
        const {userId} = data;
        const actualUserId = "user" + userId;

        if(users.has(actualUserId))
        {
            const userSocketId = users.get(actualUserId);
            io.to(userSocketId).emit('endCallMessage');
            console.log('End call message sent from video call client to corresponding user client');
        }
        else{
            console.log(`Receiver with id ${actualUserId} is not exist`);
        }
    });
    // Notification about consultation request
    socket.on('newCR',(data) =>{
        const {senderId, receiverId} = data;
        const actualReceiverId = "user" + receiverId;

        if(users.has(actualReceiverId))
        {
            const actualReceiverSocketId = users.get(actualReceiverId);
            io.to(actualReceiverSocketId).emit('newCR',data);
            console.log('Consultation request notification sent');
        }
        else{
            console.log(`Receiver with id ${actualReceiverId} is not available`);
        }
    });
    socket.on('acceptedCRNoti',(data) =>{
        const {senderId, receiverId} = data;
        const actualReceiverId = "user" + receiverId;

        if(users.has(actualReceiverId))
        {
            const actualReceiverSocketId = users.get(actualReceiverId);
            io.to(actualReceiverSocketId).emit('acceptedCRNoti',data);
            console.log('Accepted consultation request notification sent');
        }
        else{
            console.log(`Receiver with id ${actualReceiverId} is not available`);
        }
    });
});

server.listen(port,() =>{
    console.log(`Server is running on http://localhost:${port}`);
})