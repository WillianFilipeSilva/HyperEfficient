namespace HyperEfficient.Dtos.Equipamento
{
    public class EquipamentoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public double PotenciaKwh { get; set; }
        public Entities.Categoria Categoria { get; set; }
        public Entities.Setor Setor { get; set; }
        public bool Ativo { get; set; }
        public string? DeviceIdIntegration { get; set; }
    }
}