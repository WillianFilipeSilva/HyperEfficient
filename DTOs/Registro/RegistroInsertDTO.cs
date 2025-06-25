using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Registro;

public class RegistroInsertDto
{
    [Required]
    public DateTime DataInicial { get; set; }
    [Required]
    public DateTime DataFinal { get; set; }
    [Required] [Range(1, int.MaxValue)]
    public int EquipamentoId { get; set; }
}