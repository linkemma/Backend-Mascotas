using AutoMapper;
using BackendMascotas.Models.DTOs;

namespace BackendMascotas.Models.Profiles
{
    public class MascotaProfile: Profile
    {
        public MascotaProfile()
        {
            CreateMap<Mascotas, MascotaDTo>();
            CreateMap<MascotaDTo, Mascotas>();
        }
    }
}
