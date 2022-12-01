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
    onChangeValue: Function
}

const FormatFileForm = ({format, setModalActive, setSelectedFile,onChangeValue} : FormatFileFormInterface) => {

    const [fileName, setFileName] = useState('');
    const [otherFormat, setOtherFormat] = useState('');
    const [duration, setDuration] = useState(0);
    const [author, setAuthor] = useState('');
    
    let form;
    
    if (format.toString() == "Image"){
        form =  <div>
                <TextField
                    required
                    id="outlined-required"
                    label="File name"
                    name={"imageFileName"}
                    value={fileName}
                    onChange={(e) => {
                        setFileName(e.target.value);
                        onChangeValue(e.target);
                    }}
                />
            </div>
    }
    else if (format.toString() == "Video"){
        form = <div>
                <TextField
                    required
                    id="outlined-required"
                    name={"videoFileName"}
                    label="File name"
                    value={fileName}
                    onChange={(e) => {
                        setFileName(e.target.value);
                        onChangeValue(e.target);
                    }}
                    />
                <TextField
                    required
                    id="outlined-required"
                    type="number"
                    name={"videoDuration"}
                    label="Duration"
                    value={duration}
                    onChange={(e) => {
                        setDuration(Number(e.target.value));
                        onChangeValue(e.target);
                    }}
                />
            </div>
    }
    else if (format.toString() == "Audio"){
        form =  <div>
                <TextField
                    required
                    id="outlined-required"
                    name={"audioFileName"}
                    label="File name"
                    value={fileName}
                    onChange={(e) => {
                        setFileName(e.target.value);
                        onChangeValue(e.target);
                    }}
                />
                <TextField
                    required
                    id="outlined-required"
                    type="number"
                    name={"audioDuration"}
                    label="Duration"
                    value={duration}
                    onChange={(e) => {
                        setDuration(Number(e.target.value));
                        onChangeValue(e.target);
                    }}
                />
                <TextField
                    required
                    id="outlined-required"
                    name={"audioAuthor"}
                    label="Author"
                    value={author}
                    onChange={(e) => {
                        setAuthor(e.target.value);
                        onChangeValue(e.target);
                    }}
                />
            </div>
    }
    else if (format.toString() == "Other"){
        form =  <div>
                <TextField
                    required
                    id="outlined-required"
                    name={"otherFileName"}
                    label="File name"
                    value={fileName}
                    onChange={(e) => {
                        setFileName(e.target.value);
                        onChangeValue(e.target);
                    }}
                />
                <TextField
                    required
                    id="outlined-required"
                    name={"otherFileFormat"}
                    label="Format"
                    value={format}
                    onChange={(e) => {
                        setOtherFormat(e.target.value);
                        onChangeValue(e.target);
                    }}
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
