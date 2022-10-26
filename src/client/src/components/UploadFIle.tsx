import React, {useRef, useState} from 'react';
import {Button, ButtonGroup} from "@mui/material";

interface UploadedFile {
    name: string;
    type: string;
}

export const UploadFIle = () => {
    const filePicker = useRef<any>(null);
    const [selectedFile, setSelectedFile] = useState<UploadedFile>();
    
    const  handleChange = (event: any) => {
        console.log(event.target.files);
        setSelectedFile(event.target.files[0]);
    };
    
    const handleUpload = async () => {
        if (!selectedFile) {
            alert("No file");
            return;
        }
    };

    function handlePick() {
        filePicker.current.click();
    }

    return (
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
                
                <Button color="inherit" onClick={handleUpload}>
                    UPLOAD
                </Button>
            </ButtonGroup>

            {selectedFile && (
                <div className={"file-name-container"}>
                     {selectedFile.name}
                </div>
            )}
            
        </div>
    );
};

