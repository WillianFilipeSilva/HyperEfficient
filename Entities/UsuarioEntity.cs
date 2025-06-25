using HyperEfficient.Entities.Base;

namespace HyperEfficient.Entities;

public class UsuarioEntity : EntityBase
{
    public string Nome { get; set; }

    public DateTime CriadoEm { get; set; }

    public string Senha { get; set; }

    public string Email { get; set; }

    public bool Ativo { get; set; }
}