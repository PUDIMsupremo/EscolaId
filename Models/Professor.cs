using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaId.Models
{
    [Table("Professor")]
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Contato { get; set; }
        public DateTime Nascimento { get; set; }
        public bool Efetivo { get; set; }
    }
}