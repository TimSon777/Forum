import React from 'react';
import logo from './logo.svg';
import {
    AppBar
} from "@mui/material";
import MessageArea from "./components/message-area";
import './App.css';


function App() {
    const DUMMY_DATA = [
        {
            senderId: "perborgen",
            text: "who'll win?"
        },
        {
            senderId: "janedoe",
            text: "who'll win?"
        }
    ]
    
    
  return (
      <div className="App">
          <MessageArea></MessageArea>
      </div>
  );
}

export default App;
