import React from "react";
import Axios from "axios";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import RequestsHistory from "../requestsHistory/requestHistory";
import { useNavigate } from "react-router-dom";

class Login extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      userName: "",
      password: "",
    };
  }


  handleValidation = (e) => {
    let username = this.state.userName;
    let password = this.state.password;

    if (username.length == 0|| password.length == 0) {
      return false;
    }
  };

  changeHandler = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };

  submitHandler = (e) => {
    const swal = withReactContent(Swal);
    e.preventDefault();
    
    if (this.handleValidation() == false) {
      swal.fire("Existen campos vacios", "", "warning");
    } else {
      Axios
    .post("https://localhost:5001/api/Auth/login", this.state)
    .then((response) => {
      console.log(response.data);
      localStorage.setItem('userToken', JSON.stringify(response.data));
    })
    .catch((error) => {
      swal.fire("Credenciales incorrectos", {error}, "error");
    });
    }
  };

  render() {
    const { userName, password } = this.state;
    return (

        <div className="container p-5 mt-lg-5">
             <div className="card" style={{boxShadow: "0 4px 8px 0 rgba(0,0,0,0.2)", transition: "0.5s", borderRadius: "10px"}}>
             <div className="card-header text-center">
                  <p style={{fontfamily: "Roboto", fontSize:"30px", fontWeight: "Bold"}}>Inicio de Sesión</p>
             </div>
                 <div className="card-body">
                     <form onSubmit={this.submitHandler}>
                         <div className="mb-3">
                             <input type="text" name="userName" value={userName} onChange={this.changeHandler} className="form-control" placeholder="Ingrese su nombre de usuario"/>
                         </div>
                         <br/>
                         <div className="mb-3">
                             <input type="password" name="password" value={password} onChange={this.changeHandler} className="form-control" placeholder="Ingrese su contraseña"/>
                         </div>
                         <br />
                         <button className="btn btn-success" type="submit">
                             Ingresar
                         </button>
                     </form>
                 </div>
                 <div className="card-footer justify-content-end">
                     <a href="#">He olvidado mi contraseña</a>
                 </div>
             </div>
         </div>
    );
  }
}
export default Login;
