using Microsoft.EntityFrameworkCore;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlumnoClase>()
                .HasKey(al => new { al.AlumnoId, al.ClaseId });
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<Cursos> Cursos { get; set; }

        public DbSet<AlumnoClase> AlumnoClase { get; set; }
    }
}
