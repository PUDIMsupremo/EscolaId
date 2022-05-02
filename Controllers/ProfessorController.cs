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
    public class ProfessorControllers : ControllerBase
    {
        [HttpGet]
        [Route("/v1/professores")]
        public async Task<IActionResult> Get
        (
            [FromServices] AppDbContext context
        )
        {
        try
        {
            var professores = await context.Professores.ToListAsync();
            return Ok(new ResultViewModel<List<Professor>>(professores));
        }        
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X00 - Falha interna no servidor"));
        }
        }

        [HttpGet]
        [Route("/v1/professorid/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context 
        )
        {
        try
        {
            var professores = await context.Professores.FirstOrDefaultAsync(x => x.Id == id);
            if(professores == null)
                return NotFound(new ResultViewModel<Professor>("88X01 - Id não localizado"));
            return Ok(new ResultViewModel<Professor>(professores));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X01 - Falha interna no servidor"));
        }
        }
        [HttpPost]
        [Route("/v1/professorpost")]
        public async Task<IActionResult> PostAsync
        (
            [FromBody] EditorProfessorViewModel model,
            [FromServices] AppDbContext context 
        )
        {
            if (!ModelState.IsValid)
               return BadRequest(new ResultViewModel<Professor>(ModelState.GetErrors()));
        try
        {
            var professor = new Professor
            {
                Id = 0,
                Nome = model.Nome,
                Documento = model.Documento,
                Contato = model.Contato,
                Nascimento = DateTime.Now,
                Efetivo = true
            };
            await context.Professores.AddAsync(professor);
            await context.SaveChangesAsync();

            return Created($"/{professor.Id}", model);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X02 - Falha ao cadastrar professor(a)"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X02 - Falha interna no servidor"));
        }
        }
        [HttpPut]
        [Route("/v1/professorput/{id:int}")]
        public async Task<IActionResult> Put
        (
            [FromRoute] int id,
            [FromBody] EditorProfessorViewModel model,
            [FromServices] AppDbContext context 
        )
        {
        try
        {
            var professores = await context.Professores.FirstOrDefaultAsync(x => x.Id == id);
            if(professores == null)
                return NotFound(new ResultViewModel<Professor>("88X03 - Professor não cadastrado"));

            professores.Nome = model.Nome;
            professores.Documento = model.Documento;
            professores.Contato = model.Contato;
            professores.Efetivo = model.Efetivo;

            context.Professores.Update(professores);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<Professor>(professores));
        }
        catch(DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X03 - Não foi possível atualizar cadastro"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X03 - Falha interna no servidor"));
        }
        }

        [HttpDelete]
        [Route("/v1/professordelete/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
        try
        {
            var professores = await context.Professores.FirstOrDefaultAsync(x => x.Id == id);
            if(professores == null)
                return NotFound(new ResultViewModel<Professor>("88X04 - Professor não cadastrado"));
            context.Professores.Remove(professores);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Professor>(professores));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X04 - Não foi possível deletar"));
        }
        catch 
        {
            return StatusCode(500, new ResultViewModel<Professor>("88X04 - Falha interna no servidor"));
        }
        }
    }
}