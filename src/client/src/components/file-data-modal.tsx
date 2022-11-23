import React from 'react';
import {Button, ButtonGroup} from "@mui/material";

interface FileDataModalProps {
    active: boolean; 
    setActive:  React.Dispatch<React.SetStateAction<boolean>>;
    children: React.ReactNode;
}

const FileDataModal = ({active, setActive, children} : FileDataModalProps) => {
    return (
        <div className={active ? "modal active" : "modal"}>
            <div className={active ? "modal-content active" : "modal-content"}>
                {children}
            </div>
        </div>
    );
};

export default FileDataModal;