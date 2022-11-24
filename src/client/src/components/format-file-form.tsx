import React, {useState} from 'react';
import Input from '@mui/material/Input';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

enum Format{
    Image,
    Video,
    Audio, 
    Other
}

interface FormatFileFormInterface {
    format: string;
    setSelectedFile: any;
    setModalActive: React.Dispatch<React.SetStateAction<boolean>>;
}

const FormatFileForm = ({format, setModalActive, setSelectedFile} : FormatFileFormInterface) => {

    const [fileName, setFileName] = useState('');

    const [error, setError] = useState(false);

    const handleClick = () => {
        if (!fileName) {
            setError(true);
            return null;
        }
        else {
            setModalActive(false);
        }
    };
    
    const handleChangeValue = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setError(false);
        setFileName(e.target.value);
    };
    
    if (format.toString() == "Image"){
        
        //                
        //                 <Button onClick={handleClick}>
        //                     Submit
        //                 </Button>
        return (
            <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    value={fileName}
                    error={!!error}
                    onChange={(e) => handleChangeValue(e)}
                    helperText={error ? 'this is required' : ''}
                />

                <Button color="inherit" onClick={() => {
                    setModalActive(false);
                    setSelectedFile(undefined);
                }}> Cancel </Button>

                <Button color="primary" onClick={() => {
                    handleClick();
                }}>
                    Submit </Button>
            </div>
        );
    }
    else if (format.toString() == "Video"){
        return (
            <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    />
                <TextField
                    required
                    id="outlined-required"
                    type="number"
                    label="Duration"
                />
            </div>
        );
    }
    else if (format.toString() == "Audio"){
        return (
            <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                />
                <TextField
                    required
                    id="outlined-required"
                    type="number"
                    label="Duration"
                />
            </div>
        );
    }
    else if (format.toString() == "Other"){
        return (
            <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                />
            </div>
        );
    }
    else {
        return (
            <div>
                Select file format
            </div>
        );
    }
};

export default FormatFileForm;
