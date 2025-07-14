using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Usuario
{
    public class UsuarioInsertDto
    {
        [Required] [StringLength(100)] public string Nome { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [MinLength(6)] public string Senha { get; set; }
    }
}