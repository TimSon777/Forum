import React from 'react';
import {BrowserRouter, Link, Route, Router, Routes } from 'react-router-dom';
import './App.css';
import AdminPage from './pages/AdminPage';
import Chat from "./pages/Chat";


function App() {
  return (
              <div>
                  <AdminPage></AdminPage>
              </div>
  );
}

export default App;
