using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.User
{
    public class InvalidBirthDateException : BaseDomainException
    {
        const string defaultMessage = "The birthdate is invalid.";
        public InvalidBirthDateException(string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E3005";
            this.Title = "Fecha de Nacimiento Invalida";
            this.spMessage = "La fecha de nacimiento ingresada es invalida.";
        }

        public InvalidBirthDateException() : this(defaultMessage)
        {

        }
    }
}
