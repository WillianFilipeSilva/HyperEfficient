using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Equipamento;

public class EquipamentoInsertDto
{
    [Required] [StringLength(100)]
    public string Nome { get; set; }

    [Required] [StringLength(300)]
    public string Descricao { get; set; }

    [Range(1, double.MaxValue)]
    public decimal Gastokwh { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoriaId { get; set; }

    [Range(1, int.MaxValue)]
    public int SetorId { get; set; }
}