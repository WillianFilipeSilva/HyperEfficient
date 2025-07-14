using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Setor
{
    public class SetorInsertDto
    {
        [Required] [StringLength(100)] public string Nome { get; set; }
        [StringLength(300)] public string Descricao { get; set; }
    }
}