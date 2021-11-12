using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnaPinta.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly UnaPintaDBContext _dBContext;

        public FileRepository(UnaPintaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void Delete(File file)
        {
            _dBContext.Files.Remove(file);
        }

        public async Task<File> SelectById(long id)
        {
            return await _dBContext.Files.SingleOrDefaultAsync(f => f.Id == id);
        }
    }
}
