using System;

namespace HyperEfficient.Entity
{
    public class EquipamentoEntity
    {
        public int Id { get; set; }

        public decimal GastokWh { get; set; }

        public int SetorId { get; set; }

        public int CategoriaId { get; set; }

        public string Descricao { get; set; }

        public string Nome { get; set; }
        
        public bool Ativo { get; set; }
    }
}
