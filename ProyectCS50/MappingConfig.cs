using AutoMapper;
using ProyectCS50.Models;
using ProyectCS50.Models.Dto;

namespace ProyectCS50
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Empleado, EmpleadoDto>().ReverseMap();
            CreateMap<Empleado, EmpleadoCreateDto>().ReverseMap();
            CreateMap<Empleado, EmpleadoUpdateDto>().ReverseMap();
            CreateMap<Deduccione, DeduccionDto>().ReverseMap();
            CreateMap<Deduccione, DeduccionCreateDto>().ReverseMap();
            CreateMap<Deduccione, DeduccionUpdateDto>().ReverseMap();
            CreateMap<Ingreso, IngresoDto>().ReverseMap();
            CreateMap<Ingreso, IngresoCreateDto>().ReverseMap();
            CreateMap<Ingreso, IngresoUpdateDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<Nomina, NominaDto>().ReverseMap();
            CreateMap<Nomina, NominaCreateDto>().ReverseMap();
            CreateMap<Nomina, NominaUpdateDto>().ReverseMap();

            CreateMap<DetalleDeduccione, DetalleDeduccionDto>().ReverseMap();
            CreateMap<DetalleDeduccione, DetalleDeduccionCreateDto>().ReverseMap();
            CreateMap<DetalleDeduccione, DetalleDeduccionUpdateDto>().ReverseMap();

            CreateMap<DetalleIngreso, DetalleIngresoDto>().ReverseMap();
            CreateMap<DetalleIngreso, DetalleIngresoCreateDto>().ReverseMap();
            CreateMap<DetalleIngreso, DetalleIngresoUpdateDto>().ReverseMap();



        }

    }
}
