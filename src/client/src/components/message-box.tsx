import React from 'react';
import '../App.css';

export interface User {
    name: string;
}

export interface GetMessageItem {
    user: User;
    text: string;
}

export interface SendMessageItem {
    iPv4: number;
    text: string;
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