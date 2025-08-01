using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Equipamento
{
    public class EquipamentoInsertDto
    {
        [Required] [StringLength(100)] public string Nome { get; set; }
        [StringLength(300)] public string Descricao { get; set; }
        [Range(0, double.MaxValue)] public double PotenciaKwh { get; set; } = 0;
        [Range(1, int.MaxValue)] public int CategoriaId { get; set; }
        [Range(1, int.MaxValue)] public int SetorId { get; set; }
        [StringLength(25)] public string DeviceIdIntegration { get; set; }
    }
}