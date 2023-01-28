using Microsoft.EntityFrameworkCore;

namespace BackendMascotas.Models
{
    public class AplicationDbContext: DbContext
    {
         public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {

        }
        public DbSet<Mascotas> Mascotas { get; set; }
    }
}
