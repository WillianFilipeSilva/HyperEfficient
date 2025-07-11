namespace HyperEfficient.Dtos.Relatorios
{
    public class RelatorioEmpresaResponse
    {
        public TotalizadoresDTO Totalizadores { get; set; }
        public List<DadosMensaisDTO> DadosMensais { get; set; }
        public List<SetorResumoDTO> Setores { get; set; }
        public List<CategoriaResumoDTO> Categorias { get; set; }
    }
}