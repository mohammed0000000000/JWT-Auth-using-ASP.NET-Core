using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Models;
using WebAPIDemo.Repository;
using WebAPIDemo.Services.Contracts;
using WebAPIDemo.Services.ViewModels;

namespace WebAPIDemo.Services.Implementation
{
    public class EmployoeeService : IEmployeeService
    {
        private readonly IRepository<Employee, int> employeeRepository;
        private readonly IMapper employeeMaper;
        private readonly ILogger<EmployoeeService> logger;
        public EmployoeeService(IRepository<Employee, int> employeeRepository, IMapper employeeMaper, ILogger<EmployoeeService>logger) {
            this.employeeRepository = employeeRepository;  
            this.employeeMaper = employeeMaper;
            this.logger = logger;
        }
        public async Task<EmployeeViewModel> Create(EmployeeViewModel employeeModel) {
            try{
                var employee = employeeMaper.Map<Employee>(employeeModel);
                var createdEmployee = await employeeRepository.Create(employee);
                if (createdEmployee != null){
                    return employeeMaper.Map<EmployeeViewModel>(createdEmployee);
                }
                return null;
            }
            catch(DbUpdateException ex){
                // Log the error if necessary
                //_logger.LogError(dbEx, "Database error occurred while creating employee.");
                throw;
            }
            catch(Exception ex){
                // Catch general exceptions, log and rethrow if needed
                // _logger.LogError(ex, "An error occurred while creating employee.");
                throw;
            }
        }

        public async Task<bool> DeleteById(int id) {
            try {
                var result = await employeeRepository.DeleteById(id);
                return result;
            } catch (Exception ex) {
                return false;
            }
        }

        public async Task< List<EmployeeViewModel>> GetAll() {
            try {
                var List = await employeeRepository.GetAll();
                var employeeList = List.ToList();
                List<EmployeeViewModel> employees = null;
                if (employeeList is not null) {
                    employees = employeeMaper.Map<List<EmployeeViewModel>>(employeeList);
                }
                return employees;
            } catch {
                return null;
            }
        }

        public async Task<EmployeeViewModel> GetById(int id) {
            try {
                var employeeModel = await employeeRepository.getById(id);
                EmployeeViewModel employeeViewModel = null;
                if (employeeModel is not null) {
                    employeeViewModel = employeeMaper.Map<EmployeeViewModel>(employeeModel);
                }
                return employeeViewModel;
            } catch (Exception ex) {
                return null;
            }
        }

        public async Task<bool> UpdateEmp(EmployeeViewModel employee) {
            try {
                var employeeModel = await employeeRepository.getById(employee.Id);
                if (employeeModel is null) return false;
                var updatedEmployee = employeeMaper.Map(employee, employeeModel);
                return await employeeRepository.Update(employeeModel) ? true : false;   

            } 
            catch(DbUpdateException dbEx){
                logger.LogError(dbEx, dbEx.Message);
                throw;
            }
            catch (Exception ex) {
                logger.LogError( ex.Message);
                throw;
            }
            return false;
        }

    }
}
