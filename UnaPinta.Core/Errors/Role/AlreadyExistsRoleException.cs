using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Errors.Role
{
    public class AlreadyExistsRoleException : BaseDomainException
    {
        const string defaultMessage = "There is already a role with this name";
        public AlreadyExistsRoleException(string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E1000";
            this.Title = "Rol duplicado";
            this.spMessage = "Ya existe un rol con este nombre";
        }

        public AlreadyExistsRoleException() : this(defaultMessage) { }

    }
}
