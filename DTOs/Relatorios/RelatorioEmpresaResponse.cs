namespace HyperEfficient.Dtos.Relatorios
{
    public class RelatorioEmpresaResponse
    {
        public TotalizadoresDto Totalizadores { get; set; }
        public List<DadosMensaisDto> DadosMensais { get; set; }
        public List<SetorResumoDto> Setores { get; set; }
        public List<CategoriaResumoDto> Categorias { get; set; }
    }
}