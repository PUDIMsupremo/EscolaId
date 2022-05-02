using System.ComponentModel.DataAnnotations;

namespace EscolaId.ViewModels
{
    public class EditorTurmaViewModel
    {
        [Required]
        [MinLength(3), MaxLength(40)] 
        public string Ano { get; set; }
       
    }
}