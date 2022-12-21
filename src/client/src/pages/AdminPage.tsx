import {Button, Checkbox, FormControlLabel, FormGroup } from '@mui/material';
import axios from 'axios';
import React, {useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface Ip {
    IPv4: string;
}

interface AuthResponse {
    accessToken: string;
}

const AdminPage: React.FC = () => {
    const [isChecked, setIsChecked] = useState(false);

    const navigate = useNavigate();

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
        
        let authResponse = await axios.post<AuthResponse>(process.env.REACT_APP_AUTH_SERVER as string, form);
        localStorage.setItem("access_token", authResponse.data.accessToken);
        //TODO
        navigate('/');
    };

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
