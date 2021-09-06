using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.User
{
    public class UserNotFoundException : BaseDomainException
    {
        const string defaultMessage = "The user doesn't exist";
        public UserNotFoundException(string message, string userId) : base(message)
        {
            this.StatusCode = 404;
            this.ErrorCode = "E3000";
            this.Title = "Usuario no existe";
            this.spMessage = $"El usuario con el id '{userId}' no existe.";
        }

        public UserNotFoundException(string userId) : this(defaultMessage, userId) { }
    }
}
