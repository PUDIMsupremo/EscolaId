using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaId.Models
{
    [Table("Turma")]
    public class Turma
    {
        public int Id { get; set; }
        public string Ano { get; set; }
    }
}