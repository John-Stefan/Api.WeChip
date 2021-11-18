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
    public class OfertaController : ControllerBase
    {
        [HttpGet]
        [Route("ofertas")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var ofertas = await context
                .Ofertas
                .AsNoTracking()
                .ToListAsync();

            return Ok(ofertas);
        }

        [HttpGet]
        [Route("ofertas/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var oferta = await context
                .Ofertas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return oferta == null
                ? NotFound()
                : Ok(oferta);
        }

        [HttpPost("ofertas")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateOfertaViewModel model)
        {
            var tipoHardware = 1;
            List<OfertaProduto> listaProdutos = new List<OfertaProduto>();

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var cliente = context.Clientes.FirstOrDefault(c => c.Id == model.ClienteId);
                if (cliente == null)
                    return BadRequest("Cliente informado não encontrado");

                foreach (var produto in model.ProdutosOfertasId)
                {
                    var produtoOferta = context.Produtos.FirstOrDefault(c => c.Id == produto);
                    if (produtoOferta == null)
                        throw new Exception("Produto informado não existe");

                    var ofertaProduto = new OfertaProduto
                    {
                        Descricao = produtoOferta.Descricao,
                        Produto = produtoOferta
                    };

                    model.ValorTotal += produtoOferta.Preco;
                    listaProdutos.Add(ofertaProduto);
                }

                var endereco = new Endereco
                {
                    CEP = model.Endereco.CEP,
                    Bairro = model.Endereco.Bairro,
                    Cidade = model.Endereco.Cidade,
                    Complemento = model.Endereco.Complemento,
                    Estado = model.Endereco.Estado,
                    Numero = model.Endereco.Numero,
                    Rua = model.Endereco.Rua
                };

                var oferta = new Oferta
                {
                    ValorTotal = model.ValorTotal,
                    Cliente = cliente,
                    Endereco = endereco,
                    OfertaProdutos = listaProdutos
                };

                var produtoHardware = listaProdutos.FirstOrDefault(c => c.Produto.TipoId == tipoHardware);
                if (model.ValorTotal > decimal.Zero && endereco == null && produtoHardware != null)
                    throw new Exception("Endereço é obrigatorio");

                if (model.ValorTotal > cliente.Credito)
                    throw new Exception($"Credito do cliente {cliente.Nome} insuficiênte");

                cliente.Credito -= model.ValorTotal;
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();

                await context.Ofertas.AddAsync(oferta);
                await context.SaveChangesAsync();
                return Created($"v1/ofertas/{oferta.Id}", oferta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
