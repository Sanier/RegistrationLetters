using Microsoft.EntityFrameworkCore;
using Moq;
using RegistrationLetters.DAL.Interfaces;
using RegistrationLetters.Domain.Entities;
using RegistrationLetters.Domain.Enum;
using RegistrationLetters.Domain.Models;
using RegistrationLetters.Services.Implementations;

namespace RegistrationLetters.API.Tests.Services
{
    [TestFixture]
    public class MailServiceTests
    {
        private Mock<IBaseRepositories<MailEntity>> _mockMailRepository;
        private Mock<IBaseRepositories<EmployeeEntity>> _mockEmployeeRepository;
        private MailService _mailService;

        [SetUp]
        public void Setup()
        {
            _mockMailRepository = new Mock<IBaseRepositories<MailEntity>>();

            _mockEmployeeRepository = new Mock<IBaseRepositories<EmployeeEntity>>();
            _mailService = new MailService(_mockMailRepository.Object, _mockEmployeeRepository.Object);

            
        }

        [Test]
        public async Task CreateNewMail_ShouldReturnSuccess_WhenMailModelIsValid()
        {
            // Arrange
            var mailModel = new MailModel
            {
                Title = "Test Title",
                Content = "Test Content",
                Sender = new EmployeeModel { FirstName = "John", LastName = "Doe" },
                Addressee = new EmployeeModel { FirstName = "Jane", LastName = "Doe" },
                Date = DateTime.Now
            };

            // Act
            var result = await _mailService.CreateNewMail(mailModel);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCode.Success));
        }

        [Test]
        public async Task GetMail_ShouldReturnMails_WhenEmployeeExists()
        {
            // Arrange
            var employeeModel = new EmployeeModel { Id = 1, FirstName = "John", LastName = "Doe" };

            _mockEmployeeRepository
                .Setup(x => x.Get())
                .Returns(new List<EmployeeEntity>
                {
                    new EmployeeEntity 
                    { 
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe"
                    }
                }
                .AsQueryable());

                _mockMailRepository
                .Setup(x => x.Get())
                .Returns(new List<MailEntity>
                {
                    new MailEntity 
                    { 
                        Title = "Test Title", 
                        Content = "Test Content", 
                        SenderId = 1, 
                        AddresseeId = 2, 
                        Date = DateTime.Now.ToString() 
                    }
                }
                .AsQueryable());

            // Act
            var result = await _mailService.GetMail(employeeModel);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCode.Success));
            Assert.That(result.Data.Count(), Is.EqualTo(1));
        }
    }

}
