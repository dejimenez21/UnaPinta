import React, { Component } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from './components/userequester/login/login';
import UserRegister from './components/userequester/register/register';

class App extends Component {
  render() {
    return (
    <Router>
        <div>          
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <ul className="navbar-nav mr-auto">
            <li><Link to={'/'} className="nav-link">Inicio de Sesión</Link></li>
            <li><Link to={'/userRegister'} className="nav-link">Registro de Usuario</Link></li>
          </ul>
          </nav>
          <hr />
          <Routes>
              <Route exact path='/' element={<Login/>} />
              <Route path='/userRegister' element={<UserRegister/>} />
          </Routes>
        </div>
      </Router>
    );
  }
}

export default App;