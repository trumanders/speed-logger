using System.Linq.Expressions;

namespace Persistence.Interfaces
{
	public interface IRepository
	{
		public Task<List<TDto>> GetAllAsync<TEntity, TDto>()
			where TEntity : class, IEntity
			where TDto : class;

		public Task<List<TDto>> GetAllAsync<TEntity, TDto>(string applicationUserId)
			where TEntity : class, IEntity, IUserOwned
			where TDto : class;

		public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
			where TEntity : class, IEntity;

		public Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
			where TEntity : class, IEntity
			where TDto : class;



		// Save changes
		public Task<bool> SaveChangesAsync();
	}
}
