
window.onload = (ev) => {
    let url: URL = new URL("/ws", window.location.href);
    url.protocol = url.protocol.replace("http", "ws");
    let websocket: WebSocket = new WebSocket(url.href);
    websocket.onmessage = (ev: MessageEvent) => {
        console.debug("WebSocket message received:", ev);
    }
    websocket.onopen = (ev: Event) => {
        websocket.send("test");
    }


}