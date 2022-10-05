import React, {FC, ReactNode, useEffect, useState} from 'react';
import '../App.css';
import axios from "axios";

interface Props {
    children?: ReactNode
}


const MessageArea = ({children, ...props}: Props) => {
    return (
    <div className={"message-area-container"}>
        {children}
    </div>
    )
};

export default MessageArea;