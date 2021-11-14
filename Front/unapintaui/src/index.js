import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import Login from './components/userequester/login/login';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';


// ReactDOM.render(
//   <App/>,
//   document.getElementById('root')
// );

ReactDOM.render(
  <Login/>,
  document.getElementById('root')
);



// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
