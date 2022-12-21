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
import { BsFillCursorFill } from "react-icons/bs";
import {Guid} from "guid-typescript";

interface Props {
    connection: HubConnection;
    fileKey: string | null;
}

interface UploadedFile {
    name: string;
    type: string;
}

const ForumForm = ({connection, fileKey}: Props) => {
    const {
            formState: { errors },
    } = useForm();

    const [customAlert, setCustomAlert] = useState(false);
    
    let [ip, setIp] = useState(1);
    let [isLoaded, setLoaded] = useState(false);

    const filePicker = useRef<any>(null);
    const [selectedFile, setSelectedFile] = useState<UploadedFile>();
    
    const [modalActive, setModalActive] = useState(false);

    const [fileFormat, setFileFormat] = useState('Other');
    
    const [fileName, setFileName] = useState('');
    const [otherFormat, setOtherFormat] = useState('');
    const [duration, setDuration] = useState(0);
    const [author, setAuthor] = useState('');
    
    const [isSend, setIsSend] = useState(false);

    const [fileUploaded, setFileUploaded] = useState(false);

    
    const handleSelectChange = (event: SelectChangeEvent) => {
        setFileFormat(event.target.value);
    };

    const handleChange = (event: any) => {
        console.log(event.target.files[0]);
        setSelectedFile(event.target.files[0]);
        setModalActive(true);
    };

    const handleUpload = async () => {
        if (selectedFile) {
            const requestId = Guid.raw();
            const formData = new FormData();
            // @ts-ignore
            formData.set('File', selectedFile!);
            formData.append('RequestId', requestId);
            
            try {
                await connection.invoke("SaveConnectionId", requestId);
                let metadata = createMetadata(requestId)
                
                let requests = [];
                requests.push(axios.post(process.env.REACT_APP_FILE_API as string, formData));
                requests.push(axios.post(process.env.REACT_APP_METADATA_API as string, metadata, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }));

                await Promise.all(requests);
                setIsSend(true);
            }
            catch (response) {
                    console.log(response);
                    alert("File cannot be uploaded!");
            }
            finally {
                    setSelectedFile(undefined);
            };
        }
    };

    function handlePick() {
        filePicker.current.click();
    };

    const [message, setMessage] = useState<{text: '', fileKey: string | null}>({text: '', fileKey: null});

    const onFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        
        e.preventDefault();
        
        if ((fileKey === null || fileKey === '') && isSend){
            alert("File not uploaded");
        }
        else {
            setCustomAlert(false);
            const sendMessageItem: SendMessageItem = {iPAddress: ip, text: message.text, fileKey: fileKey};

            await connection.invoke('SendMessage', sendMessageItem)
                .catch(err => {
                    console.log(err);
                    setCustomAlert(true);
                });

            setMessage({text: '', fileKey: null});
            setIsSend(false);
            setFileUploaded(false);
        }
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
    
    const createMetadata = (requestId : string): string => {
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


        let obj =
            {
                requestId: requestId,
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
                            onClick={(event: any)=> {
                                event.target.value = null
                            }}
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
                                    
                                <Button type={"button"} color="primary" onClick={async (e) => {
                                    setModalActive(false);
                                    await handleUpload();
                                }}>
                                    Submit
                                </Button>

                                <Button type={"button"} color="warning" onClick={() => {
                                        setSelectedFile(undefined);
                                        setModalActive(false);
                                    }}> 
                                    Cancel 
                                </Button>
                                </Box>
                            
                        </FileDataModal>
                    </>
                )}
                
                <Button name={"send-message-button"} disabled={!isSend} color={"inherit"}>
                    <BsFillCursorFill size={25} ></BsFillCursorFill>
                </Button>
                
            </form> }

            <CustomAlert isAlert={fileUploaded} color={"white"}>
                File is loading...
            </CustomAlert>
            
            <CustomAlert isAlert={customAlert} color={"white"}>
                SERVER ERROR
            </CustomAlert>
        </>
        );
}

export default ForumForm;
