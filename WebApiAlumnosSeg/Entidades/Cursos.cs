using Microsoft.AspNetCore.Identity;

namespace WebApiAlumnosSeg.Entidades
{
    public class Cursos
    {
        public int Id { get; set; }
        public string Contenido { get; set; }

        public int ClaseId { get; set; }

        public Clase Clase { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
