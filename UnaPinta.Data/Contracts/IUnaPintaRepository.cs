using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using System.Collections.Generic;
using System;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Data.Contracts
{
    public interface IUnaPintaRepository
    {
        Task<bool> SaveChangesAsync();

        Task<User> GetUserById(long id);

        //BloodTypes table methods
        Task<IEnumerable<BloodType>> GetAllBloodTypes();
        Task<BloodType> GetBloodTypeById(BloodTypeEnum id);

        //BloodComponents table methods
        Task<IEnumerable<BloodComponent>> GetAllBloodComponents();
        Task<BloodComponent> GetBloodComponentById(BloodComponentEnum id);

        //WaitList table methods
        void AddWaitListItem(WaitList item);
        Task<DateTime> GetAvailabilityDateByDonorId(long id);

        //Condition table methods
        Task<Condition> GetConditionById(ConditionEnum id);

        //Province table methods
        Task<IEnumerable<Province>> SelectAllProvinces();
        Task<Province> SelectProvinceByCode(string code);
    }
}