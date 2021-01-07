using System.Threading.Tasks;
using Api.Entities;

namespace Api.Services
{
    public class SqlUnaPintaRepo : IUnaPintaRepository
    {
        private readonly UnaPintaDBContext _context;

        public SqlUnaPintaRepo(UnaPintaDBContext context)
        {
            _context = context;
        }

        public void AgregarDonante(User donante)
        {
            _context.Users.Add(donante);
        }

        public async Task<bool> SaveChangesAsync()
        {
            bool saved = await _context.SaveChangesAsync() > -1;
            return saved;
        }
    }
}