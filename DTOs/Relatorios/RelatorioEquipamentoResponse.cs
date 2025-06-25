namespace HyperEfficient.Dtos.Relatorios;

public class RelatorioEquipamentoResponse
{
    public int EquipamentoId { get; set; }
    public string NomeEquipamento { get; set; }
    public decimal TempoUsoTotal { get; set; }
    public decimal GastoEnergeticoTotal { get; set; }
    public bool Ativo { get; set; }
}