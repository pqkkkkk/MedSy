let senderIdValue;
let receiverIdValue;

const localVideo = document.getElementById('localVideo');
const remoteVideo = document.getElementById('remoteVideo');
const toggleMicButton = document.getElementById('toggleMic');
const toggleCameraButton = document.getElementById('toggleCamera');
const endCall = document.getElementById('endCall');
const startCall = document.getElementById('startCall');

startCall.addEventListener('click',() =>{
    startVideoCall();
});
toggleMicButton.addEventListener('click', () => {
    micEnabled = !micEnabled;
    localStream.getAudioTracks()[0].enabled = micEnabled;
    toggleMicButton.textContent = micEnabled ? 'Turn Off Mic' : 'Turn On Mic';
});
toggleCameraButton.addEventListener('click', () => {
    cameraEnabled = !cameraEnabled;
    localStream.getVideoTracks()[0].enabled = cameraEnabled;
    toggleCameraButton.textContent = cameraEnabled ? 'Turn Off Camera' : 'Turn On Camera';
});
endCall.addEventListener('click',() =>{
    // Ngắt kết nối WebRTC
    if (peerConnection) {
        peerConnection.close();
        peerConnection = null;
    }

    // Dừng stream video/audio
    if (localStream) {
        localStream.getTracks().forEach((track) => track.stop());
        localStream = null;
    }

    // Gửi thông báo tới signaling server
    signalingSocket.emit('endCallMessage', { userId: senderIdValue });

    // Cập nhật giao diện người dùng
    localVideo.srcObject = null;
    remoteVideo.srcObject = null;
    toggleMicButton.disabled = true;
    toggleCameraButton.disabled = true;
});

let localStream;
let peerConnection;
let micEnabled = true;
let cameraEnabled = true;

const signalingSocket = io('http://172.20.10.2:5555');

signalingSocket.on('offer', async (offer) =>
{``
    await  peerConnection.setRemoteDescription(new RTCSessionDescription(offer));
    const answer = await  peerConnection.createAnswer();
    await  peerConnection.setLocalDescription(answer);
    signalingSocket.emit('answer',{
        answer: answer,
        senderId: senderIdValue,
        receiverId : receiverIdValue
    });
});
signalingSocket.on('answer',async (answer) =>{
    await  peerConnection.setRemoteDescription(new RTCSessionDescription(answer));
});
signalingSocket.on('candidate', async (candidate) =>{
    await  peerConnection.addIceCandidate(new RTCIceCandidate(candidate));
});

async function initializePeerConnection() {
    const configuration = { iceServers: [{ urls: 'stun:stun.l.google.com:19302' }] };
    peerConnection = new RTCPeerConnection(configuration);

    // Lấy video và audio từ thiết bị
    try {
        localStream = await navigator.mediaDevices.getUserMedia({video: true, audio: true});
        localVideo.srcObject = localStream;
    }
    catch(err)
    {
        console.log('Error occurred: ',err);
        alert('Error occurred when accessing media devices');
    }

    // Thêm track vào peerConnection
    localStream.getTracks().forEach((track) => {
        peerConnection.addTrack(track, localStream);
    });

    // Hiển thị remote stream
    peerConnection.ontrack = (event) => {
        remoteVideo.srcObject = event.streams[0];
    };

    // Gửi ICE candidate qua signaling server
    peerConnection.onicecandidate = (event) => {
        if (event.candidate) {
            signalingSocket.emit('candidate', event.candidate);
        }
    };
}

async function startVideoCall() {
    await  initializePeerConnection();
    const offer = await  peerConnection.createOffer();
    await  peerConnection.setLocalDescription(offer);
    signalingSocket.emit('offer',{
       offer : offer,
       senderId:senderIdValue,
        receiverId: receiverIdValue,
    });

}
function receiveDataFromUserClient(senderId, receiverId)
{
    senderIdValue = senderId;
    receiverIdValue = receiverId;
    signalingSocket.emit('register',{
        userId: senderIdValue,
        role : "videocall",
    });
}
