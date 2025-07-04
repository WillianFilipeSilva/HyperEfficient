using System.ComponentModel.DataAnnotations;

namespace HyperEfficient.Dtos.Categoria
{
    public class CategoriaInsertDto
    {
        [Required] [StringLength(100)] public string Nome { get; set; }
    }
}