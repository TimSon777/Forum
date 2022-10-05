import React, {useEffect} from 'react';
import MessageArea, {setUpSignalRConnection} from "./components/message-area";
import './App.css';
import CustomTextArea from "./components/custom-text-area";
import MessageBox from "./components/message-box";

function App() {
   
    //useEffect(() => {
        setUpSignalRConnection();
   // }, []);
    
  return (
      <div className="App">
          <div className="chat-container">
              <h1 className={"forum-header"}>FORUM</h1>
              <MessageArea>
                  <MessageBox/>
                  <MessageBox/>
                  <MessageBox/>
                  <MessageBox/>
                  <MessageBox/>
                  <MessageBox/>
                  <MessageBox/>
                  <MessageBox/>
              </MessageArea>
              <CustomTextArea/>
          </div>
      </div>
  );
}

export default App;
