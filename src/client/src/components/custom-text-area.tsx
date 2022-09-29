import React, {useState} from 'react';
import '../App.css';
import CustomButton from "./CustomButton";
import CustomInput from "./CustomInput";


const CustomTextArea = () => {

    const [message, setMessage] = useState({text: ''})

    const addNewPost = (e: any) => {
        e.preventDefault()
        setMessage({text: ''})
    }

    return (
            <form className="custom-text-area-form" >
                    <CustomInput type={"text"}></CustomInput>
            </form>
        );
}

export default CustomTextArea;