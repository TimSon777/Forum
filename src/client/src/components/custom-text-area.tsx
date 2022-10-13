import React, {FormEvent, useEffect, useState} from 'react';
import '../App.css';
import CustomInput from "./ui/custom-input";
import {SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";
import axios from "axios";
import { useForm } from 'react-hook-form';
import {Alert} from "@mui/material";

interface Props {
    connection: HubConnection;
}

interface Ip {
    IPv4: string;
}

const CustomTextArea = ({connection}: Props) => {
    const {
            formState: { errors },
    } = useForm();

    const [alert, setAlert] = useState(false);
    
    let [ip, setIp] = useState(1);
    let [isLoaded, setLoaded] = useState(false);
    
    useEffect(() => {
        axios.get<Ip>('https://geolocation-db.com/json/')
            .then(value => {
                const ipInArr = value.data.IPv4.split(".").map(x => parseInt(x));
                setIp(ipInArr[0] * 256 * 256 * 256 + ipInArr[1] * 256 * 256 + ipInArr[2] * 256 + ipInArr[3]);
                setLoaded(true);
                console.log("IP", ip, value.data.IPv4);
            })
            .catch(err => {
                console.log(err);
            });
    }, []);

    const [message, setMessage] = useState({text: ''});

    const onFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        setAlert(false);
        const sendMessageItem: SendMessageItem = {iPv4: ip, text: message.text};
        await connection.invoke('SendMessageAsync', sendMessageItem)
            .catch(err => {
                console.log(err);
                setAlert(true);
            });
        
        setMessage({text: ''});
    }
    
    return (
        <>
            {isLoaded && <form className="custom-text-area-form" onSubmit={onFormSubmit} >
                <CustomInput
                    {...{ required: true, maxLength: 500}}
                    value={message.text}
                    type={"text"}
                    onChange={(e: any) => setMessage({text: e.target.value})}
                />
            </form> }
                 <div className={"alert"} style={{ display: alert ? "block" : "none" }}>
                      SERVER ERROR
                 </div> 
        </>
        );
}

export default CustomTextArea;