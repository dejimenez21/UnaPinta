using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.User
{
    public class TokenBadFormatException : BaseDomainException
    {
        const string defaultMessage = "The format of the token is incorrect.";
        public TokenBadFormatException(string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E3002";
            this.Title = "Formato de token incorrecto";
            this.spMessage = "El token de verificación de correo esta en un formato incorrecto. Favor revise la entrada.";
        }

        public TokenBadFormatException() : this(defaultMessage) { }
    }
}
