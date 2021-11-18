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
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var produtos = await context
                .Produtos
                .ToListAsync();

            return Ok(produtos);
        }

        [HttpGet]
        [Route("produtos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var produto = await context
                .Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return produto == null
                ? NotFound()
                : Ok(produto);
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produtoTipo = context.ProdutoTipos.FirstOrDefault(c => c.Id == model.Tipo);
            if (produtoTipo == null)
                return BadRequest();

            var produto = new Produto
            {
                Codigo = model.Codigo,
                Descricao = model.Descricao,
                Preco = model.Preco,
                Tipo = produtoTipo
            };

            var buscaProdutos = context
                .Produtos
                .FirstOrDefaultAsync(c => c.Codigo == produto.Codigo);

            try
            {
                if (buscaProdutos?.Result != null)
                    throw new Exception("Produto informado já existe no banco de dados");

                await context.Produtos.AddAsync(produto);
                await context.SaveChangesAsync();
                return Created($"v1/produtos/{produto.Id}", produto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("produtos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] UpdateProdutoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var produto = await context
                .Produtos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (produto == null)
                NotFound();

            try
            {
                produto.Descricao = model.Descricao;
                produto.Preco = model.Preco;

                context.Produtos.Update(produto);
                await context.SaveChangesAsync();

                return Ok(produto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("produtos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var produto = await context
                .Produtos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (produto == null)
                NotFound();

            try
            {
                context.Produtos.Remove(produto);
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
