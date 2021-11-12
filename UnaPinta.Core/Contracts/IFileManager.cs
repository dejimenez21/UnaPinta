using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Contracts
{
    public interface IFileManager
    {
        Task<File> RetrieveFileById(long id);
    }
}
