import React, {useEffect, useState} from 'react';

import './App.css';
import CustomTextArea from "./components/custom-text-area";
import MessageBox, {configureConnection} from "./components/message-box";
import MessageArea from "./components/message-area";
import axios from "axios";


function App() {
    const [messages, setMessages] = useState([
        {Id: 1, Name: 'Name', Text: 'text texr text text'},
        {Id: 2, Name: 'Name', Text: 'text texr text text'},
        {Id: 3, Name: 'Name', Text: 'text texr text text'},
        {Id: 4, Name: 'Name', Text: 'text texr text text'},
        {Id: 5, Name: 'Name', Text: 'text texr text text'},
        {Id: 6, Name: 'Name', Text: 'text texr text text'},
        {Id: 7, Name: 'Name', Text: 'text texr text text'},
        {Id: 8, Name: 'Name', Text: 'text texr text text'}
    ])

     function fetchMessages() {
        const response = axios.get(process.env.REACT_APP_ORIGIN_API + '/history/20').then(value => {
            console.log(value.data);
            setMessages(value.data);
        })
            .catch(err => console.log(err));
    }

    //useEffect(() => {
     //   configureConnection(setMessages);
   // configureConnection();
   // }, []);
    
  return (
      <div className="App">
          <div className="chat-container">
              <h1 className={"forum-header"}>FORUM</h1>
              <MessageArea>
                  {messages.map(message =>
                      <MessageBox message={message} key={message.Id}></MessageBox>
                  )}
              </MessageArea>
              <CustomTextArea/>
          </div>
      </div>
  );
}

export default App;
