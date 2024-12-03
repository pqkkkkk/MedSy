const socket = io('http://localhost:5555');

function sendNewCRMessage(){
    const data = {
        senderId : 1,
        receiverId : 2,
    };
    socket.emit('newCR',data);
}
function  sendAcceptedCRMessage(){
    const data = {
        senderId : 2,
        receiverId : 1,
    };
    socket.emit('acceptedCRNoti',data);
}