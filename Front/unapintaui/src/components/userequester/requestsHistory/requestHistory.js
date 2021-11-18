import React from 'react';
import Axios from 'axios';

class RequestHistory extends React.Component {
  constructor(props){
    super(props)
}
    fetchRequesterData = (e) =>{
        let getToken = JSON.parse(localStorage.getItem('userToken'));
        alert(getToken.token);
        Axios.get("https://localhost:5001/api/Requests/datatable", {
          headers: {
            'Authorization': `token ${getToken.token}`
          }
        })
          .then((response) => {
            // handle success
            console.log(response.data);
          })
          .catch((error) => {
            // handle error
            console.log(error);
          });
    }

    render(){
        return (
        <div className="container">
            <button className="btn btn-primary" onClick={this.fetchRequesterData}>Sync</button>
        </div>
        )
    }
}

export default RequestHistory;