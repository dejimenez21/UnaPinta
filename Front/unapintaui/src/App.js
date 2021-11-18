import React, { Component } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from './components/userequester/login/login';
import RequestHistory from './components/userequester/requestsHistory/requestHistory';
import DonorRegister from './components/userequester/register/donorRegister';

class App extends Component {
  render() {
    return (
    <Router>
        <Routes>
            <Route exact path='/' element={<Login/>} />
            <Route path='/userRequestHistory' element={<RequestHistory/>} />
            <Route path='/donorRegister' element={<DonorRegister/>} />
        </Routes>
      </Router>
    );
  }
}

export default App;