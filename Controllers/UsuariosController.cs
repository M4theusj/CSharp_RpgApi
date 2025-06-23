using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Utils;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioExistente(string username)
            => await _context.TB_USUARIOS
                             .AnyAsync(x => x.Username.ToLower() == username.ToLower());

        // <-- Aqui fica só UM POST Registrar
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario user)
        {
            try
            {
                if (await UsuarioExistente(user.Username))
                    return BadRequest("Nome de usuário já existe");

                // Gera hash+salt e limpa a senha em texto
                Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                user.PasswordString = string.Empty;

                await _context.TB_USUARIOS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais){
            try{
                Usuario? usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

                if(usuario == null){
                    throw new System.Exception("Usuário não encontrado.");
                }
                else if(!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt)){
                    throw new System.Exception("Senha incorreta.");
                }
                else{

                    var date = DateTime.Now;
                    Console.WriteLine(date);
                    usuario.DataAcesso = date; 
                    await _context.SaveChangesAsync();

                    return Ok(usuario);
                }
            }
            catch(System.Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(Usuario credenciais){
            
            try
            {
            Usuario? usuario = await _context.TB_USUARIOS
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(credenciais.Username.ToLower()));

            if(usuario == null){
                throw new System.Exception("Usuário não encontrado.");
            }
                Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                usuario.PasswordString = string.Empty;
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;
                await _context.SaveChangesAsync();

                return Ok("Senha alterada");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(){

            try
            {
                List<Usuario> lista = await _context.TB_USUARIOS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id){

            try
            {
                Usuario? usuario = await _context.TB_USUARIOS
                        .FirstOrDefaultAsync(u => u.Id == id);

                if(usuario == null){
                        throw new System.Exception("Usuário não encontrado.");
                    }
                
                return Ok(usuario);
            }

            catch(System.Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }
    }
}