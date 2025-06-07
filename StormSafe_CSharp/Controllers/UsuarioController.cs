
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StormSafe.Domain.Enums;
using StormSafe.DTO.Request;
using StormSafe.DTO.Response;
using StormSafe.Infrastructure.Contexts;
using StormSafe.Infrastructure.Persistence;
using System.Net;

namespace StormSafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Usuários")]
    public class UsuarioController: ControllerBase
    {
        private readonly StormSafeDbContext _context;

        public UsuarioController(StormSafeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna lista de usuários
        /// </summary>
        /// <response code="200">Lista de usuários retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {
            var usuariosDto = await _context.Usuarios
                .Select(u => new UsuarioResponse
                {
                    IdUsuario = u.IdUsuario,
                    Nome = u.Nome,
                    Email = u.Email,
                    TipoUsuario = u.TipoUsuario.ToString()
                })
                .ToListAsync();

            return Ok(usuariosDto);
        }

        /// <summary>
        /// Retorna usuário por Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UsuarioResponse>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var dto = new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario.ToString()
            };

            return Ok(dto);
        }

        /// <summary>
        /// Cria novo usuário
        /// </summary>
        /// <param name="request">Dados do usuário</param>
        /// <response code="201">Usuário criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<UsuarioResponse>> PostUsuario(UsuarioRequest request)
        {
            var usuario = Usuario.Create(request.Nome, request.Email, request.Senha,
                Enum.TryParse(request.TipoUsuario, true, out TipoUsuario tipo) ? tipo : TipoUsuario.ADMIN);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var response = new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario.ToString()
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, response);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="request">Dados atualizados</param>
        /// <response code="200">Usuário atualizado com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UsuarioResponse>> PutUsuario(Guid id, UsuarioRequest request)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.AtualizarUsuario(request.Nome, request.Email, request.Senha,
                Enum.TryParse(request.TipoUsuario, true, out TipoUsuario tipo) ? tipo : TipoUsuario.CIVIL);

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            var response = new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario.ToString()
            };

            return Ok(response);
        }


        /// <summary>
        /// Deleta usuário pelo Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <response code="204">Usuário deletado com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
