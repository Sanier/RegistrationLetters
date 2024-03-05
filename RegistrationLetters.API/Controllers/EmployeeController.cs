using Microsoft.AspNetCore.Mvc;
using RegistrationLetters.Domain.Models;
using RegistrationLetters.Services.Interfaces;

namespace RegistrationLetters.API.Controllers
{
    /// <summary>
    /// The EmployeeController class is a controller that handles HTTP requests related to employees.
    /// </summary>
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Handles the HTTP GET request to get all employees.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult that can be converted to an HTTP response message.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeService.GetAllEmployees();

            if (response.StatusCode == Domain.Enum.StatusCode.Success)
                return Ok(response.Data);

            return BadRequest(response.Description);
        }

        /// <summary>
        /// Handles the HTTP GET request to get employee.
        /// </summary>
        /// <param name="employeeModel">The EmployeeModel instance to create.</param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> GetEmployee(EmployeeModel employeeModel)
        {
            var response = await _employeeService.GetEmployee(employeeModel);

            if (response.StatusCode == Domain.Enum.StatusCode.Success)
                return Ok(response.Data);

            return BadRequest(response.Description);
        }

        /// <summary>
        /// Handles the HTTP POST request to create a new employee.
        /// </summary>
        /// <param name="employeeModel">The EmployeeModel instance to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult that can be converted to an HTTP response message.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewEmployee([FromBody]EmployeeModel employeeModel)
        {
            var response = await _employeeService.CreateNewEmployee(employeeModel);

            if (response.StatusCode == Domain.Enum.StatusCode.Success)
                return Ok(response.Description);

            return BadRequest(response.Description);
        }
    }
}
