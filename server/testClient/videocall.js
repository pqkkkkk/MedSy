const localVideo = document.getElementById('localVideo');
const remoteVideo = document.getElementById('remoteVideo');

let localStream;
let peerConnection;

const signalingSocket = io('http://localhost:5555');

signalingSocket.on('offer', async (offer) =>
{
    await  peerConnection.setRemoteDescription(new RTCSessionDescription(offer));
    const answer = await  peerConnection.createAnswer();
    await  peerConnection.setLocalDescription(answer);
    signalingSocket.emit('answer',answer);
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
    localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
    localVideo.srcObject = localStream;

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

async function startVideoCall()
{
    await  initializePeerConnection();
    const offer = await  peerConnection.createOffer();
    await  peerConnection.setLocalDescription(offer);
    signalingSocket.emit('offer',offer);
}

startVideoCall();