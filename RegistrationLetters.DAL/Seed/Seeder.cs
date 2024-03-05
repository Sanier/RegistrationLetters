using AutoFixture;
using RegistrationLetters.Domain.Entities;

namespace RegistrationLetters.DAL.Seed
{
    /// <summary>
    /// Seeder class is used for populating the database with initial data.
    /// </summary>
    public static class Seeder
    {
        /// <summary>
        /// The Seed method checks if there is already data in the database, and if not, it adds new data.
        /// </summary>
        /// <param name="appDbContext">The application's database context.</param>
        public static void Seed(this AppDbContext appDbContext)
        {
            //https://www.twilio.com/en-us/blog/containerize-your-sql-server-with-docker-and-aspnet-core-with-ef-core
            if (!(appDbContext.Mail.Any() && appDbContext.Employee.Any()))
            {
                Fixture fixture = new Fixture();

                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                fixture.Customize<MailEntity>(mail => mail.Without(m => m.Id));
                fixture.Customize<EmployeeEntity>(employee => employee.Without(e => e.Id));

                //--- The next two lines add 20 rows to your database
                var mail = fixture.CreateMany<MailEntity>(20).ToList();
                var employees = fixture.CreateMany<EmployeeEntity>(20).ToList();

                appDbContext.AddRange(employees);
                appDbContext.AddRange(mail);

                appDbContext.SaveChanges();
            }
        }
    }
}
