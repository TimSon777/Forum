import React from 'react';
import '../App.css';

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

const MessageBox = (props: any) => {
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
                </div>
            </div>
        );
}

export default MessageBox;