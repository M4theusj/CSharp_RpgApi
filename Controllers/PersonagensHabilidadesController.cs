using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagemHabilidadesController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonagemHabilidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPh)
        {
            try
            {
                var personagem = await _context.TB_PERSONAGENS
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades)
                        .ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == novoPh.PersonagemId);

                if (personagem == null)
                    return NotFound("Personagem não encontrado.");

                var habilidade = await _context.TB_HABILIDADES
                    .FirstOrDefaultAsync(h => h.Id == novoPh.HabilidadeId);

                if (habilidade == null)
                    return NotFound("Habilidade não encontrada.");

                var ph = new PersonagemHabilidade
                {
                    Personagem = personagem,
                    Habilidade = habilidade
                };

                await _context.TB_PERSONAGENS_HABILIDADES.AddAsync(ph);
                await _context.SaveChangesAsync();

                return Ok(ph);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                var p = await _context.TB_PERSONAGENS
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades)
                        .ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (p == null)
                    return NotFound("Personagem não encontrado.");

                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHabilidadesPersonagem/{idPersonagem}")]
        public async Task<IActionResult> GetHabilidadesPersonagem(int idPersonagem)
        {
            try
            {
                var lista = await _context.TB_PERSONAGENS_HABILIDADES
                    .Include(ph => ph.Habilidade)
                    .Where(ph => ph.PersonagemId == idPersonagem)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHabilidades")]
        public async Task<IActionResult> GetHabilidades()
        {
            try
            {
                var lista = await _context.TB_HABILIDADES.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeletePersonagemHabilidade(PersonagemHabilidade ph)
        {
            try
            {
                var phRemover = await _context.TB_PERSONAGENS_HABILIDADES
                    .FirstOrDefaultAsync(x =>
                        x.PersonagemId == ph.PersonagemId &&
                        x.HabilidadeId == ph.HabilidadeId
                    );

                if (phRemover == null)
                    return NotFound("PersonagemHabilidade não encontrada.");

                _context.TB_PERSONAGENS_HABILIDADES.Remove(phRemover);
                await _context.SaveChangesAsync();

                return Ok("Removido com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
