using Microsoft.EntityFrameworkCore;
using RegistrationLetters.DAL.Interfaces;
using RegistrationLetters.Domain.Entities;

namespace RegistrationLetters.DAL.Repositories
{
    /// <summary>
    /// This class provides the CRUD operations for the MailEntity instances in the database.
    /// </summary>
    public class MailRepository : IBaseRepositories<MailEntity>
    {
        private readonly AppDbContext _appDbContext;

        public MailRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Create(MailEntity entity)
        {
            await _appDbContext.Mail.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task Delete(MailEntity entity)
        {
            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<MailEntity> Get()
        {
            return _appDbContext.Mail.AsNoTracking();
        }

        public async Task<MailEntity> Update(MailEntity entity)
        {
            _appDbContext.Mail.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
