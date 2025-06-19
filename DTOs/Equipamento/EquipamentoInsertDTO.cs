using System;

namespace HyperEfficient.DTOs.Equipamento
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
