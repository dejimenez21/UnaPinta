import React from "react";
import Select from "react-select";
import Axios from "axios";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

class DonorRegister extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      name: "",
      lastName: "",
      sex: false,
      age: "",
      bloodType: 0,
      bithDate: Date.now(),
      email: "",
      password: "",
      weight: 0.0,
      phone: "",
      province: "",
      username: "",
    };
  }

  changeHandler = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };

  submitHandler = (e) => {
    console.log(this.state.name);
  };

  validationHandler = (e) => {

  };

  fetchProvincesData = (e) =>{
    const swal = withReactContent(Swal);
    Axios.get("https://localhost:5001/api/provinces", {
    })
      .then((response) => {
        // handle success
        response.data.forEach(element => {
          this.optionsProvinces.push({value: element.code, label: element.name});          
        });
      })
      .catch((error) => {
        // handle error
        swal.fire({
          title: "",
          text: "No se ha podido descargar las provincias",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "ok",
        });
        console.log(error);
      });
}

  optionsBloodTypes = [
    { value: "1", label: "A+" },
    { value: "2", label: "A-" },
    { value: "3", label: "B+" },
    { value: "4", label: "B-" },
    { value: "5", label: "AB+" },
    { value: "6", label: "AB-" },
    { value: "7", label: "O+" },
    { value: "8", label: "O-" },
  ];

  optionsGenderTypes = [
    { value: "true", label: "M" },
    { value: "false", label: "F" },
  ];

  optionsProvinces = [
    {value: "", label: ""}
  ];

  render() {    
    this.fetchProvincesData();
    return (
      <div className="container">
        <div className="container p-5 mt-lg-5">
          <div
            className="card"
            style={{
              boxShadow: "0 4px 8px 0 rgba(0,0,0,0.2)",
              transition: "0.5s",
              borderRadius: "10px",
            }}
          >
            <div className="card-header text-center">
              <p
                style={{
                  fontfamily: "Roboto",
                  fontSize: "30px",
                  fontWeight: "Bold",
                }}
              >
                Registro de Donante
              </p>
            </div>
            <form onSubmit={this.submitHandler}>
              <div className="card-body">
                <div className="row g-3">
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Nombre</label>
                    <input
                      type="text"
                      name="name"
                      value={this.name}
                      onChange={this.changeHandler}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Apellidos</label>
                    <input
                      type="text"
                      name="lastname"
                      value={this.lastName}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Sexo</label>
                    <Select options={this.optionsGenderTypes} placeholder="Seleccionar"/>
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>
                      Tipo de sangre
                    </label>
                    <Select
                      options={this.optionsBloodTypes}
                      placeholder="Seleccionar"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>
                      Fecha de nacimiento
                    </label>
                    <input
                      type="date"
                      name="bithDate"
                      value={this.bithDate}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Email</label>
                    <input
                      type="email"
                      name="email"
                      value={this.email}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Telefono</label>
                    <input
                      type="text"
                      name="phone"
                      value={this.phone}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Usuario</label>
                    <input
                      type="text"
                      name="username"
                      value={this.username}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Contraseña</label>
                    <input
                      type="password"
                      id="passwordtxt"
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Peso (Kg)</label>
                    <input
                      type="number"
                      name="weight"
                      value={this.weight}
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>
                      Confirmación de contraseña
                    </label>
                    <input
                      name="password"
                      value={this.password}
                      type="password"
                      id="confirmpasswordtxt"
                      className="form-control"
                    />
                  </div>
                  <div className="col-sm-6">
                    <label style={{ fontfamily: "Robot" }}>Provincia</label>
                    <Select
                      options={this.optionsProvinces}
                      placeholder="Seleccionar"
                      name="province"/>
                  </div>
                </div>
                <br />
                <div className="text-end">
                  <button className="btn btn-outline-danger">
                    Guardar
                  </button>
                </div>
              </div>
              <br />
            </form>
          </div>
        </div>
      </div>
    );
  }
}

export default DonorRegister;
