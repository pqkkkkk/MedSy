const params = new URLSearchParams(window.location.search);
const userId = params.get('userid');
function SendPaymentCompleteMessage() {
    const socket = io('http://localhost:5555');
    socket.emit('paymentCompleteMessage', { userId });
}