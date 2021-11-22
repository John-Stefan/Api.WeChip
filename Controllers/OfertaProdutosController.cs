using Api.WeChip.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WeChip.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1")]
    public class OfertaProdutosController : ControllerBase
    {
        [HttpGet]
        [Route("ofertaprodutos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var ofertas = await context
                .OfertaProdutos
                .AsNoTracking()
                .ToListAsync();

            return Ok(ofertas);
        }
    }
}
