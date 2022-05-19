using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.DTOs;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg.Controllers
{
    [ApiController]
    [Route("clases/{claseId:int}/cursos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CursosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public CursosController(ApplicationDbContext dbContext, IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
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

        [HttpGet("{id:int}", Name ="obtenerCurso")]
        public async Task<ActionResult<CursoDTO>> GetById(int id)
        {
            var curso = await dbContext.Cursos.FirstOrDefaultAsync(cursoDB => cursoDB.Id == id);

            if(curso == null)
            {
                return NotFound();
            }

            return mapper.Map<CursoDTO>(curso);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(int claseId, CursoCreacionDTO cursoCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existeClase = await dbContext.Clases.AnyAsync(claseDB => claseDB.Id == claseId);
            if (!existeClase)
            {
                return NotFound();
            }

            var curso = mapper.Map<Cursos>(cursoCreacionDTO);
            curso.ClaseId = claseId;
            curso.UsuarioId = usuarioId;
            dbContext.Add(curso);
            await dbContext.SaveChangesAsync();

            var cursoDTO = mapper.Map<CursoDTO>(curso);

            return CreatedAtRoute("obtenerCurso", new {id = curso.Id, claseId = claseId }, cursoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int claseId, int id, CursoCreacionDTO cursoCreacionDTO)
        {
            var existeClase = await dbContext.Clases.AnyAsync(claseDB => claseDB.Id == claseId);
            if (!existeClase)
            {
                return NotFound();
            }

            var existeCurso = await dbContext.Cursos.AnyAsync(cursoDB => cursoDB.Id == id);
            if (!existeCurso)
            {
                return NotFound();
            }

            var curso = mapper.Map<Cursos>(cursoCreacionDTO);
            curso.Id = id;
            curso.ClaseId = claseId;

            dbContext.Update(curso);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
