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
    public class AlumnoController : ControllerBase
    {
        private readonly colegioApiDbContext _context;

        public AlumnoController(colegioApiDbContext context)
        {
            _context = context;
        }

        // GET: api/AlumnoFullName
        [HttpGet("/getFUllName")]
        public ActionResult<IEnumerable<AlumnoFullName>> GetAlumnosFullName()
        {

            var alumnoFullName = (from b in _context.Alumnos
                        select new AlumnoFullName()
                        {
                            IdAlumno = b.IdAlumno,
                            FullName = (b.Nombre + " " + b.Apellido),
                            FechaNacimiento = b.FechaNacimiento,
                            Genero = b.Genero
                        }).ToList();

            return alumnoFullName;

        }

        // GET: api/Alumno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

       

        // GET: api/Alumno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }

        // PUT: api/Alumno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumno)
        {
            bool respuesta = false;
            if (id != alumno.IdAlumno)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: El Alumno no coincide" });
            }

            _context.Entry(alumno).State = EntityState.Modified;

            try
            {
                int changes = await _context.SaveChangesAsync();
                respuesta = changes > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "Error: El Alumno NO se actualizo" });
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Alumno se actualizo correctamente" });
        }

        // POST: api/Alumno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            bool respuesta = false;
            _context.Alumnos.Add(alumno);
            int changes = await _context.SaveChangesAsync();
            respuesta = changes > 0;
            if(respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Alumno se creo correctamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "El Alumno no se puede Guardar" });
            }
        }

        // DELETE: api/Alumno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            bool respuesta = false;
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = respuesta, message = "El Alumno no se puede eliminar" });
            }

            _context.Alumnos.Remove(alumno);
            int changes =  await _context.SaveChangesAsync();
            respuesta = changes > 0;
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, message = "El Alumno se elimino correctamente" });
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.IdAlumno == id);
        }
    }

    public class AlumnoFullName
    {
        public int IdAlumno { get; set; }
        public string? FullName { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Genero { get; set; }
    }
}
