using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        /*inicio*/
        private readonly dbColegioContext dbColegioContext;

        public AlumnoController(dbColegioContext dbColegioContext)
        {
            this.dbColegioContext = dbColegioContext;
        }
        /*fin*/
        // GET: api/<AlumnoController>
        [HttpGet]
        public async Task<ActionResult<List<Alumno>>> Get()
        {
            var alumnos = await dbColegioContext.Alumnos.ToListAsync();
            return Ok(alumnos);
        }

        // GET api/<AlumnoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var alumno = await dbColegioContext.Alumnos.FirstOrDefaultAsync(x => x.AlumnoId == id);
            if (alumno is null)
            {
                return NotFound($"El alumno con identificador: {id} no ha sido encontrado.");
            }
            return Ok(alumno);
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public async Task<ActionResult<Alumno>> Post(Alumno alumno)
        {
            if (alumno.Sexo.ToLower() != "f" && alumno.Sexo.ToLower() != "m")
            {
                return BadRequest("Sexo debe ser F/M");
            }
            var alumnoNuevo = dbColegioContext.Alumnos.Add(alumno);

            var entity = await dbColegioContext.SaveChangesAsync();
            if (entity <= 0)
            {
                return BadRequest("No se pudo crear recurso.");
            }
            //return Ok(alumno);
            return CreatedAtAction(nameof(Get), new { id = alumno.AlumnoId }, alumno);
        }

        // PUT api/<AlumnoController>/5
        [HttpPut]
        public async Task<ActionResult> Put(Alumno alumno)
        {

            var alumnoActualizar = await dbColegioContext.Alumnos.FirstOrDefaultAsync(x => x.AlumnoId == alumno.AlumnoId);
            if (alumno is null)
            {
                return NotFound($"El alumno con identificador: {alumno.AlumnoId} no ha sido encontrado.");
            }
            alumnoActualizar.Nombres = alumno.Nombres;
            alumnoActualizar.Apellidos = alumno.Apellidos;
            alumnoActualizar.Direccion = alumno.Direccion;
            alumnoActualizar.Sexo = alumno.Sexo;
            alumnoActualizar.FechaNacimiento = alumno.FechaNacimiento;
            await dbColegioContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var alumno = await dbColegioContext.Alumnos.FirstOrDefaultAsync(x => x.AlumnoId == id);
            if (alumno is null)
            {
                return NotFound($"El alumno con identificador: {id} no ha sido encontrado.");
            }
            dbColegioContext.Alumnos.Remove(alumno);
            await dbColegioContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
