using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IUnaPintaRepository _repo;

        public ProvinceService(IUnaPintaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Province>> GetAllProvinces()
        {
            return await _repo.SelectAllProvinces();
        }
    }
}
