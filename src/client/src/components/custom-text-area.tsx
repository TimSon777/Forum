import React, {useEffect, useState} from 'react';
import '../App.css';
import CustomInput from "./CustomInput";
import {configureConnection, SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";


const CustomTextArea = () => {
    let connection: HubConnection;
    
    const [message, setMessage] = useState({text: ''});

    const onFormSubmit = async (e: HTMLFormElement) => {
        e.preventDefault();
        const sendMessageItem: SendMessageItem = {iPv4: 114, text: "Text from connection", port: 3534}
        connection.invoke('SendMessageAsync', sendMessageItem)
            .then(r => console.log(r)) 
                .catch(e => console.log(e)) ;
        
    }
    
    useEffect(() => {
        const cnct = async () => {
            connection = await configureConnection();
            console.log(connection)
        }
        
        cnct();
        console.log(connection);
       
    }, [])
    
    return (
            <form className="custom-text-area-form" onSubmit={e => onFormSubmit} >
                    <CustomInput type={"text"}></CustomInput>
            </form>
        );
}

export default CustomTextArea;