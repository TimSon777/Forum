import React from 'react';
import {HubConnection} from "@microsoft/signalr";


interface AlertProps {
    isAlert: boolean;
}

const CustomAlert = ({isAlert}: AlertProps) => {
    return (
        <div className={"alert"} style={{ display: isAlert ? "block" : "none" }}>
            SERVER ERROR
        </div>
    );
};

export default CustomAlert;