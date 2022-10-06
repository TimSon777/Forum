import React, {FormEvent, useEffect, useState} from 'react';
import '../App.css';
import CustomInput from "./CustomInput";
import {SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";
import axios from "axios";

interface Props {
    connection: HubConnection;
}
interface Ip {
    IPv4: string;
}
const CustomTextArea = ({connection}: Props) => {
    let [ip, setIp] = useState(1);
    let [isLoaded, setLoaded] = useState(false);
    useEffect(() => {
        axios.get<Ip>('https://geolocation-db.com/json/')
            .then(value => {
                const ipInArr = value.data.IPv4.split(".").map(x => parseInt(x));
                setIp(ipInArr[0] * 256 * 256 + ipInArr[1] * 256 * 256 + ipInArr[2] * 256 + ipInArr[3]);
                setLoaded(true);
                console.log("IP", ip, value.data.IPv4)
            })
            .catch(err => console.log(err));
    }, []);
    const [message, setMessage] = useState({text: ''});

    const onFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log("Here");
        console.log(ip)
        const sendMessageItem: SendMessageItem = {iPv4: ip, text: message.text, port: 3534}
        
        await connection.invoke('SendMessageAsync', sendMessageItem);
        setMessage({text: ''});
    }
    
    return (
        <>
            {isLoaded && <form className="custom-text-area-form" onSubmit={onFormSubmit} >
                <CustomInput
                    value={message.text}
                    type={"text"}
                    onChange={(e: any) => setMessage({text: e.target.value})}
                ></CustomInput>
            </form> }</>
        );
}

export default CustomTextArea;