import React, {FormEvent, useEffect, useState} from 'react';
import '../App.css';
import CustomInput from "./CustomInput";
import {SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";

interface Props {
    connection: HubConnection;
}

const CustomTextArea = ({connection}: Props) => {
    
    const [message, setMessage] = useState({text: ''});

    const onFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log("Here");
        
        const sendMessageItem: SendMessageItem = {iPv4: 114, text: message.text, port: 3534}
        
        await connection.invoke('SendMessageAsync', sendMessageItem);
        setMessage({text: ''});
    }
    
    return (
            <form className="custom-text-area-form" onSubmit={onFormSubmit} >
                    <CustomInput 
                        value={message.text}
                        type={"text"}
                        onChange={(e: any) => setMessage({text: e.target.value})}
                        ></CustomInput>
            </form>
        );
}

export default CustomTextArea;