using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly colegioApiDbContext _context;

        public MatriculaController(colegioApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Matricula
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetMatriculas()
        {
            return await _context.Matriculas.ToListAsync();
        }

        // GET: api/Matricula/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatricula(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            if (matricula == null)
            {
                return NotFound();
            }

            return matricula;
        }

        // PUT: api/Matricula/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatricula(int id, Matricula matricula)
        {
            bool respuesta = false;
            if (id != matricula.idmatricula)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: La matricula no coincide" });
            }

            _context.Entry(matricula).State = EntityState.Modified;

            try
            {
                int changes = await _context.SaveChangesAsync();
                respuesta = changes > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: La matricula NO se actualizo" });
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "La matricula se actualizo correctamente" });
        }

        // POST: api/Matricula
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matricula>> PostMatricula(Matricula matricula)
        {
            bool respuesta = false;
            _context.Matriculas.Add(matricula);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            if(respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "La matricula se creo correctamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "La matricula no se puede Guardar" });
            }
        }

        // DELETE: api/Matricula/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(int id)
        {
            bool respuesta = false;
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "La matricula no se puede eliminar" });
            }

            _context.Matriculas.Remove(matricula);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "La matricula se elimino correctamente" });
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.idmatricula == id);
        }
    }
}
