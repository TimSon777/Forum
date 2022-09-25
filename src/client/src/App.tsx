import React from 'react';
import MessageArea from "./components/message-area";
import './App.css';
import CustomTextArea from "./components/custom-text-area";
import MessageBox from "./components/message-box";


function App() {
  return (
      <div className="App">
          <div className="chat-container">
              <MessageArea>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
                  <MessageBox></MessageBox>
              </MessageArea>
              <CustomTextArea></CustomTextArea>
          </div>
      </div>
  );
}

export default App;
