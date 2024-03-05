namespace RegistrationLetters.DAL.Interfaces
{
    /// <summary>
    /// The IBaseRepositories<T> interface defines the basic CRUD operations for a repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IBaseRepositories<T>
    {
        Task<bool> Create(T entity);
        Task Delete(T entity);
        IQueryable<T> Get();
        Task<T> Update(T entity);
    }
}
