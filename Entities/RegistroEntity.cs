using HyperEfficient.Entities.Base;

namespace HyperEfficient.Entities
{
    public class RegistroEntity : EntityBase
    {
        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public int EquipamentoId { get; set; }
    }
}