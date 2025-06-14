using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using RpgApi.Models.Enuns;

namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? Foto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DataAcesso { get; set; }

        [NotMapped]
        public string PasswordString {get; set;} = string.Empty;
        public List<Personagem> Personagens {get; set;} = new List<Personagem>();
        public string? Perfil {get; set;}
        public string? Email {get; set;} = string.Empty;
        public ClasseEnum Classe {get; set;}
        public byte[]? FotoPersonagem {get; set;}
        public int? UsuarioId {get; set;}
        public Usuario? UsuarioOg {get; set;}


    }
}