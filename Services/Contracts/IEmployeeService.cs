using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebAPIDemo.Services.ViewModels;

namespace WebAPIDemo.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeViewModel> GetById(int id);
        Task<List<EmployeeViewModel>> GetAll();    

        Task<bool> UpdateEmp(EmployeeViewModel employee); 
        Task<bool> DeleteById(int id);

        Task<EmployeeViewModel> Create(EmployeeViewModel employee);
    }
}
