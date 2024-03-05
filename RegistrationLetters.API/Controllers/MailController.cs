using Microsoft.AspNetCore.Mvc;
using RegistrationLetters.Domain.Models;
using RegistrationLetters.Services.Interfaces;

namespace RegistrationLetters.API.Controllers
{
    /// <summary>
    /// The MailController class is a controller that handles HTTP requests related to mails.
    /// </summary>
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Handles the HTTP POST request to create a new mail.
        /// </summary>
        /// <param name="mailModel">The MailModel instance to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult that can be converted to an HTTP response message.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewMail([FromBody] MailModel mailModel)
        {
            var response = await _mailService.CreateNewMail(mailModel);

            if (response.StatusCode == Domain.Enum.StatusCode.Success)
                return Ok(response.Description);

            return BadRequest(response.Description);
        }

        /// <summary>
        /// Handles the HTTP GET request to get all mails for a specific employee.
        /// </summary>
        /// <param name="employeeModel">he EmployeeModel instance to get mails for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult that can be converted to an HTTP response message.</returns>
        [HttpGet("getMail")]
        public async Task<IActionResult> GetMail(EmployeeModel employeeModel)
        {
            var response = await _mailService.GetMail(employeeModel);

            if (response.StatusCode == Domain.Enum.StatusCode.Success)
                return Ok(response.Data);

            return BadRequest(response.Description);
        }
    }
}
