using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.User
{
    public class EmailVerificationTokenInvalidException : BaseDomainException
    {
        const string defaultMessage = "Invalid token";
        public EmailVerificationTokenInvalidException(string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E3003";
            this.Title = "Token invalido";
            this.spMessage = "El token enviado no es válido.";
        }

        public EmailVerificationTokenInvalidException() : this(defaultMessage) { }
    }
}
