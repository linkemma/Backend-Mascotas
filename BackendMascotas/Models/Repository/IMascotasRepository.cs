namespace BackendMascotas.Models.Repository
{
    public interface IMascotasRepository
    {
        Task<List<Mascotas>> GetAll();
    }
}
