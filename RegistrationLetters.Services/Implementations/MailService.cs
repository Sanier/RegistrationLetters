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
    /// This class provides the operations for managing mails.
    /// </summary>
    public class MailService : IMailService
    {
        private readonly IBaseRepositories<MailEntity> _mailRepository;
        private readonly IBaseRepositories<EmployeeEntity> _employeeRepository;
        public MailService(IBaseRepositories<MailEntity> mailRepository, IBaseRepositories<EmployeeEntity> employeeRepository)
        {
            _mailRepository = mailRepository;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Asynchronously creates a new MailModel instance.
        /// </summary>
        /// <param name="mailModel">The MailModel instance to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a base response with a collection of MailModel.</returns>
        public async Task<IBaseResponse<IEnumerable<MailModel>>> CreateNewMail(MailModel mailModel)
        {
            try
            {
                if (string.IsNullOrEmpty(mailModel.Title) && string.IsNullOrEmpty(mailModel.Content))
                    throw new ArgumentNullException("The letter must not be empty");

                //Then you should put it in a separate method
                var sender = await _employeeRepository.Get()
                    .FirstOrDefaultAsync(s => s.FirstName == mailModel.Sender.FirstName
                                           && s.LastName == mailModel.Sender.LastName);

                //Then you should put it in a separate method
                var adressee = await _employeeRepository.Get()
                    .FirstOrDefaultAsync(s => s.FirstName == mailModel.Addressee.FirstName
                                           && s.LastName == mailModel.Addressee.LastName);

                if ((string.IsNullOrEmpty(adressee.FirstName) && string.IsNullOrEmpty(adressee.LastName))
                    || (string.IsNullOrEmpty(sender.FirstName) && string.IsNullOrEmpty(sender.LastName)))
                    throw new ArgumentNullException("Employee is not found");

                var mail = new MailEntity()
                {
                    Title = mailModel.Title,
                    Content = mailModel.Content,
                    Date = mailModel.Date.Day.ToString(),
                    AddresseeId = adressee.Id,
                    SenderId = sender.Id,
                };

                await _mailRepository.Create(mail);
                await _mailRepository.Update(mail);

                return new BaseResponse<IEnumerable<MailModel>>()
                {
                    Description = $"{"Сообщение было успешно отправлено"}",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<MailModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Asynchronously gets all MailModel instances for a specific employee.
        /// In the future it is worth adding a new model for entering information
        /// </summary>
        /// <param name="employeeModel">The EmployeeModel instance to get mails for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a base response with a collection of MailModel.</returns>
        public async Task<IBaseResponse<IEnumerable<MailModel>>> GetMail(EmployeeModel employeeModel)
        {
            try
            {
                //It’s worth changing it to your full name later
                //Then you should put it in a separate method
                var adressee = await _employeeRepository.Get()
                    .Where(e => (e.Id == employeeModel.Id)
                    || (e.FirstName == employeeModel.FirstName
                    && e.LastName == employeeModel.LastName))
                    .Select(e => new EmployeeModel
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobPosition = e.JobPosition
                    })
                    .ToListAsync();

                //The operations below will take a long time to complete, but for now they are working demo
                var senderId = await _mailRepository.Get()
                    .FirstOrDefaultAsync(e => e.Id == adressee[0].Id);

                //Then you should put it in a separate method
                var sender = await _employeeRepository.Get()
                    .Where(s => s.Id == senderId.SenderId)
                    .Select(s => new EmployeeModel
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        JobPosition = s.JobPosition
                    })
                    .ToListAsync();

                if (adressee == null || sender == null)
                    throw new ArgumentNullException("Employee is not found");

                var mail = await _mailRepository.Get()
                    .Where(a => a.AddresseeId == adressee[0].Id)
                    .Select(a => new MailModel
                    {
                        Title = a.Title,
                        Content = a.Content,
                        Addressee = adressee[0],
                        Sender = sender[0]
                    })
                    .ToListAsync();

                if (!mail.Any())
                    throw new ArgumentNullException("Employee is not found");

                return new BaseResponse<IEnumerable<MailModel>>()
                {
                    //This will need to be corrected, because... it eats up memory
                    Data = mail,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<MailModel>>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
