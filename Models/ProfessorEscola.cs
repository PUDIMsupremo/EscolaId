using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Models
{
    [Keyless]
    [Table("ProfessorEscola")]
    public class ProfessorEscola
    {
        public int ProfessorId { get; set; }
        public string ProfessorDocumento { get; set; }
        public int EscolaId { get; set; }
        public bool Ativo { get; set; }
    }
}