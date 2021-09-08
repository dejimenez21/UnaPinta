using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public interface IProvincesRepository
    {
        Task<List<Provinces>> GetProvinces();
    }
}
