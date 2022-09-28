import React from 'react';
import '../App.css';


//class CustomTextArea extends React.Component<{}, { value: string }> {
const CustomTextArea = () => {
        return (
            <form className="custom-text-area-form" >
                    <textarea className="custom-text-area"/>
            </form>
        );
}

export default CustomTextArea;