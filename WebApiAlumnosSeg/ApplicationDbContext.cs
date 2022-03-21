using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Clase> Clases { get; set; }
    }
}
