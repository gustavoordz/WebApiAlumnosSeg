using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg.Controllers
{
    [ApiController]
    [Route("clases")]
    public class ClasesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ClasesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        [HttpGet("/listadoClase")]
        public async Task<ActionResult<List<Clase>>> GetAll()
        {
            return await dbContext.Clases.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Clase>> GetById(int id)
        {
            return await dbContext.Clases.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Clase clase)
        {
            //var existeAlumno = await dbContext.Alumnos.AnyAsync(x => x.Id == clase.AlumnoId);

            //if (!existeAlumno)
            //{
            //    return BadRequest($"No existe el alumno con el id: {clase.AlumnoId} ");
            //}

            dbContext.Add(clase);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Clase clase, int id)
        {
            var exist = await dbContext.Clases.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("La clase especificada no existe. ");
            }

            if (clase.Id != id)
            {
                return BadRequest("El id de la clase no coincide con el establecido en la url. ");
            }

            dbContext.Update(clase);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Clases.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            //var validateRelation = await dbContext.AlumnoClase.AnyAsync


            dbContext.Remove(new Clase { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
