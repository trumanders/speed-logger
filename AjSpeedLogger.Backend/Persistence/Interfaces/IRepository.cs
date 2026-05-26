namespace Persistence.Interfaces;

public interface IRepository
{
	// GET
	public Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity;

	public Task<TEntity?> GetByIdAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] includes)
		where TEntity : class, IEntity;

	public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity;

	// ADD
	public Task<TEntity> AddAsync<TEntity>(TEntity entity)
		where TEntity : class, IEntity;

	public Task AddRangeAsync<TEntity>(ICollection<TEntity> entities)
		where TEntity : class, IEntity;

	// DELETE
	public Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity;

	// UPDATE
	public Task<bool> Update<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity;

	// CHECK IF STUDENT HAS MULTIPLE LOGS
	public Task<bool> IsCountGreaterThan<TEntity>(Expression<Func<TEntity, bool>> predicate, int count)
		where TEntity : class, IEntity;

	// Save changes
	public Task<bool> SaveChangesAsync();
}
