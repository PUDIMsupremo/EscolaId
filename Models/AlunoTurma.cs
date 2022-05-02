using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Models
{
    [Keyless]
    [Table("AlunoTurma")]
    public class AlunoTurma
    {
        public int AlunoId { get; set; }
        public string AlunoDocumento { get; set; }
        public int TurmaId { get; set; }
        public bool Ativa { get; set; }
    }
}