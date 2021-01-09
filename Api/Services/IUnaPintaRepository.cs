using System.Threading.Tasks;
using Api.Entities;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IUnaPintaRepository
    {
        Task<bool> SaveChangesAsync();

        //Users table methods
        void AddUser(User user);
        void UpdateUser(User user);
        Task<IEnumerable<User>> GetAllDonors();
        Task<IEnumerable<User>> GetDonorsByBloodType(BloodTypeEnum bloodTypeId);
        Task<User> GetUserById(int id);

        //Request table methods
        void CreateRequest(Request request);

        //BloodTypes table methods
        Task<IEnumerable<BloodType>> GetAllBloodTypes();

        //BloodComponents table methods
        Task<IEnumerable<BloodComponent>> GetAllBloodComponents();

        //ConfirmationCodes table methods
        void AddConfirmationCode(ConfirmationCode code);
        Task<ConfirmationCode> GetCodeByUser(string code, int id);
    }
}