import React, {useEffect, useRef} from 'react';
import '../App.css';
import MessageBox from "./message-box";


const MessageArea = ({messages}: any) => {
    
    const messagesEndRef = useRef<null | HTMLDivElement>(null);
    
    const scrollToBottom = () => {
        if (messagesEndRef.current)
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
    };
    
    useEffect(scrollToBottom, [messages]);

    return (
        <div className={"message-area-container"}>
            {messages.map((message: any) =>
                <MessageBox message={message}></MessageBox>
            )}
            <div ref={messagesEndRef} />
        </div>
    )
};

export default MessageArea;