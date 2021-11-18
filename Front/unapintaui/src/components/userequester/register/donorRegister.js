import React from "react";

class DonorRegister extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
        name: '',
        lastName: '',
        sex: '',
        age: '',
        bloodType: '',
        bithDate: '',
        email: '',
        password: '',
        weight: '',
        phone: '',
        province: ''
    };
  }  

  render() {
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
                <div class="row g-3">
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Nombre</label>
                  <input type="text" value={this.name} class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Apellidos</label>
                  <input type="text" value={this.lastName} class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Sexo</label>
                  <select class="form-control">
                    <option value="" disabled selected hidden>
                      Seleccionar
                    </option>
                    <option value="true">M</option>
                    <option value="false">F</option>
                  </select>
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Tipo de sangre</label>
                  <select class="form-control">
                    <option value="" disabled selected hidden>
                      Seleccionar
                    </option>
                    <option value="1">A+</option>
                    <option value="2">A-</option>
                    <option value="3">B+</option>
                    <option value="4">B-</option>
                    <option value="5">AB+</option>
                    <option value="6">AB-</option>
                    <option value="7">O+</option>
                    <option value="8">O-</option>
                  </select>
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>
                    Fecha de nacimiento
                  </label>
                  <input type="date" class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Email</label>
                  <input type="email" class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Telefono</label>
                  <input type="text" id="phonetxt" class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Usuario</label>
                  <input type="text" class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Contraseña</label>
                  <input
                    type="password"
                    id="passwordtxt"
                    class="form-control"
                  />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Peso (Kg)</label>
                  <input type="number" class="form-control" />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>
                    Confirmación de contraseña
                  </label>
                  <input
                    type="password"
                    id="confirmpasswordtxt"
                    class="form-control"
                  />
                </div>
                <div class="col-sm-6">
                  <label style={{ fontfamily: "Robot" }}>Provincia</label>
                  <select id="provincesDropdown" class="form-control"></select>
                </div>
              </div>
              <br />
              <div class="text-center">
                <button class="btn btn-danger float-right">
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
