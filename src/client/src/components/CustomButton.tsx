import React, {ReactNode} from 'react';

interface Props {
    children?: string,
}

const CustomButton = ({children, ...props}: Props) => {
    return (
        <button className={"send-button"}>
            {children}
        </button>
    );
};

export default CustomButton;