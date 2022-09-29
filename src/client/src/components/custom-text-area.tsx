import React, {useState} from 'react';
import '../App.css';
import CustomButton from "./CustomButton";
import CustomInput from "./CustomInput";


const CustomTextArea = () => {
    return (
            <form className="custom-text-area-form" >
                    <CustomInput type={"text"}></CustomInput>
            </form>
        );
}

export default CustomTextArea;