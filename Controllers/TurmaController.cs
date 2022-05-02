using EscolaId.Data;
using EscolaId.Extensions;
using EscolaId.Models;
using EscolaId.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Controllers
{
     [ApiController]
    public class TurmaControllers : ControllerBase
    {
        [HttpGet]
        [Route("/v1/turmas")]
        public async Task<IActionResult> Get
        (
            [FromServices] AppDbContext context
        )
        {
        try
        {
            var turmas = await context.Turmas.ToListAsync();
            return Ok(new ResultViewModel<List<Turma>>(turmas));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X00 - Falha interna no servidor"));
        }
        }
        [HttpGet]
        [Route("/v1/turmaid/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
        try
        {
            var turmas = await context.Turmas.FirstOrDefaultAsync(x => x.Id == id);
            if (turmas == null)
                return NotFound(new ResultViewModel<Turma>("89X01 - Id não encontrado"));
            return Ok(new ResultViewModel<Turma>(turmas));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X01 - Falha interna no servidor"));
        }
        }   

        [HttpPost]
        [Route("/v1/turmapost")]
        public async Task<IActionResult> PostAsync
        (
            [FromBody] EditorTurmaViewModel model,
            [FromServices] AppDbContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Professor>(ModelState.GetErrors()));
        try
        {
            var turma = new Turma 
            {
                Id = 0,
                Ano = model.Ano
            };
            await context.Turmas.AddAsync(turma);
            await context.SaveChangesAsync();

            return Created($"/{turma.Id}", model);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X02 - Falha ao atualizar"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X02 - Falha interna no servidor"));
        }
        }

        [HttpPut]
        [Route("/v1/turmaput/{id:int}")]
        public async Task<IActionResult> Put
        (
            [FromRoute] int id,
            [FromBody] EditorTurmaViewModel model,
            [FromServices] AppDbContext context 
        )
        {
        try
        {
             var turmas = await context.Turmas.FirstOrDefaultAsync(x => x.Id == id);
            if(turmas == null)
                return NotFound(new ResultViewModel<Turma>("89X03 - Turma não encontrada"));

            turmas.Ano = model.Ano;

            context.Turmas.Update(turmas);
            await context.SaveChangesAsync();
            
            return Ok(new ResultViewModel<Turma>(turmas));
        }
        catch(DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X03 - Não foi possível atualizar"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X03 - Falha interna no servidor"));
        }
        }
        [HttpDelete]
        [Route("/v1/turmadelete/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
        try
        {
            var turmas = await context.Turmas.FirstOrDefaultAsync(x => x.Id == id);
            if(turmas == null)
                return NotFound(new ResultViewModel<Turma>("89X04 - Turma não localizada"));
            context.Turmas.Remove(turmas);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Turma>(turmas));
        }
        catch(DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X04 - Falha ao deletar"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Turma>("89X04 - Falha interna no servidor"));
        }
        }
    }
}