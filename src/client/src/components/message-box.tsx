import React from 'react';
import '../App.css';
import {HubConnection, HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {log} from "util";

export interface GetMessageItem {
    name: string;
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

//export const configureConnection = async (setMessage: (fc: () => void) => void) => {



const MessageBox = (props: any) => {
        return (
            <div className={"message-box-container"}>
                <div className={"message-box-image-container"}>
                    <img className={"message-box-image"} src="https://i.pinimg.com/564x/0b/6d/b2/0b6db2a77b8ad28a87cc7450a6628691.jpg" alt={"Photo"}
                    width={"64px"} height={"64px"}/>
                </div>
                
                <div className={"message-box-text-container"}>
                    <div className={"message-box-user-name"}>
                        {props.message.port}
                    </div>
                    <div className={"message-box-text"}>
                        {props.message.text}
                    </div>
                </div>
            </div>
        );
}

export default MessageBox;