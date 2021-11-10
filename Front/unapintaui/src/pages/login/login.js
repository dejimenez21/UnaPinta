import React from 'react'
import { Form } from 'react-bootstrap'
import { Button } from 'react-bootstrap'

class Login extends React.Component {
    render(){
        return(
            <div className="container">
                <Form>
                    <Form.Group>
                        <Form.Control type="text" placeholder="Ingrese su nombre de usuario"/>
                        <Form.Control type="password" placeholder="Ingrese su contraseña"/>
                    </Form.Group>
                <Button variant="primary" type="submit">
                    Ingresar
                </Button>
                </Form>
            </div>
        )
    }
}

export default Login;