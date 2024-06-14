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
    public class GradoController : ControllerBase
    {
        private readonly colegioApiDbContext _context;

        public GradoController(colegioApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Grado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grado>>> GetGrados()
        {
            return await _context.Grados.ToListAsync();
        }

        // GET: api/Grado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grado>> GetGrado(int id)
        {
            var grado = await _context.Grados.FindAsync(id);

            if (grado == null)
            {
                return NotFound();
            }

            return grado;
        }

        // PUT: api/Grado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrado(int id, Grado grado)
        {
            bool respuesta = false;
            if (id != grado.idgrado)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: El Grado no coincide" });
            }

            _context.Entry(grado).State = EntityState.Modified;

            try
            {
                int changes = await _context.SaveChangesAsync();
                respuesta = changes > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradoExists(id))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: El Grado NO se actualizo" });
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Grado se actualizo correctamente" });
        }

        // POST: api/Grado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grado>> PostGrado(Grado grado)
        {
            bool respuesta = false;
            _context.Grados.Add(grado);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            if(respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Grado se creo correctamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "El Grado no se puede Guardar" });
            }
        }

        // DELETE: api/Grado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrado(int id)
        {
            bool respuesta = false;
            var grado = await _context.Grados.FindAsync(id);
            if (grado == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "El Grado no se puede eliminar" });
            }

            _context.Grados.Remove(grado);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Grado se elimino correctamente" });
        }

        private bool GradoExists(int id)
        {
            return _context.Grados.Any(e => e.idgrado == id);
        }

        [HttpGet("{id}/alumnos")]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnosPorGrado(int id)
        {
            try
            {
                var alumnos = await _context.Matriculas
                    .Where(m => m.idgrado == id)
                    .Include(m => m.IdAlumnoNavigation)
                    .Select(m => m.IdAlumnoNavigation)
                    .ToListAsync();

                if(!alumnos.Any())
                {
                    return NotFound();
                }

                return alumnos;
            }
            catch
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
