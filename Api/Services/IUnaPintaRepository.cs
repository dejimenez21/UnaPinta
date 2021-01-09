using System.Threading.Tasks;
using Api.Entities;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IUnaPintaRepository
    {
        Task<bool> SaveChangesAsync();

        //Donors table methods
        void AddDonor(User donor);
        Task<IEnumerable<User>> GetAllDonors();
        Task<IEnumerable<User>> GetDonorsByBloodType(BloodTypeEnum bloodTypeId);

        //Request table methods
        void CreateRequest(Request request);

        //BloodTypes table methods
        Task<IEnumerable<BloodType>> GetAllBloodTypes();

        //BloodComponents table methods
        Task<IEnumerable<BloodComponent>> GetAllBloodComponents();
    }
}