using HyperEfficient.Entities.Base;

namespace HyperEfficient.Entities
{
    public class SetorEntity : EntityBase
    {
        public decimal GastoGeral { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }
    }
}