using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.Province
{
    public class ProvinceNotFoundException : BaseDomainException
    {
        const string defaultMessage = "The province doesn't exist";
        public ProvinceNotFoundException(string message, string provinceCode) : base(message)
        {
            this.StatusCode = 404;
            this.ErrorCode = "E4000";
            this.Title = "Provincia no existe";
            this.spMessage = $"La provincia con el código \"{provinceCode}\" no existe.";
        }

        public ProvinceNotFoundException(string provinceCode) : this(defaultMessage, provinceCode) { }
    }
}
