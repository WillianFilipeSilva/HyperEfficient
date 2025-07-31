using HyperEfficient.Entities.Base;

namespace HyperEfficient.Entities
{
    public class Equipamento : EntityBase
    {
        public double PotenciaKwh { get; set; } = 0;
        public int SetorId { get; set; }
        public int CategoriaId { get; set; }
        public string? Descricao { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; } = false;
        public string? DeviceIdIntegration { get; set; }
    }
}