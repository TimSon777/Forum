import React, {FormEvent, useEffect, useRef, useState} from 'react';
import '../App.css';
import CustomInput from "./ui/custom-input";
import {SendMessageItem} from "./message-box";
import {HubConnection} from "@microsoft/signalr";
import axios from "axios";
import { useForm } from 'react-hook-form';
import {Button, ButtonGroup} from "@mui/material";
import { AiFillPushpin } from "react-icons/ai";
import CustomAlert from "./ui/custom-alert";
import FileDataModal from "./file-data-modal";
import Select, { SelectChangeEvent } from '@mui/material/Select';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import FormatFileForm from './format-file-form';

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

const ForumForm = ({connection}: Props) => {
    const {
            formState: { errors },
    } = useForm();

    const [alert, setAlert] = useState(false);
    
    let [ip, setIp] = useState(1);
    let [isLoaded, setLoaded] = useState(false);

    const filePicker = useRef<any>(null);
    const [selectedFile, setSelectedFile] = useState<UploadedFile>();
    
    const [fileKey, setKey] = useState<string | null>(null); 
    
    const [modalActive, setModalActive] = useState(false);

    const [fileFormat, setFileFormat] = React.useState('');

    const handleSelectChange = (event: SelectChangeEvent) => {
        setFileFormat(event.target.value);
    };

    const  handleChange = (event: any) => {
        console.log(event.target.files[0]);
        setSelectedFile(event.target.files[0]);
        
        setModalActive(true);
    };

    const handleUpload : () => Promise<string | null> = async () => {
        if (selectedFile) {
            const formData = new FormData();
            // @ts-ignore
            formData.set('file', selectedFile!);
            console.log('formdata:' + formData);
            console.log(process.env.REACT_APP_FILE_API);
            try {
                const response = await axios.create({
                    baseURL: process.env.REACT_APP_FILE_API,
                }).post("/file", formData);


                const data = response.data;
                const key: string = data.key;
                console.log("Key: " + key);
                setKey(key);
                console.log("Key after set key: " + fileKey);
                return key;
            }
            catch(response) {
                    console.log(response);
                    return null;
                }
            finally {
                    setSelectedFile(undefined);
                };
            
            return null;
        }
        
        return null;
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
        
        await handleUpload().then(async (k) => {
            console.log(`After handle upload: ${fileKey}`)
            console.log("Prosto: " + fileKey);
            setAlert(false);
            const sendMessageItem: SendMessageItem = {iPv4: ip, text: message.text, fileKey: k == '' ? null : k};
            console.log(sendMessageItem);
            await connection.invoke('SendMessageAsync', sendMessageItem)
                .catch(err => {
                    console.log(err);
                    setAlert(true);
                });

            console.log("Message: " + message.text + " " + message.fileKey);

            setMessage({text: '', fileKey: null});
            setKey('');
        });
        
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
                            <AiFillPushpin size={25} />
                        </Button>
                        
                        <input
                            className={"hidden"}
                            type = "file"
                            ref={filePicker}
                            onChange={handleChange}
                            accept={"image/*,text/*"}
                        />
                    </ButtonGroup>
                </div>
            </form> }

            {selectedFile && (
                /*<div className={"file-name-container"}>
                    {selectedFile.name}
                </div>*/
                <FileDataModal active={modalActive} setActive={setModalActive}>
                    <p className={"file-name-text"}>{selectedFile.name}</p>
                    
                    <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                        <InputLabel id="demo-simple-select-standard-label">Format</InputLabel>
                        <Select
                            labelId="demo-simple-select-standard-label"
                            id="demo-simple-select-standard"
                            value={fileFormat}
                            onChange={handleSelectChange}
                            label="Format"
                        >
                            <MenuItem value="">
                                <em>None</em>
                            </MenuItem>
                            <MenuItem value={"Image"}>Image</MenuItem>
                            <MenuItem value={"Video"}>Video</MenuItem>
                            <MenuItem value={"Audio"}>Audio</MenuItem>
                            <MenuItem value={"Other"}>Other</MenuItem>
                        </Select>
                    </FormControl>
                    
                    <div className={"select-file-fields"}>
                        <FormatFileForm format={fileFormat}></FormatFileForm>
                    </div>

                    <Button color="inherit" onClick={() => {
                        setModalActive(false);
                        setSelectedFile(undefined);
                    }}> Cancel </Button>

                    <Button color="primary" onClick={() => {
                        setModalActive(false);
                    }}>
                        Submit </Button>
                </FileDataModal>
            )}

            <CustomAlert isAlert={alert}></CustomAlert>

            
        </>
        );
}

export default ForumForm;