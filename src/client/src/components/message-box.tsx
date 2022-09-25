import React from 'react';
import '../App.css';

class MessageBox extends React.Component {
    render() {
        return (
            <div className={"message-box-container"}>
                <div className={"message-box-image-container"}>
                    <img className={"message-box-image"} src="https://i.pinimg.com/564x/65/6c/e7/656ce70e02adb9b43b278b4c8374d56e.jpg" alt={"Photo"}
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
}

export default MessageBox;