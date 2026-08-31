using AutoMapper;
using StaffSphere.DTOs;
using StaffSphere.Models;

namespace StaffSphere.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<Attendance, AttendanceDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : ""));
            CreateMap<CreateAttendanceDto, Attendance>();
        }
    }
}