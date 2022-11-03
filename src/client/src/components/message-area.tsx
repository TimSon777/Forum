import React, {useEffect, useRef} from 'react';
import '../App.css';
import MessageBox, {GetMessageItem} from "./message-box";

interface MessageAreaProprs {
    messages: GetMessageItem[]
}

const MessageArea = ({messages}: MessageAreaProprs) => {
    
    const messagesEndRef = useRef<null | HTMLDivElement>(null);
    
    const scrollToBottom = () => {
        if (messagesEndRef.current)
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
    };
    
    useEffect(scrollToBottom, [messages]);

    return (
        <div className={"message-area-container"}>
            {messages.map((message: GetMessageItem) =>
                <MessageBox message={message}></MessageBox>
            )}
            <div ref={messagesEndRef} />
        </div>
    )
};

export default MessageArea;