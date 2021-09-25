using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.User
{
    public class InvalidCredentialsException : BaseDomainException
    {
        const string defaultMessage = "Incorrect username or password.";

        public InvalidCredentialsException(string message) : base(message)
        {
            this.StatusCode = 401;
            this.ErrorCode = "E3004";
            this.Title = "Credenciales invalidas";
            this.spMessage = "Usuario y/o contraseña incorrectos.";
        }

        public InvalidCredentialsException() : this(defaultMessage) { }
    }
}
