function removeChatBy(userName) {
    $(`#chat-${userName}`).remove();
}

async function refreshHandlerAsync(connection, userName) {
    $(`#btn-${userName}`).click(async function () {
        const connectionId = $(this).val();
        const message = $(`textarea[id=${connectionId}]`).val();
        const id = $(this).attr('id').split('-', 2)[1];

        await connection
            .invoke("SendMessageToUser", connectionId, message)
            .then((result) => {
                if (result.isSuccess) {
                    displayMessage(result.value, true, id)
                } else {
                    alert(result.error)
                }
            });
    });

    $(`.btn-close-chat-by-consultant-${userName}`).click(async function () {
        const userName = $(this).val();
        await connection
            .invoke("CloseChatByConsultant", userName)
            .then(result => {
                if (result.isSuccess) {
                    alert("Chat was closed");
                } else {
                    alert(result.error);
                }
            });
        
        removeChatBy(userName);
    })
}

function establishConnection(url) {
    return new signalR.HubConnectionBuilder()
        .withUrl(url)
        .build();
}

async function disconnectByClick(connection) {
    $('#offline').click(async () => {
        await connection.stop()
    })
}

async function refreshChatsByClick(connection) {
    $('#refresh-chats').click(() => {
        $.get({
            url: 'GetTextingUsersChats',
            success: (userNames) => {
                $('#chats').empty()
                userNames.forEach(userName => {
                    $.get({
                        url: `ChatForConsultant?userName=${userName}`,
                        success: async (html) => {
                            $('#chats').prepend(html)
                            await refreshHandlerAsync(connection, userName)
                        },
                        error: (jqXHR, exception) => {
                            alert(`Status code: ${jqXHR} with message ${exception}`);
                        }
                    });
                });
            },
            error: (jqXHR, exception) => {
                alert(`Status code: ${jqXHR} with message ${exception}`);
            }
        });
    });
}

function displayMessage(messageId, isOwner, id) {
    $.get({
        url: `/User/SupportChat/MessageForConsultant?messageId=${messageId}&isOwner=${isOwner}`,
        success: (html) => {
            $(`#${id}`).append(html)
        },
        error: (jqXHR, exception) => {
            alert(`Status code: ${jqXHR} with message ${exception}`);
        }
    });
}

function displayUserMessage(connection) {
    connection.on("GetMessageIdAndUserNameForConsultant",
        (messageId, userName) => displayMessage(messageId, false, userName));
}

async function displayNewChat(connection) {
    connection.on("GetNewUserName", async function (userName) {
        if (!$(`chat-${userName}`).length) {
            $.ajax({
                method: 'GET',
                url: `/User/SupportChat/ChatForConsultant?userName=${userName}`,
                success: async (html) => {
                    $('#chats').append(html);
                    await refreshHandlerAsync(connection, userName);
                },
                error: (jqXHR, exception) => {
                    alert(`Status code: ${jqXHR} with message ${exception}`);
                }
            });
        }

        $(`#delete-chat-${userName}`).hide();
    });
}

function notifyWhenUserCloseChat(connection) {
    connection.on("GetUserNameWhenCloseByUser", function (userName) {
        removeChatBy(userName);
        alert(userName + " close chat");
    });
}

async function connectConsultant(connection) {
    await connection.invoke("ConnectConsultant");
}

function displayButtonToDeleteChat(connection) {
    connection.on("GetUserNameWhenLeft", function (userName) {
        $(`#delete-chat-${userName}`).show();
    });
}

async function startChatAsync() {
    const connection = establishConnection('/chat');
    await disconnectByClick(connection);
    await refreshChatsByClick(connection);
    displayUserMessage(connection);
    await displayNewChat(connection);
    displayButtonToDeleteChat(connection);
    notifyWhenUserCloseChat(connection);
    await connection.start();
    await connectConsultant(connection);
}

$(document).ready(function () {
    $('#online').click(async () => {
        await startChatAsync();
    });
});