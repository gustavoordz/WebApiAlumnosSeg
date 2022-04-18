using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.DTOs;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg.Controllers
{
    [ApiController]
    [Route("clases/{claseId:int}/cursos")]
    public class CursosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CursosController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CursoDTO>>> Get(int claseId)
        {
            var existeClase = await dbContext.Clases.AnyAsync(claseDB => claseDB.Id == claseId);
            if (!existeClase)
            {
                return NotFound();
            }

            var cursos = await dbContext.Cursos.Where(cursoDB => cursoDB.ClaseId == claseId).ToListAsync();

            return mapper.Map<List<CursoDTO>>(cursos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int claseId, CursoCreacionDTO cursoCreacionDTO)
        {
            var existeClase = await dbContext.Clases.AnyAsync(claseDB => claseDB.Id == claseId);
            if (!existeClase)
            {
                return NotFound();
            }

            var curso = mapper.Map<Cursos>(cursoCreacionDTO);
            curso.ClaseId = claseId;
            dbContext.Add(curso);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
