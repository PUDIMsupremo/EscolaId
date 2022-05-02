using System.ComponentModel.DataAnnotations;

namespace EscolaId.ViewModels
{
    public class EditorProfessorViewModel
    {
        [Required(ErrorMessage = "O nome deve ter entre 3 e 40 caractéres")]
        [MaxLength(40), MinLength (3)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O documento deve conter apenas 11 digitos sem simbolos especiais")]
        [MinLength (11), MaxLength(11)]
        [Key]
        public string Documento { get; set; }
        
        [Required(ErrorMessage = "Deve conter DDD sem simbolos especiais e o número, caso não complete 11 digitos adicionar um 9 EX: 629XXXXXXXX")]
        [MinLength (11), MaxLength(11)]
        public string Contato { get; set; }
        [Required]
        public bool Efetivo { get; set; }
    }
}