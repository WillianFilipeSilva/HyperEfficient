using System;

namespace HyperEfficient.Entity
{
    public class RegistroEntity
    {
        public int Id { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public int EquipamentoId { get; set; }
    }
}
