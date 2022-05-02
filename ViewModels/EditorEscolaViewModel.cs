using System.ComponentModel.DataAnnotations;

namespace EscolaId.ViewModels
{
    public class EditorEscolaViewModel
    {
        [Required]
        [MaxLength(80), MinLength(10)]
        public string Nome { get; set; }
    }
}