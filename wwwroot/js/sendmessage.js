const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

hubConnection.on("Send", sendFunc);
hubConnection.on("Notify", notifyFunc);
hubConnection.on("Counter", function (broCounter, sisCounter) {
    var broElem = document.getElementById('broClick');
    broElem.innerHTML = broCounter;

    var sisElem = document.getElementById('sisClick');
    sisElem.innerHTML = sisCounter;
});


hubConnection.on("ConnectedMessages", function (messages) {
    for (let i = 0, l = messages.length; i < l; i++) {
        sendFunc(messages[i].sendMessage);
        notifyFunc(messages[i].senderUser, messages[i].sendTime);
    }
});

const btns = document.querySelectorAll('button[id^=but]')
btns.forEach(btn => {
    btn.addEventListener('click', event => {
        var data = $('#' + event.target.id).text();
        hubConnection.invoke("Send", data);
        hubConnection.invoke("Notify");
        hubConnection.invoke("Counter");
    });
});

function sendFunc(data) {
    let elem = document.createElement("p");
    elem.style.cssText = "font-size: 16pt;";
    elem.appendChild(document.createTextNode(data));

    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
}

function notifyFunc(userName, time) {
    let elem = document.createElement("p");
    elem.style.cssText = 'font-size: 9pt; margin: 0; padding: 0; font-family: Verdana, Arial, Helvetica, sans-serif; class="font-weight-light;"';
    elem.appendChild(document.createTextNode("Sent by " + userName + " at " + time));

    let firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
}

hubConnection.start();
