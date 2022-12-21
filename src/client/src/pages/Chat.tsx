import React, {useEffect, useRef, useState} from 'react';
import '../App.css';
import axios from "axios";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {GetMessageItem} from "../components/message-box";
import ForumForm from "../components/forum-form";
import MessageArea from "../components/message-area";
import swal from 'sweetalert2'
import { Spinner } from '@patternfly/react-core';

interface ChatProprs {
    username: string;
    isAdmin: boolean;
}

function Chat({username, isAdmin}: ChatProprs) {
    const [messages, setMessages] = useState<GetMessageItem[]>([]);
    const [connection, setConnection] = useState<HubConnection>();
    const [fileKey, setKey] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        axios.get<GetMessageItem[]>(process.env.REACT_APP_ORIGIN_FORUM_API + '/api/history/20/' + username, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('access_token')}`
            }
        })
            .then(value => {
                setMessages(value.data);
            });
    }, []);

    const ConnectionAlert = (connection: HubConnection, userName: string) => {
        connection.on("ConnectionUp", () => {
            swal.fire(
                `The ${userName} has joined`,
                'Connection Up',
                'success'
            )
        })

        connection.on("ConnectionDown", () => {
            swal.fire(
                `Oh no... The ${userName} has left`,
                'Connection Down',
                'error'
            )
        })
    }
    
    const configureConnection = () => {
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_ORIGIN_FORUM_API + '/forum', {
                accessTokenFactory: () =>  localStorage.getItem("access_token") ?? ''
            })
            .build();
        
        
        try {
            connection.start().then(async () => {
                connection.on('ReceiveMessage', (message: GetMessageItem) => {
                    setMessages((st) => [...st, message]);
                });

                connection.on("ReceiveFileUploadedNotification", (fileId) => {
                    console.log("ReceiveFileUploadedNotification");
                    setKey(fileId);
                });

                connection.on("MateNotFound", () => {
                    setIsLoading(true);
                });
                
                if (isAdmin) {
                    ConnectionAlert(connection, username);
                }
                else {
                    ConnectionAlert(connection, "Admin");
                }
                
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

    {isLoading ? <Spinner size="xl" /> : <div>User didn't connect yet</div>}
    
    if (!connection)
        return <div>Loading...</div>
    
    return (
        <div className="App">
            <div className="chat-container">
                <h1 className={"forum-header"}>FORUM</h1>
                <MessageArea messages={messages}></MessageArea>
                <ForumForm connection={connection} fileKey={fileKey}/>
            </div>
        </div>
    );
}

export default Chat;
