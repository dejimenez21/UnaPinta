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

        //Users table methods
        void AddUser(User user);
        void UpdateUser(User user);
        Task<IEnumerable<User>> GetAllDonors();
        Task<IEnumerable<User>> GetDonorsByBloodType(List<BloodTypeEnum> bloodTypes);
        Task<User> GetUserById(int id);
        Task<bool> GetUserByEmail(string email);

        //Request table methods
        void CreateRequest(Request request);
        Task<Request> GetRequestById(int id);

        //BloodTypes table methods
        Task<IEnumerable<BloodType>> GetAllBloodTypes();
        Task<BloodType> GetBloodTypeById(BloodTypeEnum id);

        //BloodComponents table methods
        Task<IEnumerable<BloodComponent>> GetAllBloodComponents();
        Task<BloodComponent> GetBloodComponentById(BloodComponentEnum id);

        //ConfirmationCodes table methods
        void AddConfirmationCode(ConfirmationCode code);
        Task<ConfirmationCode> GetCodeByUser(string code, int id);

        //WaitList table methods
        void AddWaitListItem(WaitList item);
        Task<DateTime> GetAvailabilityDateByDonorId(int id);

        //Condition table methods
        Task<Condition> GetConditionById(ConditionEnum id);
    }
}