using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StormSafe.DTO.Request;
using StormSafe.DTO.Response;
using StormSafe.Infrastructure.Contexts;
using StormSafe.Infrastructure.Persistence;
using System.Net;

namespace StormSafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Rios")]
    public class RioController : ControllerBase
    {
        private readonly StormSafeDbContext _context;

        public RioController(StormSafeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna lista de rios
        /// </summary>
        /// <response code="200">Lista de rios retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RioResponse>>> GetRios()
        {
            var rios = await _context.Rios
                .Select(r => new RioResponse
                {
                    RioId = r.RioId,
                    Nome = r.Nome
                })
                .ToListAsync();

            return Ok(rios);
        }

        /// <summary>
        /// Retorna um rio pelo Id
        /// </summary>
        /// <param name="id">Id do rio</param>
        /// <response code="200">Rio encontrado</response>
        /// <response code="404">Rio não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<RioResponse>> GetRio(int id)
        {
            var rio = await _context.Rios.FindAsync(id);

            if (rio == null)
                return NotFound();

            var response = new RioResponse
            {
                RioId = rio.RioId,
                Nome = rio.Nome
            };

            return Ok(response);
        }

        /// <summary>
        /// Cria um novo rio
        /// </summary>
        /// <param name="request">Dados do rio</param>
        /// <response code="201">Rio criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<RioResponse>> PostRio(RioRequest request)
        {
            var rio = new Rio { Nome = request.Nome };

            _context.Rios.Add(rio);
            await _context.SaveChangesAsync();

            var response = new RioResponse
            {
                RioId = rio.RioId,
                Nome = rio.Nome
            };

            return CreatedAtAction(nameof(GetRio), new { id = rio.RioId }, response);
        }

        /// <summary>
        /// Atualiza um rio existente
        /// </summary>
        /// <param name="id">Id do rio</param>
        /// <param name="request">Dados atualizados</param>
        /// <response code="200">Rio atualizado com sucesso</response>
        /// <response code="404">Rio não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<RioResponse>> PutRio(int id, RioRequest request)
        {
            var rio = await _context.Rios.FindAsync(id);
            if (rio == null)
                return NotFound();

            rio.Nome = request.Nome;
            _context.Rios.Update(rio);
            await _context.SaveChangesAsync();

            var response = new RioResponse
            {
                RioId = rio.RioId,
                Nome = rio.Nome
            };

            return Ok(response);
        }

        /// <summary>
        /// Deleta um rio
        /// </summary>
        /// <param name="id">Id do rio</param>
        /// <response code="204">Rio deletado com sucesso</response>
        /// <response code="404">Rio não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteRio(int id)
        {
            var rio = await _context.Rios.FindAsync(id);
            if (rio == null)
                return NotFound();

            _context.Rios.Remove(rio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
