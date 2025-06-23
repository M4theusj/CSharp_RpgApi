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
    public class PersonagensController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonagensController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                var p = await _context.TB_PERSONAGENS
                    .Include(x => x.Arma)
                    .Include(x => x.Usuario)
                    .Include(x => x.PersonagemHabilidades)
                        .ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (p == null)
                    return NotFound();

                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _context.TB_PERSONAGENS.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RestaurarPontosVida")]
        public async Task<IActionResult> RestaurarPontosVidaAsync(Personagem p)
        {
            try
            {
                var pEncontrado = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(x => x.Id == p.Id);

                if (pEncontrado == null)
                    return NotFound("Personagem não encontrado.");

                pEncontrado.PontosVida = 100;

                bool atualizou = await TryUpdateModelAsync(
                    pEncontrado,
                    prefix: "p",
                    x => x.PontosVida
                );

                int linhasAfetadas = atualizou
                    ? await _context.SaveChangesAsync()
                    : 0;

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                await _context.TB_PERSONAGENS.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();
                return Ok(novoPersonagem.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem personagem)
        {
            try
            {
                _context.TB_PERSONAGENS.Update(personagem);
                int linhas = await _context.SaveChangesAsync();
                return Ok(linhas);
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
                var pRemover = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (pRemover == null)
                    return NotFound("Personagem não encontrado.");

                _context.TB_PERSONAGENS.Remove(pRemover);
                int linhas = await _context.SaveChangesAsync();
                return Ok(linhas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeleteAsync(PersonagemHabilidade ph)
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
                int linhas = await _context.SaveChangesAsync();
                return Ok(linhas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
