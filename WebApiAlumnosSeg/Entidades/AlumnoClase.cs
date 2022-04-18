namespace WebApiAlumnosSeg.Entidades
{
    public class AlumnoClase
    {
        public int AlumnoId { get; set; }
        public int ClaseId  { get; set; }
        public int Orden { get; set; }
        public Alumno Alumno { get; set; }
        public Clase Clase { get; set; }

    }
}
