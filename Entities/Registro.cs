using HyperEfficient.Entities.Base;

namespace HyperEfficient.Entities
{
    public class Registro : EntityBase
    {
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int EquipamentoId { get; set; }
        public double? TotalTempo { get; private set; }
    }
}