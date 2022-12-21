import {Button, Checkbox, FormControlLabel, FormGroup } from '@mui/material';
import axios from 'axios';
import React, {useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Chat from './Chat';
import jwt_decode from "jwt-decode";


interface Ip {
    IPv4: string;
}

interface AuthResponse {
    accessToken: string;
}

interface Claims {
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string;
    "admin": string;
}


const AdminPage: React.FC = () => {
    const [isChecked, setIsChecked] = useState(false);

    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setIsChecked(event.target.checked);
    };
    
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let ipResponse = await axios.get<Ip>('https://geolocation-db.com/json/');
        const ipInArr = ipResponse.data.IPv4.split(".").map(x => parseInt(x));
        const ip = ipInArr[0] * 256 * 256 * 256 + ipInArr[1] * 256 * 256 + ipInArr[2] * 256 + ipInArr[3];
        
        let form = new FormData();
        form.set("userName", ip.toString())
        form.set("isAdmin", isChecked.toString());
        
        let authResponse = await axios.post<AuthResponse>(process.env.REACT_APP_AUTH_SERVER + "/connect/token", form);
        localStorage.setItem("access_token", authResponse.data.accessToken);
    };
    
    let jwtToken = localStorage.getItem("access_token");
    
    if (jwtToken){
        let decode = jwt_decode(jwtToken) as Claims;
        let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        console.log("TOKEN:" + username);
        return(<Chat username={username}></Chat>);
    }

    return (
        <form className={"admin-form"} onSubmit={handleSubmit}>
            <FormGroup>
                <FormControlLabel control={<Checkbox checked={isChecked} onChange={handleCheckboxChange}/>} label="Admin?" />
            </FormGroup>
            <Button type="submit">Submit</Button>
        </form>
    );
};

export default AdminPage;
