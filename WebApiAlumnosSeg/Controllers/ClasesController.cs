using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.DTOs;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg.Controllers
{
    [ApiController]
    [Route("clases")]
    public class ClasesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ClasesController(ApplicationDbContext context,IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoClase")]
        public async Task<ActionResult<List<Clase>>> GetAll()
        {
            return await dbContext.Clases.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerClase")]
        public async Task<ActionResult<ClaseDTOConAlumnos>> GetById(int id)
        {
            var clase =  await dbContext.Clases
                .Include(claseDB => claseDB.AlumnoClase)
                .ThenInclude(alumnoClaseDB => alumnoClaseDB.Alumno)
                .Include(cursoDB => cursoDB.Cursos)
                .FirstOrDefaultAsync(x => x.Id == id);

            clase.AlumnoClase = clase.AlumnoClase.OrderBy(x => x.Orden).ToList();

            return mapper.Map<ClaseDTOConAlumnos>(clase);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClaseCreacionDTO claseCreacionDTO)
        {

            if(claseCreacionDTO.AlumnosIds == null)
            {
                return BadRequest("No se puede crear una clase sin alumnos.");
            }

            var alumnosIds = await dbContext.Alumnos
                .Where(alumnoBD => claseCreacionDTO.AlumnosIds.Contains(alumnoBD.Id)).Select(x => x.Id).ToListAsync();

            if(claseCreacionDTO.AlumnosIds.Count != alumnosIds.Count)
            {
                return BadRequest("No existe uno de los alumnos enviados");
            }

            var clase = mapper.Map<Clase>(claseCreacionDTO);

            OrdenarPorAlumnos(clase);

            dbContext.Add(clase);
            await dbContext.SaveChangesAsync();

            var claseDTO = mapper.Map<ClaseDTO>(clase);

            return CreatedAtRoute("obtenerClase", new {id = clase.Id}, claseDTO);
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(Clase clase, int id)
        //{
        //    var exist = await dbContext.Clases.AnyAsync(x => x.Id == id);

        //    if (!exist)
        //    {
        //        return NotFound("La clase especificada no existe. ");
        //    }

        //    if (clase.Id != id)
        //    {
        //        return BadRequest("El id de la clase no coincide con el establecido en la url. ");
        //    }

        //    dbContext.Update(clase);
        //    await dbContext.SaveChangesAsync();
        //    return Ok();

        //}

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ClaseCreacionDTO claseCreacionDTO)
        {
            var claseDB = await dbContext.Clases
                .Include(x => x.AlumnoClase)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(claseDB == null)
            {
                return NotFound();
            }

            claseDB = mapper.Map(claseCreacionDTO, claseDB);

            OrdenarPorAlumnos(claseDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        private void OrdenarPorAlumnos(Clase clase)
        {
            if (clase.AlumnoClase != null)
            {
                for (int i = 0; i < clase.AlumnoClase.Count; i++)
                {
                    clase.AlumnoClase[i].Orden = i;
                }
            }
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
