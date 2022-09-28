import React, {ReactNode} from 'react';
import '../App.css';

interface Props {
    children?: ReactNode
}

const MessageArea = ({children, ...props}: Props) => (
               <div className={"message-area-container"}>
                   {children}
               </div>
       );

export default MessageArea;