
namespace Persistence.Interfaces
{
	public interface IRepository
	{
		// GET
		public Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, IEntity;

		public Task<TEntity?> GetByIdAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, IEntity;

		public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, IEntity;


		// ADD
		public Task<TEntity> AddAsync<TEntity>(TEntity entity)
			where TEntity : class, IEntity;

		public Task AddRangeAsync<TEntity>(ICollection<TEntity> entities)
			where TEntity : class, IEntity;


		// DELETE
		public Task<bool> DeleteAsync<TEntity>(int id, string applicationUserId)
			where TEntity : class, IEntity, IUserOwned;

		// UPDATE
		public Task<bool> Update<TEntity, TDto>(int id, TDto dto, string applicationUserId)
			where TEntity : class, IEntity, IUserOwned
			where TDto : class;


		// Save changes
		public Task<bool> SaveChangesAsync();
	}
}
