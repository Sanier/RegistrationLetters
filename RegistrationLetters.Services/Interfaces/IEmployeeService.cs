using RegistrationLetters.Domain.Models;
using RegistrationLetters.Domain.Response;

namespace RegistrationLetters.Services.Interfaces
{
    /// <summary>
    /// The IEmployeeService interface defines the operations for managing employees.
    /// </summary>
    public interface IEmployeeService
    {
        Task<IBaseResponse<IEnumerable<EmployeeModel>>> GetAllEmployees();

        Task<IBaseResponse<EmployeeModel>> GetEmployee(EmployeeModel employeeModel);

        Task<IBaseResponse<IEnumerable<EmployeeModel>>> CreateNewEmployee(EmployeeModel employeeModel);
    }
}
