import React from 'react';
import axios from 'axios';

class Login extends React.Component {
    constructor(props){
        super(props)

        this.state = {
            userName: '',
            password: ''
        }
    }

    changeHandler = (e) => {
        this.setState({[e.target.name]: e.target.value})
    }

    submitHandler = (e) => {
        e.preventDefault();
        console.log(this.state);
        axios.post("https://localhost:44393/api/Auth/login", this.state).then(response => {
            console.log(response)
        }).catch(error => {
            console.log(error)
        })
        
    }

    render(){
        const{userName, password} = this.state
        return(
            <div className="container p-5">
                <div className="card" style={{boxShadow: "0 4px 8px 0 rgba(0,0,0,0.2)", transition: "0.5s"}}>
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
                </div>
            </div>
        )
    }
}

export default Login;