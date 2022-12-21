import {Button, Checkbox, FormControlLabel, FormGroup, Input, InputLabel } from '@mui/material';
import axios from 'axios';
import React, {useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Chat from './Chat';
import jwt_decode from "jwt-decode";
import swal from "sweetalert2";


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
    const [name, setName] = React.useState('');
    const [isAuthorized, setIsAuthorized] = useState(localStorage.getItem("access_token") !== "" &&
        localStorage.getItem("access_token") !== null);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setName(event.target.value);
    };
    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setIsChecked(event.target.checked);
    };
    
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        /*let ipResponse = await axios.get<Ip>('https://geolocation-db.com/json/');
        const ipInArr = ipResponse.data.IPv4.split(".").map(x => parseInt(x));
        const ip = ipInArr[0] * 256 * 256 * 256 + ipInArr[1] * 256 * 256 + ipInArr[2] * 256 + ipInArr[3];*/
        
        let form = new FormData();
        form.set("userName", name)
        form.set("isAdmin", isChecked.toString());
        
        let authResponse = await axios.post<AuthResponse>(process.env.REACT_APP_AUTH_SERVER + "/connect/token", form);
        localStorage.setItem("access_token", authResponse.data.accessToken);
        setIsAuthorized(true);
    };
    
    if (isAuthorized){
        let jwtToken = localStorage.getItem("access_token") as string;
        let decode = jwt_decode(jwtToken) as Claims;
        let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        let isAdmin = !!decode["admin"];
        console.log("User name:" + username);
        return(<Chat username={username} isAdmin={isAdmin}></Chat>);
    }
    
    return (
        
        <form className={"admin-form"} onSubmit={handleSubmit}>
            <FormGroup>
                <InputLabel htmlFor="username">Name</InputLabel>
                <Input
                    value={name}
                    onChange={handleChange}
                    id="username"
                />
                
                <FormControlLabel control={<Checkbox checked={isChecked} onChange={handleCheckboxChange}/>} label="Admin?" />
            </FormGroup>
            <Button type="submit">Submit</Button>
        </form>
    );
};

export default AdminPage;
