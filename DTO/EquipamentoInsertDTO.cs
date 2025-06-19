using System;

namespace HyperEfficient.DTO
{
    public class EquipamentoInsertDTO
    {
        public decimal GastokWh { get; set; }
        public int CategoriaId { get; set; }
        public int SetorId { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
    }
}
