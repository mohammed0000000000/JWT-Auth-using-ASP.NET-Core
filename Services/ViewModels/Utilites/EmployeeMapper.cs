using AutoMapper;
using WebAPIDemo.Models;
namespace WebAPIDemo.Services.ViewModels.Utilites
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
