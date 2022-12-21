import {Button, Checkbox, FormControlLabel, FormGroup } from '@mui/material';
import React, { useState } from 'react';

const AdminPage: React.FC = () => {
    const [isChecked, setIsChecked] = useState(false);

    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setIsChecked(event.target.checked);
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
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
