using System.Threading.Tasks;
using EscolaId.Data;
using EscolaId.Extensions;
using EscolaId.Models;
using EscolaId.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Controllers
{
    [ApiController]
    public class AlunoControllers : ControllerBase
    {
        [HttpGet]
        [Route("/v1/alunos")]
        public async Task<IActionResult> GetAsync
        (
            [FromServices] AppDbContext context
        )
        {
            try{
            var alunos = await context.Alunos.ToListAsync();
            return Ok(new ResultViewModel<List<Aluno>>(alunos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Aluno>("50X09 - Falha interna no servidor"));
            }
        }

        [HttpGet]
        [Route("/v1/alunosid/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context 
        )
        {
            try
            {
            var alunos = await context.Alunos.FirstOrDefaultAsync(x => x.Id == id);
            if(alunos == null)
                return NotFound(new ResultViewModel<Aluno>("49X00 - Id não localizado"));
            return Ok(new ResultViewModel<Aluno>(alunos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Aluno>("50X10 - Falha interna no servidor"));
            }
        }
        [HttpPost]
        [Route("/v1/alunospost")]
        public async Task<IActionResult> PostAsync
        (
            [FromBody] EditorAlunoViewModel model,
            [FromServices] AppDbContext context 
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Aluno>(ModelState.GetErrors()));
        try{   
            var aluno = new Aluno
            {
                Id = 0,
                Nome = model.Nome,
                Documento = model.Documento,
                Contato = model.Contato,
                Nascimento = DateTime.Now,
                Matricula = DateTime.Now
            };
            await context.Alunos.AddAsync(aluno);
            await context.SaveChangesAsync();

            return Created($"/{aluno.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Aluno>("49X01 - Não foi possível adicionar o aluno"));
            }
            catch  
            {
                return StatusCode(500, new ResultViewModel<Aluno>("50X11 - Falha interna no servidor"));
            }
        }
        [HttpPut]
        [Route("/v1/alunosput/{id:int}")]
        public async Task<IActionResult> Put
        (
            [FromRoute] int id,
            [FromBody] EditorAlunoViewModel model,
            [FromServices] AppDbContext context 
        )
        {
        try
        {
            var alunos = await context.Alunos.FirstOrDefaultAsync(x => x.Id == id);
            if(alunos == null)
                return NotFound(new ResultViewModel<Aluno>("50X14 - Aluno não encontrado"));

            alunos.Nome = model.Nome;
            alunos.Documento = model.Documento;
            alunos.Contato = model.Contato;

            context.Alunos.Update(alunos);
            await context.SaveChangesAsync();
            
            return Ok(alunos);
            }
            catch (DbUpdateException ex)
            {   
                return StatusCode(500, new ResultViewModel<Aluno>("50X13 - Não foi possível atualizar"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Aluno>("50X12 - Falha interna no servidor "));
            }
        }

        [HttpDelete]
        [Route("/v1/alunosdelete/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
             try
            {
                var aluno = await context
                    .Alunos
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (aluno == null)
                    return NotFound(new ResultViewModel<Aluno>("Conteúdo não encontrado"));

                context.Alunos.Remove(aluno);
                await context.SaveChangesAsync();

                return Ok("Apagado!");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Aluno>("05XE7 - Não foi possível excluir o aluno"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Aluno>("05X12 - Falha interna no servidor"));
            }
        }
    }
}