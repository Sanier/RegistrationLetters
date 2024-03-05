using Microsoft.EntityFrameworkCore;
using RegistrationLetters.DAL.Interfaces;
using RegistrationLetters.Domain.Entities;

namespace RegistrationLetters.DAL.Repositories
{
    /// <summary>
    /// This class provides the CRUD operations for the EmployeeEntity instances in the database.
    /// </summary>
    public class EmployeeRepository : IBaseRepositories<EmployeeEntity>
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Create(EmployeeEntity entity)
        {
            await _appDbContext.Employee.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task Delete(EmployeeEntity entity)
        {
            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<EmployeeEntity> Get()
        {
            return _appDbContext.Employee.AsNoTracking();
        }

        public async Task<EmployeeEntity> Update(EmployeeEntity entity)
        {
            _appDbContext.Employee.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
