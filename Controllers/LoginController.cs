using Api.WeChip.Data;
using Api.WeChip.Models;
using Api.WeChip.Services;
using Api.WeChip.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.WeChip.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateUsuarioViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetValidationState);

                var usuarioExistente = context.Usuarios.FirstOrDefault(c => c.Username == model.Username);
                if (usuarioExistente != null)
                    throw new Exception("Usuario informado já foi cadastrado");

                var usuario = new Usuario
                {
                    Username = model.Username,
                    Password = model.Password
                };

                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();
                usuario.Password = "";

                return Created($"v1/login/{usuario.Id}", usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("login/{username}/{password}")]
        public async Task<IActionResult> ObterTokenAsync(
            [FromServices] AppDbContext context, [FromRoute] string username, [FromRoute] string password)
        {
            // Recupera o usuario
            var usuario = await context
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Username == username && c.Password == password);

            // Verifica se o usuario existe
            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(usuario);

            // Oculta a senha
            usuario.Password = "";

            // Retorna o token
            return usuario == null
                ? NotFound()
                : Ok(token);
        }
    }
}
