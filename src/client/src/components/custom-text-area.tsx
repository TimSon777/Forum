import React, {FormEvent, useEffect, useRef, useState} from 'react';
import '../App.css';
import CustomInput from "./ui/custom-input";
import {SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";
import axios from "axios";
import { useForm } from 'react-hook-form';
import {Alert, Button, ButtonGroup} from "@mui/material";
//import {UploadFIle} from "../components/UploadFIle";

interface Props {
    connection: HubConnection;
}

interface Ip {
    IPv4: string;
}

interface UploadedFile {
    name: string;
    type: string;
}

const CustomTextArea = ({connection}: Props) => {
    const {
            formState: { errors },
    } = useForm();

    const [alert, setAlert] = useState(false);
    
    let [ip, setIp] = useState(1);
    let [isLoaded, setLoaded] = useState(false);

    const filePicker = useRef<any>(null);
    const [selectedFile, setSelectedFile] = useState<UploadedFile>();
    const [key, setKey] = useState('');

    const  handleChange = (event: any) => {
        console.log(event.target.files);
        setSelectedFile(event.target.files[0]);
    };

    const handleUpload = async () => {
        if (!selectedFile) {
            return;
        }

        const json = JSON.stringify(selectedFile);
        const blob = new Blob([json], {
            type: 'application/json'
        });

        let formData = new FormData();
        formData.append('file',  blob);

        const response = await fetch(process.env.REACT_APP_FILE_API + "/file", {
            method: 'POST',
            body: formData
        });

        const data = await response.json();

        console.log("Response: " + data.key);
        let key = data.key;

        let fileFromServer = await fetch(process.env.REACT_APP_FILE_API + "/file" + `/${key}`);
        console.log(fileFromServer);
        
        setKey(key);
    };

    function handlePick() {
        filePicker.current.click();
    }
    
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

    const [message, setMessage] = useState({text: '', fileKey: ''});

    const onFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        handleUpload();

        setAlert(false);
        const sendMessageItem: SendMessageItem = {iPv4: ip, text: message.text, fileKey: key};
        await connection.invoke('SendMessageAsync', sendMessageItem)
            .catch(err => {
                console.log(err);
                setAlert(true);
            });
        
        setMessage({text: '', fileKey: ''});
    }
    
    return (
        <>
            {isLoaded && <form className="custom-text-area-form" onSubmit={onFormSubmit} >
                <CustomInput
                    {...{ required: true, maxLength: 500}}
                    value={message.text}
                    type={"text"}
                    onChange={(e: any) => setMessage({text: e.target.value, fileKey: key})}
                />
                
                <div className={"upload-file-area"}>
                    <ButtonGroup size="large" aria-label="large button group">
                        <Button color="inherit" onClick={handlePick}>
                            PICK FILE
                        </Button>

                        <input
                            className={"hidden"}
                            type = "file"
                            ref={filePicker}
                            onChange={handleChange}
                            accept={"image/*,text/*"}
                        />
                    </ButtonGroup>

                    {selectedFile && (
                        <div className={"file-name-container"}>
                            {selectedFile.name}
                        </div>
                    )}
                </div>
                
            </form> }
                 <div className={"alert"} style={{ display: alert ? "block" : "none" }}>
                      SERVER ERROR
                 </div> 
        </>
        );
}

export default CustomTextArea;