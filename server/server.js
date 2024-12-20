
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
app.use(express.json());
app.use('/testClient', express.static(path.join(__dirname, 'testClient')));
app.get('/chat',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','client.html'));
});
app.get('/videocall',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','videocall.html'));
});
app.get('/newCRTest',(req,res) =>{
    res.sendFile(path.join(__dirname,'testClient','sendCRMessageTest.html'));
});
app.post('/create_payment_url', function (req, res, next) {
    console.log(req.body);
    //var ipAddr = req.headers['x-forwarded-for'] ||
    //    req.connection.remoteAddress ||
    //    req.socket.remoteAddress ||
    //    req.connection.socket.remoteAddress;
    var ipAddr = '127.0.0.1';
    var config = require('config');

    var tmnCode = config.get('vnp_TmnCode');
    var secretKey = config.get('vnp_HashSecret');
    var vnpUrl = config.get('vnp_Url');
    var returnUrl = config.get('vnp_ReturnUrl');

    var date = new Date();
    var year = date.getFullYear();
    var month = String(date.getMonth() + 1).padStart(2, '0');
    var day = String(date.getDate()).padStart(2, '0');
    var hours = String(date.getHours()).padStart(2, '0');
    var minutes = String(date.getMinutes()).padStart(2, '0');
    var seconds = String(date.getSeconds()).padStart(2, '0');
    var createDate = `${year}${month}${day}${hours}${minutes}${seconds}`;
    var orderId = req.body.orderId + `${hours}${minutes}${seconds}`;
    var amount = req.body.amount;
    var bankCode = req.body.bankCode;

    var orderInfo = req.body.orderDescription;
    var orderType = req.body.orderType;
    var locale = req.body.language;
    if(locale === null || locale === ''){
        locale = 'vn';
    }
    var currCode = 'VND';
    var vnp_Params = {};
    vnp_Params['vnp_Version'] = '2.1.0';
    vnp_Params['vnp_Command'] = 'pay';
    vnp_Params['vnp_TmnCode'] = tmnCode;
    vnp_Params['vnp_Locale'] = locale;
    vnp_Params['vnp_CurrCode'] = currCode;
    vnp_Params['vnp_TxnRef'] = orderId;
    vnp_Params['vnp_OrderInfo'] = orderInfo;
    vnp_Params['vnp_OrderType'] = orderType;
    vnp_Params['vnp_Amount'] = amount * 100;
    vnp_Params['vnp_ReturnUrl'] = returnUrl;
    vnp_Params['vnp_IpAddr'] = ipAddr;
    vnp_Params['vnp_CreateDate'] = createDate;
    if(bankCode !== null && bankCode !== ''){
      vnp_Params['vnp_BankCode'] = bankCode;
    }

    vnp_Params = sortObject(vnp_Params);

    var querystring = require('qs');
    var signData = querystring.stringify(vnp_Params, { encode: false });
    var crypto = require("crypto");
    var hmac = crypto.createHmac("sha512", secretKey);
    var signed = hmac.update(new Buffer.from(signData, 'utf-8')).digest("hex");
    vnp_Params['vnp_SecureHash'] = signed;
    vnpUrl += '?' + querystring.stringify(vnp_Params, { encode: false });

    res.status(200).json({code: '00', data: vnpUrl});
});

app.get('/vnpay_return', function (req, res, next) {
    var vnp_Params = req.query;
    console.log(vnp_Params);
    var secureHash = vnp_Params['vnp_SecureHash'];

    delete vnp_Params['vnp_SecureHash'];
    delete vnp_Params['vnp_SecureHashType'];

    vnp_Params = sortObject(vnp_Params);

    var config = require('config');
    var tmnCode = config.get('vnp_TmnCode');
    var secretKey = config.get('vnp_HashSecret');

    var querystring = require('qs');
    var signData = querystring.stringify(vnp_Params, { encode: false });
    var crypto = require("crypto");
    var hmac = crypto.createHmac("sha512", secretKey);
    var signed = hmac.update(new Buffer.from(signData, 'utf-8')).digest("hex");

    const orderId = vnp_Params['vnp_TxnRef'];
    const userId = orderId.split('_')[0];
    const data = {userid : userId};
    const queryString = new URLSearchParams(data).toString();
    if(secureHash === signed){
        console.log('Return success');
        res.redirect(`/testClient/vnpaySuccess.html?${queryString}`);
    } else{
        console.log('Return failed');
        res.redirect(`/testClient/vnpayFail.html?${queryString}`);
    }
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
    // Payment complete message
    socket.on('paymentCompleteMessage',(data) =>{
        const {userId} = data;
        const actualUserId = "user" + userId;

        if(users.has(actualUserId))
        {
            const userSocketId = users.get(actualUserId);
            io.to(userSocketId).emit('paymentCompleteMessage');
            console.log('Payment complete message sent');
        }
        else{
            console.log(`Receiver with id ${actualUserId} is not exist`);
        }
    });
});

server.listen(port,() =>{
    console.log(`Server is running on http://localhost:${port}`);
})

function sortObject(obj) {
    let sorted = {};
    let str = [];
    let key;
    for (key in obj){
        if (obj.hasOwnProperty(key)) {
            str.push(encodeURIComponent(key));
        }
    }
    str.sort();
    for (key = 0; key < str.length; key++) {
        sorted[str[key]] = encodeURIComponent(obj[str[key]]).replace(/%20/g, "+");
    }
    return sorted;
}