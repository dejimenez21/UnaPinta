using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using UnaPinta.Data.Contracts;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Data
{
    public class SqlUnaPintaRepo : IUnaPintaRepository
    {
        private readonly UnaPintaDBContext _context;

        public SqlUnaPintaRepo(UnaPintaDBContext context)
        {
            _context = context;
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

        public async Task<BloodComponent> GetBloodComponentById(BloodComponentEnum id)
        {
            return await _context.BloodComponents.SingleOrDefaultAsync(x=>x.Id==id);
            
        }

        public async Task<BloodType> GetBloodTypeById(BloodTypeEnum id)
        {
            return await _context.BloodTypes.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Condition> GetConditionById(ConditionEnum id)
        {
            return await _context.Conditions.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Request> GetRequestById(long id)
        {
            return await _context.Requests.SingleOrDefaultAsync(x=>x.Id==id);
        }

        public Task<User> GetUserById(long id)
        {
            var user = _context.Users.SingleOrDefaultAsync(x=>x.Id==id);
            return user;
        }

        public async Task<bool> SaveChangesAsync()
        {
            bool saved = await _context.SaveChangesAsync() > -1;
            return saved;
        }

        public async Task<DateTime> GetAvailabilityDateByDonorId(long id)
        {
            var items = await _context.WaitLists.Where(x=>x.UserId==id).ToListAsync();
            if(!items.Any()) return DateTime.Now.Subtract(new TimeSpan(5,5,5));
            return items.Max(x=>x.AvailableAt);
        }

        public async Task<IEnumerable<Province>> SelectAllProvinces()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<Province> SelectProvinceByCode(string code)
        {
            return await _context.Provinces.FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}