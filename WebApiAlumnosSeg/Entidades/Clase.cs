using System.ComponentModel.DataAnnotations;
using WebApiAlumnosSeg.Validaciones;

namespace WebApiAlumnosSeg.Entidades
{
    public class Clase
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
