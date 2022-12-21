import {useEffect, useState} from 'react';
import React from 'react';
import '../App.css';
import axios from "axios";
import * as stream from "stream";
import { FaFileDownload } from 'react-icons/fa';


export interface GetMessageItem {
    userName: string;
    text: string;
    fileKey: string | null;
}

export interface SendMessageItem {
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
                        {props.message.userName}  
                    </div>
                    
                    <div className={"message-box-text"}>
                        {props.message.text}
                    </div>

                    {props.message.fileKey && (
                        <div className={"download-file-container"}>
                            <p className={"file-icon"}>
                                <FaFileDownload />
                            </p>
                            <a className={"file-link"} href={process.env.REACT_APP_FILE_API + `/${props.message.fileKey}`} 
                               download={props.message.userName}>
                                {props.message.fileKey} 
                            </a>
                        </div>
                    )}
                </div>
            </div>
        );
}

export default MessageBox;