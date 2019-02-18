window.onload = (ev) => {
    let url = new URL("/ws", window.location.href);
    url.protocol = url.protocol.replace("http", "ws");
    let websocket = new WebSocket(url.href);
    websocket.onmessage = (ev) => {
        alert(`WebSocket message received: ${ev.data}`);
    };
    websocket.onopen = (ev) => {
        websocket.send("test");
    };
};
