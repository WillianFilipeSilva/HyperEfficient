using HyperEfficient.Dtos.Base;

namespace HyperEfficient.Dtos.Usuario
{
    public class UsuarioDto : BaseDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}