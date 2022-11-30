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
    const [otherFormat, setOtherFormat] = useState('');
    const [duration, setDuration] = useState(0);
    const [author, setAuthor] = useState('');

    //const [error, setError] = useState(false);

    const handleClick = () => {
       // if (!fileName) {
       //     setError(true);
       //     return null;
       // }
       // else {
            setModalActive(false);
       // }
    };
    
    const handleChangeValue = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
       // setError(false);
        setFileName(e.target.value);
    };

    let form;
    
    if (format.toString() == "Image"){
        form =  <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    value={fileName}
                    onChange={(e) => handleChangeValue(e)}
                />
            </div>
    }
    else if (format.toString() == "Video"){
        form = <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    value={fileName}
                    onChange={(e) => handleChangeValue(e)}
                    />
                <TextField
                    required
                    id="outlined-required"
                    type="number"
                    label="Duration"
                    value={duration}
                />
            </div>
    }
    else if (format.toString() == "Audio"){
        form =  <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    value={fileName}
                    onChange={(e) => handleChangeValue(e)}
                />
                <TextField
                    required
                    id="outlined-required"
                    type="number"
                    label="Duration"
                    value={duration}
                />
                <TextField
                    required
                    id="outlined-required"
                    label="Author"
                    value={author}
                />
            </div>
    }
    else if (format.toString() == "Other"){
        form =  <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    value={fileName}
                    onChange={(e) => handleChangeValue(e)}
                />
                <TextField
                    required
                    id="outlined-required"
                    label="Format"
                    value={format}
                />
            </div>
    }
    else {
        form =  <div>
                Select file format
            </div>
    }
    
    return (
        <div>
            {form}
        </div>
    );
};

export default FormatFileForm;
