using RegistrationLetters.Domain.Models;
using RegistrationLetters.Domain.Response;

namespace RegistrationLetters.Services.Interfaces
{
    /// <summary>
    /// The IMailService interface defines the operations for managing mails.
    /// </summary>
    public interface IMailService
    {
        Task<IBaseResponse<IEnumerable<MailModel>>> CreateNewMail(MailModel mailModel);

        Task<IBaseResponse<IEnumerable<MailModel>>> GetMail(EmployeeModel employeeModel);
    }
}
