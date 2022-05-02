using EscolaId.Data;
using EscolaId.Extensions;
using EscolaId.Models;
using EscolaId.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaId.Controllers
{
     [ApiController]
    public class EscolaControllers : ControllerBase
    {
        [HttpGet]
        [Route("/v1/escolas")]
        public async Task<IActionResult> Get
        (
            [FromServices] AppDbContext context
        )
        {
            try{
            var escolas = await context.Escolas.ToListAsync();
            return Ok(new ResultViewModel<List<Escola>>(escolas));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Escola>("87X00 - Falha interna no servidor"));
            }
        }
        [HttpGet]
        [Route("/v1/escolaid/{id:int}")]
        public async Task<IActionResult> GetByIdAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
            try{
            var escolas = await context.Escolas.FirstOrDefaultAsync(x => x.Id == id);
            if (escolas == null)
                return NotFound(new ResultViewModel<Escola>("87X01 - Id não localizado"));
            return Ok(new ResultViewModel<Escola>(escolas));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Escola>("87X01 - Falha interna no servidor"));
            }
        }   

        [HttpPost]
        [Route("/v1/escolapost")]
        public async Task<IActionResult> PostAsync
        (
            [FromBody] EditorEscolaViewModel model,
            [FromServices] AppDbContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Escola>(ModelState.GetErrors()));
        try{
            var escola = new Escola
            {
                Id = 0,
                Nome = model.Nome
            };
            await context.Escolas.AddAsync(escola);
            await context.SaveChangesAsync();

            return Created($"/{escola.Id}", model);
            }
           catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Aluno>("87X02 - Não foi possível adicionar o aluno"));
            }
            catch  
            {
                return StatusCode(500, new ResultViewModel<Aluno>("87X02 - Falha interna no servidor"));
            }
        }

        [HttpPut]
        [Route("/v1/escolaput/{id:int}")]
        public async Task<IActionResult> Put
        (
            [FromRoute] int id,
            [FromBody] EditorEscolaViewModel model,
            [FromServices] AppDbContext context 
        )
        {
            try{
            var escolas = await context.Escolas.FirstOrDefaultAsync(x => x.Id == id);
            if(escolas == null)
                return NotFound(new ResultViewModel<Escola>("87X03 - ID não encontrado"));

            escolas.Nome = model.Nome;

            context.Escolas.Update(escolas);
            await context.SaveChangesAsync();
            
            return Ok(escolas);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Escola>("87X03 - Não foi possível atualizar"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Escola>("87X03 - Falha interna no servidor"));
            }
        }

        [HttpDelete]
        [Route("/v1/escoladelete/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        (
            [FromRoute] int id,
            [FromServices] AppDbContext context
        )
        {
            try{
            var escolas = await context.Escolas.FirstOrDefaultAsync(x => x.Id == id);
            if(escolas == null)
                return NotFound(new ResultViewModel<Escola>("87X04 - Escola não cadastrada"));
            context.Escolas.Remove(escolas);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Escola>(escolas));
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Escola>("87X04 - Não foi possível apagar a escola"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Aluno>("87X04 - Falha interna no servidor"));
            }
        }
    }
}