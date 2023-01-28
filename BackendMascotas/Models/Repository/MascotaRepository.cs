using Microsoft.EntityFrameworkCore;

namespace BackendMascotas.Models.Repository
{
    public class MascotaRepository: IMascotasRepository
    {
        private readonly AplicationDbContext _context;

        public MascotaRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mascotas>> GetAll()
        {
            return await _context.Mascotas.ToListAsync();
        }
    }
}
