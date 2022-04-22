using System.ComponentModel.DataAnnotations;
using WebApiAlumnosSeg.Validaciones;

namespace WebApiAlumnosSeg.Entidades
{
    public class Clase
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<Cursos> Cursos { get; set; }

        public List<AlumnoClase> AlumnoClase { get; set; }
    }
}
