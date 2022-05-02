using EscolaId.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<AlunoTurma> AlunosTurmas { get; set; }
        public DbSet<TurmaEscola> TurmasEscolas { get; set; }
        public DbSet<ProfessorEscola> ProfessoresEscolas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=BancoSimples;User ID=sa;Password=1q2w3e4r@#$");

    }
}