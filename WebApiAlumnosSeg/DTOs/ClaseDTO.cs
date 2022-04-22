namespace WebApiAlumnosSeg.DTOs
{
    public class ClaseDTO
    {
        public int Id { get; set; }
     
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
        public List<CursoDTO> Cursos { get; set; }
    }
}
