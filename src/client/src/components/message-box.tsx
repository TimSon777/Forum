import React from 'react';
import '../App.css';
import {HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {log} from "util";

interface GetMessageItem {
    name: string;
    text: string;
}

export interface SendMessageItem {
    iPv4: number;
    port: number;
    text: string;
}

//export const configureConnection = async (setMessage: (fc: () => void) => void) => {

export const configureConnection = async () => {
    console.log('here')
    const connection = new HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_ORIGIN_API + '/forum')
        .build();

    console.log(connection);
    
    try {
        await connection.start().then(async () => {
            connection.on('ReceiveMessage', (message: GetMessageItem) => {
               console.log(message.text + " " + message.name);
              // setMessage(message);
            });

            const sendMessageItem: SendMessageItem = {iPv4: 114, text: "Some text", port: 3534}

            if (connection.state === HubConnectionState.Connected) {
                console.log(connection.state);
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