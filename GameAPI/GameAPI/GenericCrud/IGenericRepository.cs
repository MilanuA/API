namespace GameAPI.GenericCrud;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(string[]? includes = null);
    Task<TEntity?> GetByIdAsync(int id, string[]? includes = null);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
}
