import React, { Component } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from './components/userequester/login/login';
import RequestHistory from './components/userequester/requestsHistory/requestHistory';
import DonorRegister from './components/userequester/register/donorRegister';
import RequesterRegister from './components/userequester/register/donorRegister';

class App extends Component {
  render() {
    return (
    <Router>
        <Routes>
            <Route exact path='/' element={<Login/>} />
            <Route path='/userRequestHistory' element={<RequestHistory/>} />
            <Route path='/donorRegister' element={<DonorRegister/>} />
            <Route path='/requestRegister' element={<RequesterRegister/>} />
        </Routes>
      </Router>
    );
  }
}

export default App;