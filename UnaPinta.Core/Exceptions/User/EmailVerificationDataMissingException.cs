using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.User
{
    public class EmailVerificationDataMissingException : BaseDomainException
    {
        const string defaultMessage = "User id or token missing";
        public EmailVerificationDataMissingException(string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E3001";
            this.Title = "Email verification data missing";
            this.spMessage = "Para la verificación del correo es necesario que tanto el id del usuario como el token sean provistos.";
        }

        public EmailVerificationDataMissingException() : this(defaultMessage) { }
    }
}
