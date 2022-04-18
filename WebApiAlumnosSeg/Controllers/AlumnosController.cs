using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.DTOs;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg.Controllers
{
    [ApiController]
    [Route("alumnos")]
    public class AlumnosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AlumnosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAlumnoDTO>>> Get()
        {
            var alumnos = await dbContext.Alumnos.ToListAsync();
            return mapper.Map<List<GetAlumnoDTO>>(alumnos);
        }


        [HttpGet("{id:int}")] //Se puede usar ? para que no sea obligatorio el parametro /{param=Gustavo}  getAlumno/{id:int}/
        public async Task<ActionResult<GetAlumnoDTO>> Get(int id)
        {
            var alumno = await dbContext.Alumnos
                .Include(alumnoDB => alumnoDB.AlumnoClase)
                .ThenInclude(alumnoClaseDB => alumnoClaseDB.Clase)
                .FirstOrDefaultAsync(alumnoBD => alumnoBD.Id == id);

            if (alumno == null)
            {
                return NotFound();
            }

            return mapper.Map<GetAlumnoDTO>(alumno);
            
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetAlumnoDTO>>> Get([FromRoute] string nombre)
        {
            var alumnos = await dbContext.Alumnos.Where(alumnoBD => alumnoBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetAlumnoDTO>>(alumnos);
            
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlumnoDTO alumnoDto)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeAlumnoMismoNombre = await dbContext.Alumnos.AnyAsync(x => x.Nombre == alumnoDto.Nombre);

            if (existeAlumnoMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {alumnoDto.Nombre}");
            }

            //var alumno = new Alumno()
            //{
            //    Nombre = alumnoDto.Nombre
            //};
           
            var alumno = mapper.Map<Alumno>(alumnoDto);

            dbContext.Add(alumno);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/alumnos/1
        public async Task<ActionResult> Put(Alumno alumno, int id)
        {
            var exist = await dbContext.Alumnos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            if (alumno.Id != id)
            {
                return BadRequest("El id del alumno no coincide con el establecido en la url.");
            }

            dbContext.Update(alumno);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Alumnos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Alumno()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
