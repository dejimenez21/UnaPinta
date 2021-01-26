using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Api.Services
{
    public class SqlUnaPintaRepo : IUnaPintaRepository
    {
        private readonly UnaPintaDBContext _context;

        public SqlUnaPintaRepo(UnaPintaDBContext context)
        {
            _context = context;
        }

        public void AddConfirmationCode(ConfirmationCode code)
        {
            _context.ConfirmationCodes.Add(code);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void AddWaitListItem(WaitList item)
        {
            _context.WaitLists.Add(item);
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
            var donors = await _context.Users.Where(x=>x.UserTypeId == UserTypeEnum.Donante.ToString())
                .ToListAsync();

            return donors;
        }

        public async Task<BloodComponent> GetBloodComponentById(BloodComponentEnum id)
        {
            return await _context.BloodComponents.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<BloodType> GetBloodTypeById(BloodTypeEnum id)
        {
            return await _context.BloodTypes.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<ConfirmationCode> GetCodeByUser(string code, string id)
        {
            return await _context.ConfirmationCodes.SingleOrDefaultAsync(x=>x.Code==code&&x.UserId==id);
        }

        public async Task<Condition> GetConditionById(ConditionEnum id)
        {
            return await _context.Conditions.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IEnumerable<User>> GetDonorsByBloodType(List<BloodTypeEnum> bloodTypes)
        {
            foreach (var item in bloodTypes)
            {
                System.Console.WriteLine(item);
            }
            var donors = await _context.Users
                .Where(x=>x.UserTypeId == UserTypeEnum.Donante.ToString() && bloodTypes.Contains(x.BloodTypeId))
                .ToListAsync();

            return donors;
        }

        public async Task<Request> GetRequestById(int id)
        {
            return await _context.Requests.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> GetUserByEmail(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public Task<User> GetUserById(string id)
        {
            var user = _context.Users.SingleOrDefaultAsync(x=>x.Id==id);
            return user;
        }

        public async Task<bool> SaveChangesAsync()
        {
            bool saved = await _context.SaveChangesAsync() > -1;
            return saved;
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DateTime> GetAvailabilityDateByDonorId(string id)
        {
            var items = await _context.WaitLists.Where(x=>x.UserId==id).ToListAsync();
            if(!items.Any()) return DateTime.Now.Subtract(new TimeSpan(5,5,5));
            return items.Max(x=>x.AvailableAt);
        }
    }
}