using HyperEfficient.Entities.Base;

namespace HyperEfficient.Entities
{
    public class EquipamentoEntity : EntityBase
    {
        public decimal Gastokwh { get; set; }

        public int SetorId { get; set; }

        public int CategoriaId { get; set; }

        public string Descricao { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}