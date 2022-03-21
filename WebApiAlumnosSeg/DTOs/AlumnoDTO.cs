using System.ComponentModel.DataAnnotations;
using WebApiAlumnosSeg.Validaciones;

namespace WebApiAlumnosSeg.DTOs
{
    public class AlumnoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
