using Api.WeChip.Data;
using Api.WeChip.Models;
using Api.WeChip.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WeChip.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var status = await context
                .Status
                .AsNoTracking()
                .ToListAsync();

            return Ok(status);
        }

        [HttpGet]
        [Route("status/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var status = await context
                .Status
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return status == null
                ? NotFound()
                : Ok(status);
        }

        [HttpPost("status")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateStatusViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var status = new Status
            {
                Codigo = model.Codigo,
                Descricao = model.Descricao,
                ContabilizaVenda = model.ContabilizaVenda,
                FinalizaCliente = model.FinalizaCliente
            };

            var buscaStatus = context
                .Status
                .FirstOrDefaultAsync(c => c.Codigo == status.Codigo);

            try
            {
                if (buscaStatus.Result != null)
                    throw new Exception("Status informado já existe no banco de dados");

                await context.Status.AddAsync(status);
                await context.SaveChangesAsync();
                return Created($"v1/status/{status.Id}", status);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] UpdateStatusViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var status = await context
                .Status
                .FirstOrDefaultAsync(c => c.Id == id);

            if (status == null)
                NotFound();

            try
            {
                status.Descricao = model.Descricao;
                status.FinalizaCliente = model.FinalizaCliente;
                status.ContabilizaVenda = model.ContabilizaVenda;

                context.Status.Update(status);
                await context.SaveChangesAsync();

                return Ok(status);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("status/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var status = await context
                .Status
                .FirstOrDefaultAsync(c => c.Id == id);

            if (status == null)
                NotFound();

            try
            {
                context.Status.Remove(status);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
