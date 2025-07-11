namespace HyperEfficient.Dtos.Relatorios
{
    public class CategoriaResumoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal GastoTotal { get; set; }
        public decimal TempoUsoTotal { get; set; }
        public int QuantidadeEquipamentos { get; set; }
    }
} 