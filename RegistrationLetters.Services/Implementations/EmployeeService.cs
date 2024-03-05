using Microsoft.EntityFrameworkCore;
using RegistrationLetters.DAL.Interfaces;
using RegistrationLetters.Domain.Entities;
using RegistrationLetters.Domain.Enum;
using RegistrationLetters.Domain.Models;
using RegistrationLetters.Domain.Response;
using RegistrationLetters.Services.Interfaces;

namespace RegistrationLetters.Services.Implementations
{
    /// <summary>
    /// This class provides the operations for managing employees.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepositories<EmployeeEntity> _employeeRepository;

        public EmployeeService(IBaseRepositories<EmployeeEntity> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Asynchronously creates a new EmployeeModel instance.
        /// </summary>
        /// <param name="employeeModel">The EmployeeModel instance to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a base response with a collection of EmployeeModel.</returns>
        public async Task<IBaseResponse<IEnumerable<EmployeeModel>>> CreateNewEmployee(EmployeeModel employeeModel)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeModel.FirstName) && string.IsNullOrEmpty(employeeModel.LastName))
                    throw new ArgumentNullException("Recipient field cannot be empty");

                var employee = await _employeeRepository.Get()
                    .FirstOrDefaultAsync(e => 
                    e.FirstName == employeeModel.FirstName 
                    || e.LastName == employeeModel.LastName);

                if (employee is not null)
                    throw new ArgumentNullException("Recipient field cannot be empty");

                employee = new EmployeeEntity()
                {
                    FirstName = employeeModel.FirstName,
                    LastName = employeeModel.LastName,
                    JobPosition = employeeModel.JobPosition,
                };

                await _employeeRepository.Create(employee);
                await _employeeRepository.Update(employee);

                return new BaseResponse<IEnumerable<EmployeeModel>>()
                {
                    Description = "Успешно создан",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<EmployeeModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Asynchronously gets all EmployeeModel instances.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a base response with a collection of EmployeeModel.</returns>
        public async Task<IBaseResponse<IEnumerable<EmployeeModel>>> GetAllEmployees()
        {
            try
            {
                var employee = await _employeeRepository.Get()
                    .Select(l => new EmployeeModel
                    {
                        Id = l.Id,
                        FirstName = l.FirstName,
                        LastName = l.LastName,
                        JobPosition = l.JobPosition
                    })
                    .ToListAsync();

                if (employee is null)
                    throw new ArgumentNullException();

                return new BaseResponse<IEnumerable<EmployeeModel>>()
                {
                    Data = employee,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<EmployeeModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EmployeeModel>> GetEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var employee = await _employeeRepository.Get()
                    .Where(e => (e.FirstName == employeeModel.FirstName && e.LastName == employeeModel.LastName)
                    || e.Id == employeeModel.Id)
                    .Select(e => new EmployeeModel
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobPosition = e.JobPosition
                    })
                    .FirstOrDefaultAsync();


                if (employee is null)
                    throw new ArgumentNullException();

                return new BaseResponse<EmployeeModel>()
                {
                    Data = employee,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeModel>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
