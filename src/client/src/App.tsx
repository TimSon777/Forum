import React, {useEffect, useState} from 'react';

import './App.css';
import CustomTextArea from "./components/custom-text-area";
import MessageBox, {setUpSignalRConnection} from "./components/message-box";
import MessageArea from "./components/message-area";
import axios from "axios";



function App() {
    const [messages, setMessages] = useState([
        {Id: 1, Name: 'Name', Text: 'text texr text text'}
    ])

    async function fetchMessages(count: number) {
        const response = await axios.get("http://localhost:5091/history/10");
        console.log(response.data);
        setMessages(response.data)
    }

    //useEffect(() => {
        setUpSignalRConnection();
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
