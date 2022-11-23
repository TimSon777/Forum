import React from 'react';
import Input from '@mui/material/Input';
import TextField from '@mui/material/TextField';

enum Format{
    Image,
    Video,
    Audio, 
    Other
}

interface FormatFileFormInterface {
    format: string
}

const FormatFileForm = ({format} : FormatFileFormInterface) => {
    if (format.toString() == "Image"){
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
    else if(format.toString() == "Video"){
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
    else if(format.toString() == "Audio"){
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
    else if(format.toString() == "Other"){
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