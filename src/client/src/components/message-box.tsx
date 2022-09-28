import React from 'react';
import '../App.css';

const MessageBox = () => {
        return (
            <div className={"message-box-container"}>
                <div className={"message-box-image-container"}>
                    <img className={"message-box-image"} src="../logo.svg" alt={"Photo"}
                    width={"64px"} height={"64px"}/>
                </div>
                
                <div className={"message-box-text-container"}>
                    <div className={"message-box-user-name"}> 
                        Timur privet
                    </div>
                    <div className={"message-box-text"}>
                        Кто за зож
                    </div>
                </div>
            </div>
        );
}

export default MessageBox;