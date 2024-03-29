﻿using Api.WeChip.Data;
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
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("clientes")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var clientes = await context
                .Clientes
                .AsNoTracking()
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet]
        [Route("clientes/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var clientes = await context
                .Clientes
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Ok(clientes);
        }

        [HttpGet]
        [Route("clientes/grid/{busca}")]
        public async Task<IActionResult> GetCLientesByNameOrCPFAsync(
            [FromServices] AppDbContext context,
            [FromRoute] string busca)
        {
            var clientes = await context
                .Clientes
                .Where(c => c.Nome.ToUpper().Contains(busca.ToUpper()) || c.CPF == busca)
                .AsNoTracking()
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet]
        [Route("clientes/busca/{nameorcpf}")]
        public async Task<IActionResult> GetByNameOrCpfAsync(
            [FromServices] AppDbContext context,
            [FromRoute] string nameorcpf)
        {
            var cliente = await context
                .Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Nome.ToUpper() == nameorcpf.ToUpper() || c.CPF == nameorcpf);

            return cliente == null
                ? NotFound()
                : Ok(cliente);
        }

        [HttpPost("clientes")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var status = context.Status.FirstOrDefault(c => c.Codigo == model.StatusId);
            if (status == null)
                return BadRequest();

            var cliente = new Cliente
            {
                Nome = model.Nome,
                CPF = model.CPF,
                Telefone = model.Telefone,
                Credito = model.Credito,
                Status = status
            };

            try
            {
                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();
                return Created($"v1/clientes/{cliente.Id}", cliente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("clientes/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] UpdateClienteViewModel model,
            [FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (cliente == null)
                    NotFound();

                var status = context.Status.FirstOrDefault(c => c.Codigo == model.StatusId);
                if (status == null)
                    return BadRequest();

                cliente.Nome = model.Nome;
                cliente.CPF = model.CPF;
                cliente.Telefone = model.Telefone;
                cliente.Credito = model.Credito;
                cliente.Status = status;

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();

                return Ok(cliente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("clientes/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var cliente = await context
                .Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                NotFound();

            try
            {
                context.Clientes.Remove(cliente);
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
