using AutoMapper;
using WebApiAlumnosSeg.DTOs;
using WebApiAlumnosSeg.Entidades;

namespace WebApiAlumnosSeg.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AlumnoDTO, Alumno>();
            CreateMap<Alumno, GetAlumnoDTO>();
        }
    }
}
