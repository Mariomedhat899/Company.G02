using AutoMapper;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;

namespace Company.G02.PL.Mapping
{
    public class EmployeeProfle : Profile
    {

        public EmployeeProfle()
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();



        }
    }
}
