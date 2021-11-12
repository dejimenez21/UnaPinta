using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Data.Contracts
{
    public interface IFileRepository
    {
        Task<File> SelectById(long id);

        void Delete(File file);
    }
}
