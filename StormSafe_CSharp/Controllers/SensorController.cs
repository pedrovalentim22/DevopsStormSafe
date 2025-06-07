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
    [Tags("Sensores")]
    public class SensorController : ControllerBase
    {
        private readonly StormSafeDbContext _context;

        public SensorController(StormSafeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna lista de sensores
        /// </summary>
        /// <response code="200">Lista de sensores retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<SensorResponse>>> GetSensores()
        {
            var sensores = await _context.Sensores
                .Include(s => s.Rio)
                .Select(s => new SensorResponse
                {
                    SensorId = s.SensorId,
                    Tipo = s.Tipo,
                    RioId = s.RioId,
                    RioNome = s.Rio.Nome
                })
                .ToListAsync();

            return Ok(sensores);
        }

        /// <summary>
        /// Retorna um sensor por Id
        /// </summary>
        /// <param name="id">Id do sensor</param>
        /// <response code="200">Sensor encontrado</response>
        /// <response code="404">Sensor não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<SensorResponse>> GetSensor(int id)
        {
            var sensor = await _context.Sensores
                .Include(s => s.Rio)
                .FirstOrDefaultAsync(s => s.SensorId == id);

            if (sensor == null)
                return NotFound();

            var response = new SensorResponse
            {
                SensorId = sensor.SensorId,
                Tipo = sensor.Tipo,
                RioId = sensor.RioId,
                RioNome = sensor.Rio.Nome
            };

            return Ok(response);
        }

        /// <summary>
        /// Cria um novo sensor
        /// </summary>
        /// <param name="request">Dados do sensor</param>
        /// <response code="201">Sensor criado com sucesso</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<SensorResponse>> PostSensor(SensorRequest request)
        {
            var sensor = new Sensor
            {
                Tipo = request.Tipo,
                RioId = request.RioId
            };

            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();

            var response = new SensorResponse
            {
                SensorId = sensor.SensorId,
                Tipo = sensor.Tipo,
                RioId = sensor.RioId,
                RioNome = (await _context.Rios.FindAsync(sensor.RioId))?.Nome ?? "Desconhecido"
            };

            return CreatedAtAction(nameof(GetSensor), new { id = sensor.SensorId }, response);
        }

        /// <summary>
        /// Atualiza um sensor existente
        /// </summary>
        /// <param name="id">Id do sensor</param>
        /// <param name="request">Dados atualizados</param>
        /// <response code="200">Sensor atualizado com sucesso</response>
        /// <response code="404">Sensor não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<SensorResponse>> PutSensor(int id, SensorRequest request)
        {
            var sensor = await _context.Sensores.Include(s => s.Rio).FirstOrDefaultAsync(s => s.SensorId == id);
            if (sensor == null)
                return NotFound();

            sensor.Tipo = request.Tipo;
            sensor.RioId = request.RioId;

            _context.Sensores.Update(sensor);
            await _context.SaveChangesAsync();

            var response = new SensorResponse
            {
                SensorId = sensor.SensorId,
                Tipo = sensor.Tipo,
                RioId = sensor.RioId,
                RioNome = sensor.Rio?.Nome ?? "Desconhecido"
            };

            return Ok(response);
        }

        /// <summary>
        /// Deleta um sensor
        /// </summary>
        /// <param name="id">Id do sensor</param>
        /// <response code="204">Sensor deletado com sucesso</response>
        /// <response code="404">Sensor não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensores.FindAsync(id);
            if (sensor == null)
                return NotFound();

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
