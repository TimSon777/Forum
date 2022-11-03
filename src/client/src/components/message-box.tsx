import {useEffect, useState} from 'react';
import React from 'react';
import '../App.css';
import axios from "axios";
import * as stream from "stream";

export interface GetMessageItem {
    name: string;
    text: string;
    fileKey: string | null;
}

export interface SendMessageItem {
    iPv4: number;
    text: string;
    fileKey: string | null;
}

interface MessageBoxProps {
    message: GetMessageItem;
}

const MessageBox = (props: MessageBoxProps) => {
        return (
            <div className={"message-box-container"}>
                <div className={"message-box-image-container"}>
                    <img className={"message-box-image"} src={require('../images/wallpaperflare.com_wallpaper.jpg')} alt={"Photo"}
                    width={"64px"} height={"64px"}/>
                </div>
                
                <div className={"message-box-text-container"}>
                    <div className={"message-box-user-name"}>
                        {props.message.name}  
                    </div>
                    
                    <div className={"message-box-text"}>
                        {props.message.text}
                    </div>

                    {props.message.fileKey && (
                        <div>
                            <a href={process.env.REACT_APP_FILE_API + "/file" + `/${props.message.fileKey}`} 
                               download={props.message.name}>
                                {props.message.fileKey} 
                            </a>
                        </div>
                    )}
                </div>
            </div>
        );
}

export default MessageBox;