using System.ComponentModel.DataAnnotations;
using WebApiAlumnosSeg.Validaciones;

namespace WebApiAlumnosSeg.DTOs
{
    public class ClaseCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<int> AlumnosIds { get; set; }
    }
}
