using Api.WeChip.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WeChip.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1")]
    public class ProdutoTipoController : ControllerBase
    {
        [HttpGet]
        [Route("produtotipos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var produtoTipos = await context
                .ProdutoTipos
                .AsNoTracking()
                .ToListAsync();

            return Ok(produtoTipos);
        }
    }
}
