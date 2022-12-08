import React, {useEffect,useState} from 'react';
import '../App.css';
import axios from "axios";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {GetMessageItem} from "../components/message-box";
import ForumForm from "../components/forum-form";
import MessageArea from "../components/message-area";


function Chat() {
    const [messages, setMessages] = useState<GetMessageItem[]>([]);
    const [connection, setConnection] = useState<HubConnection>();

    useEffect(() => {
        axios.get<GetMessageItem[]>(process.env.REACT_APP_ORIGIN_API + '/api/history/20')
            .then(value => {
                setMessages(value.data);
            });
    }, []);

    const configureConnection = () => {
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_ORIGIN_API + '/forum')
            .build();

        try {
            connection.start().then(async () => {
                connection.on('ReceiveMessage', (message: GetMessageItem) => {
                    setMessages((st) => [...st, message]);
                });

                connection.on("ReceiveFileUploadedNotification", () => {
                    alert("File uploaded");
                });
            });

        } catch (err) {
            console.log(err);
        }

        return connection;
    }

    useEffect(() => {
        const cnct =  () => {
            setConnection( configureConnection());
        }
        cnct();
    }, [])

    if (!connection)
        return <div>Loading...</div>
    
    return (
        <div className="App">
            <div className="chat-container">
                <h1 className={"forum-header"}>FORUM</h1>
                <MessageArea messages={messages}></MessageArea>
                <ForumForm connection={connection}/>
            </div>
        </div>
    );
}

export default Chat;
