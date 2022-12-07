import React from 'react';
import {HubConnection} from "@microsoft/signalr";


interface AlertProps {
    isAlert: boolean;
    children: React.ReactNode;
    color: string
}

const CustomAlert = ({isAlert, children, color}: AlertProps) => {
    return (
        <div className={"alert"} style={{ display: isAlert ? "block" : "none" , color: color}}>
            {children}
        </div>
    );
};

export default CustomAlert;