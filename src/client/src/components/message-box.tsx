import React, {useEffect, useState} from 'react';
import '../App.css';
import {HubConnection, HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {log} from "util";
import axios from "axios";
import exp from "constants";

export interface GetUserItem {
    name: string;
}

export interface GetMessageItem {
    user: GetUserItem;
    text: string;
}

export interface SendMessageItem {
    iPv4: number;
    port: number;
    text: string;
}

interface Props {
    addMessage: (m: SendMessageItem) => void;
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
                        {props.message.user.name}
                    </div>
                    <div className={"message-box-text"}>
                        {props.message.text}
                    </div>
                </div>
            </div>
        );
}

export default MessageBox;