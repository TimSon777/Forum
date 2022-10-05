import React from 'react';
import '../App.css';
import {HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";

interface GetMessageItem {
    Name: string;
    Text: string;
}

interface SendMessageItem {
    IPv4: number;
    Port: number;
    Text: string;
}

export const setUpSignalRConnection = async () => {
    const connection = new HubConnectionBuilder()
        .withUrl('http://localhost:5091/forum')
        .build();

    try {
        await connection.start().then(async () => {
            connection.on('ReceiveMessage', (message: GetMessageItem) => {
                console.log(message.Text + " " + message.Name)
            });

            const sendMessageItem: SendMessageItem = {IPv4: 114, Text: "Some text", Port: 3534}

            if (connection.state === HubConnectionState.Connected) {
                await connection.invoke('SendMessageAsync', sendMessageItem);
            }
        });

    } catch (err) {
        console.log(err);
    }

    return connection;
}

const MessageBox = (props: any) => {
        return (
            <div className={"message-box-container"}>
                <div className={"message-box-image-container"}>
                    <img className={"message-box-image"} src="https://i.pinimg.com/564x/0b/6d/b2/0b6db2a77b8ad28a87cc7450a6628691.jpg" alt={"Photo"}
                    width={"64px"} height={"64px"}/>
                </div>
                
                <div className={"message-box-text-container"}>
                    <div className={"message-box-user-name"}>
                        {props.message.Name}
                    </div>
                    <div className={"message-box-text"}>
                        {props.message.Text}
                    </div>
                </div>
            </div>
        );
}

export default MessageBox;