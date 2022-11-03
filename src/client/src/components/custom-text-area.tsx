import React, {FormEvent, useEffect, useRef, useState} from 'react';
import '../App.css';
import CustomInput from "./ui/custom-input";
import {SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";
import axios from "axios";
import { useForm } from 'react-hook-form';
import {Button, ButtonGroup} from "@mui/material";

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
    const [selectedFile, setSelectedFile] = useState();
    
    const [fileKey, setKey] = useState(''); 

    const  handleChange = (event: any) => {
        console.log(event.target.files[0]);
        setSelectedFile(event.target.files[0]);
    };

    const handleUpload = async () => {
        if (selectedFile) {
            const formData = new FormData();
            formData.set('file', selectedFile!);
            console.log('formdata:' + formData);
            axios.create({
                baseURL: process.env.REACT_APP_FILE_API,
            }).post("/file", formData)
                .then(async function (response) {
                    const data = response.data;
                    const key: string = data.key;
                    console.log("Key: " + key);
                    setKey(key);
                    console.log("Key after set key: " + fileKey);
                })
                .catch(function (response) {
                    console.log(response);
                })
                .finally(() => {
                    setSelectedFile(undefined);
                });
        }
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
                console.log("IP hello", ip, value.data.IPv4);
            })
            .catch(err => {
                console.log(err);
                setLoaded(true);
            });
    }, []);

    const [message, setMessage] = useState<{text: '', fileKey: string | null}>({text: '', fileKey: null});

    const onFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        
        e.preventDefault();
        
        handleUpload().then(() => {
            console.log(`After handle upload: ${fileKey}`)
        });
        console.log("Prosto: " + fileKey);
        setAlert(false);
        const sendMessageItem: SendMessageItem = {iPv4: ip, text: message.text, fileKey: fileKey != '' ? fileKey : null};
        console.log(sendMessageItem);
        await connection.invoke('SendMessageAsync', sendMessageItem)
            .catch(err => {
                console.log(err);
                setAlert(true);
            });
        
        console.log("Message: " + message.text + " " + message.fileKey);
        
        setMessage({text: '', fileKey: null});
        setKey('');
    }
    
    return (
        <>
            {isLoaded && <form className="custom-text-area-form" onSubmit={onFormSubmit} >
                <CustomInput
                    {...{ required: true, maxLength: 500}}
                    value={message.text}
                    type={"text"}
                    onChange={(e: any) => setMessage({text: e.target.value, fileKey: fileKey != '' ? fileKey : null})}
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
                            File selected
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