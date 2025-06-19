using System;

namespace HyperEfficient.DTOs.Usuario
{
    public class UsuarioInsertDTO
    {
        public string Nome { get; set; }

        public DateTime CriadoEm { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public bool Ativo { get; set; }
    }
}
