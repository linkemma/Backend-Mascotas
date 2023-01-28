using AutoMapper;
using BackendMascotas.Models;
using BackendMascotas.Models.DTOs;
using BackendMascotas.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace BackendMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IMascotasRepository _mascotasRepository;
        public MascotasController( IMapper mapper, IMascotasRepository mascotasRepository)
        {
            _autoMapper = mapper;
            _mascotasRepository = mascotasRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ListaMascotas = await _mascotasRepository.GetAll();

                var ListaMascotasDTo = _autoMapper.Map<IEnumerable<MascotaDTo>>(ListaMascotas);

                return Ok(ListaMascotasDTo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota  = await _context.Mascotas.FindAsync(id);
                if(mascota == null)
                {
                    return NotFound();
                }

                var mascotaDTo = _autoMapper.Map<MascotaDTo>(mascota);

                return Ok(mascotaDTo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _context.Mascotas.FindAsync(id);
                if (mascota == null)
                {
                    return NotFound();
                }
                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> insert(MascotaDTo mascotaDTo)
        {
            try
            {
                var mascota = _autoMapper.Map<Mascotas>(mascotaDTo);
                mascota.FechaCreacion = DateTime.Now;
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                var mascotaD = _autoMapper.Map<MascotaDTo>(mascota);
                return CreatedAtAction("Get", new {id = mascotaD.Id}, mascotaD);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, MascotaDTo mascotaDTo)
        {
            try
            {
                var mascota = _autoMapper.Map<Mascotas>(mascotaDTo);

                if(id != mascota.Id)
                {
                    return BadRequest();
                }

                var mascotaItem = await _context.Mascotas.FindAsync(id);

                if( mascotaItem == null)
                {
                    return NotFound();
                }

                mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.Edad = mascota.Edad;
                mascotaItem.Color = mascota.Color;
                mascotaItem.Peso = mascota.Peso;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
