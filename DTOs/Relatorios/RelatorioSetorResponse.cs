namespace HyperEfficient.Dtos.Relatorios;

public class RelatorioSetorResponse
{
    public int SetorId { get; set; }
    public string NomeSetor { get; set; }
    public int QuantidadeEquipamentos { get; set; }
    public decimal TempoUsoTotal { get; set; }
    public decimal GastoEnergeticoTotal { get; set; }
}