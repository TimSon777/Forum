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
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import FormatFileForm from './format-file-form';
import GUID from '../Guid';
import { BsFillCursorFill } from "react-icons/bs";

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

    const [customAlert, setCustomAlert] = useState(false);
    
    let [ip, setIp] = useState(1);
    let [isLoaded, setLoaded] = useState(false);

    const filePicker = useRef<any>(null);
    const [selectedFile, setSelectedFile] = useState<UploadedFile>();
    
    const [fileKey, setKey] = useState<string | null>(null); 
    
    const [modalActive, setModalActive] = useState(false);

    const [fileFormat, setFileFormat] = useState('Other');
    
    const [requestId, setRequestId] = useState('');


    const [fileName, setFileName] = useState('');
    const [otherFormat, setOtherFormat] = useState('');
    const [duration, setDuration] = useState(0);
    const [author, setAuthor] = useState('');
    
    const [isSend, setIsSend] = useState(false);
    
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
            formData.set('File', selectedFile!);
            formData.append('RequestId', requestId);

            try {
                
                await connection.invoke("SaveConnectionId", requestId);
                
                const response = await axios.post("http://localhost:8083/file", formData);

                let metadata = createMetadata();
                console.log('JSON: ' + metadata);
                
                await axios.post("http://localhost:8082/metadata", metadata);
                
                const data = response.data;
                const key: string = data.key;
                
                setKey(key);
                setIsSend(true);
                alert("File uploaded!");
                return key;
            }
            catch(response) {
                    console.log(response);
                    alert("File cannot be uploaded!");
                    return null;
                }
            finally {
                    setSelectedFile(undefined);
                };
            
            return null;
        }
        
        return null;
    };
    
    const GenerateAndSetRequestId = () => {
            let guid = new GUID().toString();
            setRequestId(guid);
    };

    function handlePick() {
        filePicker.current.click();
    };
    
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
        
        await handleUpload()
            .then(async (k) => {
            setCustomAlert(false);
            const sendMessageItem: SendMessageItem = {iPv4: ip, text: message.text, fileKey: k == '' ? null : k};
            
            await connection.invoke('SendMessage', sendMessageItem)
                .catch(err => {
                    console.log(err);
                    setCustomAlert(true);
                });

            setMessage({text: '', fileKey: null});
            setKey('');
            setRequestId('');
            setIsSend(false);
            });
    }
    
    const handleOnChange = (val: HTMLInputElement) => {
        console.log('fileFormat' + fileFormat);
        
        if (val.name === 'imageFileName' || val.name === 'videoFileName'
            || val.name === 'audioFileName' || val.name === 'otherFileName') {
            setFileName(val.value);
        }
        
        if (val.name === 'videoDuration' || val.name === 'audioDuration') {
            setDuration(Number(val.value));
        }

        if (val.name === 'audioAuthor') {
            setAuthor(val.value);
        }

        if (val.name === 'otherFileFormat') {
            setOtherFormat(val.value);
        }
    }
    
    const createMetadata = (): string => {
        let metadata;
        if (fileFormat === 'Image') {
            metadata = {
                fileName: fileName
            };
        }
        else if (fileFormat === 'Audio') {
            metadata = {
                fileName: fileName,
                duration: duration,
                author: author
            };
        }
        else if (fileFormat === 'Video') {
            metadata = {
                fileName: fileName,
                duration: duration
            };
        }
        else if (fileFormat === 'Other') {
            metadata = {
                fileName: fileName,
                otherFormat: otherFormat
            };
        }

        let guid = new GUID().toString();
        console.log(guid);
        setRequestId(guid)

        let obj =
            {
                requestId: guid,
                metadata: metadata
            };
        
        return JSON.stringify(obj);
    }
    
    return (
        <>
            {isLoaded && <form className="custom-text-area-form" onSubmit={onFormSubmit}>
                <CustomInput
                    {...{ required: true, maxLength: 500}}
                    value={message.text}
                    type={"text"}
                    onChange={(e: any) => setMessage({text: e.target.value, fileKey: fileKey != '' ? fileKey : null})}
                />
                
                <div className={"upload-file-area"}>
                    <ButtonGroup size="large" aria-label="large button group">
                        <Button color="inherit" onClick={handlePick} disabled={!message.text}>
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

                {selectedFile && (
                    <>
                        <FileDataModal active={modalActive} setActive={setModalActive}>
                            
                            <p className={"file-name-text"}>{selectedFile.name}</p>

                            <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>

                                <InputLabel id="demo-simple-select-standard-label">
                                    Format
                                </InputLabel>
                                <Select
                                    labelId="demo-simple-select-standard-label"
                                    id="demo-simple-select-standard"
                                    value={fileFormat}
                                    onChange={handleSelectChange}
                                    label="Format"
                                >
                                    <MenuItem value={"Other"}>Other</MenuItem>
                                    <MenuItem value={"Image"}>Image</MenuItem>
                                    <MenuItem value={"Video"}>Video</MenuItem>
                                    <MenuItem value={"Audio"}>Audio</MenuItem>
                                </Select>

                                <div className={"select-file-fields"}>
                                    <FormatFileForm setSelectedFile={setSelectedFile} setModalActive={setModalActive}
                                                    format={fileFormat}
                                    onChangeValue = {handleOnChange}></FormatFileForm>
                                </div>
                            </FormControl>
                            
                                <Box display="flex" justifyContent={"space-between"} padding={1}>
                                    
                                <Button type={"submit"} color="primary" onClick={(e) => {
                                    setModalActive(false);
                                }}>
                                    Submit
                                </Button>

                                <Button color="warning" onClick={() => {
                                        setModalActive(false);
                                        setSelectedFile(undefined);
                                    }}> 
                                    Cancel 
                                </Button>
                                </Box>
                            
                        </FileDataModal>
                    </>
                )}
                
                <Button name={"send-message-button"} disabled={!isSend} type={"button"} color={"inherit"}>
                    <BsFillCursorFill size={25} ></BsFillCursorFill>
                </Button>
                
            </form> }
            
            <CustomAlert isAlert={customAlert} color={"white"}>
                SERVER ERROR
            </CustomAlert>
        </>
        );
}

export default ForumForm;
