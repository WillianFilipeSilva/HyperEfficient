using HyperEfficient.Entities;

namespace HyperEfficient.DTOs.Usuario
{
    public class UsuarioLoginTokenDTO
    {
        public string Token { get; set; }
        public UsuarioEntity Usuario { get; set; }
    }
}