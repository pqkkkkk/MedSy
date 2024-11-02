const socket = io('http://localhost:5555');
const chatBox = document.getElementById('chat-box');
const messageInput = document.getElementById('message');
const sendMessageButton = document.getElementById('sendMessage');

// When receiving message from server
socket.on('receiveMessage',(message)=>{
    const messageElement = document.createElement('div');
    messageElement.classList.add(message);
    messageElement.innerHTML = `<span>User:</span> ${message}`;
    chatBox.appendChild(messageElement);
    chatBox.scrollTop = chatBox.scrollHeight; // scroll down end of chat
});

// When click send button
sendMessageButton.onclick = () =>{
    const user_message = messageInput.value.trim();
    if(user_message)
    {
        const data = {
            message: user_message,
            senderId :2,
            receiverId: 1,
        };
        socket.emit('messageFromClient',data); // send message to server
        messageInput.value = "";
    }
};

messageInput.addEventListener('keypress',(e) =>{
    if(e.key == 'Enter'){
        sendMessageButton.click();
    }
});