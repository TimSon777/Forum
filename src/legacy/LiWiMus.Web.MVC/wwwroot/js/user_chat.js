function displayMessage(messageId, isOwner) {
    $.get({
        url: `/User/SupportChat/MessageForUser?messageId=${messageId}&isOwner=${isOwner}`,
        success: (html) => {
            $('#user-messages').append(html)
        },
        error: (jqXHR, exception) => {
            alert(`Status code: ${jqXHR} with message ${exception}`);
        }
    });
}

function displayConsultantMessage(connection) {
    connection.on("GetMessageIdForUser", 
        (messageId) => displayMessage(messageId, false));
}

async function sendAndDisplayMessageAsync(connection) {
    $('#btn-send-to-consultant').click(async () => {
        const text = $('#ta-send-to-consultant').val()
        await connection
            .invoke("SendMessageToConsultant", text)
            .then((result) => {
                if (result.isSuccess) {
                    displayMessage(result.value, true)
                } else {
                    alert(result.error)
                }
            });
    });
}

async function closeChatAsync(connection) {
    $('#btn-close-chat-by-user').click(async () => {
        await connection.invoke("CloseChatByUser")
            .then(result => {
                if (result.isSuccess) {
                    alert("Chat was closed");
                } else {
                    alert(result.error);
                }
            });
    })
}

function displayChat() {
    $.get({
        url: '/User/SupportChat/ChatForUser',
        success: (html) => {
            $('#chat').html(html);
        },
        error: (jqXHR, exception) => {
            alert(`Status code: ${jqXHR} with message ${exception}`);
        }
    })
}

async function connectUser(connection) {
    await connection.invoke("ConnectUser");
}

function establishConnection(url) {
    return new signalR.HubConnectionBuilder()
        .withUrl(url)
        .build();
}

async function startChatAsync() {
    const connection = establishConnection("/chat")
    displayConsultantMessage(connection);
    await sendAndDisplayMessageAsync(connection);
    await closeChatAsync(connection);
    await connection.start();
    await connectUser(connection);
    displayChat();
}

$(document).ready(async function () {
    await startChatAsync();
})