using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Models
{
    [Keyless]
    [Table("TurmaEscola")]
    public class TurmaEscola
    {
        public int TurmaId { get; set; }
        public int EscolaId { get; set; }
        public bool Ativo { get; set; }
    }
}