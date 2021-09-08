using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public interface IBloodTypesRepository
    {
        Task<List<int>> GetBloodTypes(int id);
        Task<List<Component>> GetBloodComponent();
    }
}
