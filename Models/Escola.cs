using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaId.Models
{
    [Table("Escola")]
    public class Escola    
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    } 
}