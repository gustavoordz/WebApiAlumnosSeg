using System.ComponentModel.DataAnnotations;

namespace WebApiAlumnosSeg.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
