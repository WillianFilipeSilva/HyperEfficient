using System;

namespace HyperEfficient.DTOs.Registro
{
    public class RegistroInsertDTO
    {
        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public int EquipamentoId { get; set; }
    }
}
