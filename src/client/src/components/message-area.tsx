import React, {FC, ReactNode, useEffect, useState} from 'react';
import '../App.css';
import { useParams } from 'react-router-dom';

import {HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";


interface Props {
    children?: ReactNode
}

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
                alert(message.Text);
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

const MessageArea = ({children, ...props}: Props) => {
    return (
    <div className={"message-area-container"}>
        {children}
    </div>
    )
};

export default MessageArea;