namespace HyperEfficient.Dtos.Relatorios
{
    public class RelatorioEmpresaResponse
    {
        public int QuantidadeSetores { get; set; }
        public int QuantidadeEquipamentos { get; set; }
        public decimal TempoUsoTotal { get; set; }
        public decimal GastoEnergeticoTotal { get; set; }
    }
}