import React from 'react';
import Axios from 'axios';

class RequestHistory extends React.Component {

    fetchRequesterData = (e) =>{

        let getToken = JSON.parse(localStorage.getItem('usertoken'));
        Axios.headers.common = {'Authorization': `bearer ${getToken.token}`}
        Axios.defaults.baseUrl = 'https://localhost:5001/';
        Axios.get("api/Requests/datatable")
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
        <div>

        </div>
        )
    }
}

export default RequestHistory;