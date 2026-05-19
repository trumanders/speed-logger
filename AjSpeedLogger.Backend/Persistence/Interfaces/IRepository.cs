
namespace Persistence.Interfaces
{
	public interface IRepository
	{
		// GET
		public Task<List<TDto>> GetAllAsync<TEntity, TDto>()
			where TEntity : class, IEntity
			where TDto : class;

		public Task<List<TDto>> GetAllAsync<TEntity, TDto>(string applicationUserId)
			where TEntity : class, IEntity, IUserOwned
			where TDto : class;

		public Task<TDto> GetByIdAsync<TEntity, TDto>(int id, string applicationUserId)
			where TEntity : class, IEntity, IUserOwned
			where TDto : class;

		public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, IEntity;


		// ADD
		public Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
			where TEntity : class, IEntity
			where TDto : class;

		// DELETE
		public Task<bool> DeleteAsync<TEntity>(int id, string applicationUserId)
			where TEntity : class, IEntity, IUserOwned;



		// Save changes
		public Task<bool> SaveChangesAsync();
	}
}
