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
    public class ProfesorController : ControllerBase
    {
        private readonly colegioApiDbContext _context;

        public ProfesorController(colegioApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesors()
        {
            return await _context.Profesors.ToListAsync();
        }

        // GET: api/Profesor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            var profesor = await _context.Profesors.FindAsync(id);

            if (profesor == null)
            {
                return NotFound();
            }

            return profesor;
        }

        // PUT: api/Profesor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(int id, Profesor profesor)
        {
            bool respuesta = false;
            if (id != profesor.IdProfesor)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: El Profesor no coincide" });
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                int changes = await _context.SaveChangesAsync();
                respuesta = changes > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: El Profesor NO se actualizo" });
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Profesor se actualizo correctamente" });
        }

        // POST: api/Profesor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            bool respuesta = false;
            _context.Profesors.Add(profesor);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            if(respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Profesor se creo correctamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "El Profesor no se puede Guardar" });
            }
        }

        // DELETE: api/Profesor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            bool respuesta = false;
            var profesor = await _context.Profesors.FindAsync(id);
            if (profesor == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "El profesor no se puede eliminar" });
            }

            _context.Profesors.Remove(profesor);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El profesor se elimino correctamente" });
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesors.Any(e => e.IdProfesor == id);
        }
    }
}
