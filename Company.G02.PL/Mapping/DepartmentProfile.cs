using AutoMapper;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;

namespace Company.G02.PL.Mapping
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();

        }
    }
}
