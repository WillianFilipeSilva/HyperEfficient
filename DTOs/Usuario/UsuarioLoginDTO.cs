using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Usuario;

public class UsuarioLoginDto
{
    [Required] [EmailAddress]
    public string Email { get; set; }

    [Required] [MinLength(6)]
    public string Senha { get; set; }

    public bool LembrarDeMim { get; set; } = false;
}