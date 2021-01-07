using System.Threading.Tasks;
using Api.Entities;

namespace Api.Services
{
    public interface IUnaPintaRepository
    {
        Task<bool> SaveChangesAsync();

        void AgregarDonante(User donante);
    }
}