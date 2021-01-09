using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public class SqlUnaPintaRepo : IUnaPintaRepository
    {
        private readonly UnaPintaDBContext _context;

        public SqlUnaPintaRepo(UnaPintaDBContext context)
        {
            _context = context;
        }

        public void AddDonor(User donor)
        {
            _context.Users.Add(donor);
        }

        public void CreateRequest(Request request)
        {
            _context.Requests.Add(request);
        }

        public async Task<IEnumerable<BloodComponent>> GetAllBloodComponents()
        {
            var bloodComponents = await _context.BloodComponents.ToListAsync();
            return bloodComponents;
        }

        public async Task<IEnumerable<BloodType>> GetAllBloodTypes()
        {
            var bloodTypes = await _context.BloodTypes.ToListAsync();
            return bloodTypes;
        }

        public async Task<IEnumerable<User>> GetAllDonors()
        {
            var donors = await _context.Users.Where(x=>x.UserTypeId == UserTypeEnum.Donante)
                .ToListAsync();

            return donors;
        }

        public async Task<IEnumerable<User>> GetDonorsByBloodType(BloodTypeEnum bloodTypeId)
        {
            var donors = await _context.Users
                .Where(x=>x.UserTypeId == UserTypeEnum.Donante && x.BloodTypeId == bloodTypeId)
                .ToListAsync();

            return donors;
        }

        public async Task<bool> SaveChangesAsync()
        {
            bool saved = await _context.SaveChangesAsync() > -1;
            return saved;
        }
    }
}