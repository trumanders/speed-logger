namespace Persistence.Repositories;

public class Repository(Context _db, IMapper _mapper) : IRepository
{

	// GET
	public async Task<List<TEntity>> GetAllAsync<TEntity>()
		where TEntity : class, IEntity
	{
		var entities = await _db.Set<TEntity>().ToListAsync();
		return entities;
	}

	public async Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> checkOwner)
		where TEntity : class, IEntity
	{
		var entities = await _db.Set<TEntity>()
			.Where(checkOwner)
			.ToListAsync();

		return entities;
	}

	public async Task<TEntity?> GetByIdAsync<TEntity>(Expression<Func<TEntity, bool>> checkOwner)
	where TEntity : class, IEntity
	{
		var entity = await _db.Set<TEntity>()
			.Where(checkOwner)
			.SingleOrDefaultAsync();

		return entity;
	}


	public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity
	{
		return await _db.Set<TEntity>().AnyAsync(predicate);
	}


	// ADD
	public async Task<TEntity> AddAsync<TEntity>(TEntity entity)
		where TEntity : class, IEntity
	{
		await _db.Set<TEntity>().AddAsync(entity);
		return entity;		
	}

	// ADD RANGE
	public async Task AddRangeAsync<TEntity>(ICollection<TEntity> entities)
	where TEntity : class, IEntity
	{
		await _db.Set<TEntity>().AddRangeAsync(entities);
	}

	// DELETE
	public async Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity
	{
		var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(predicate);
		
		if (entity == null)
			return false;
		
		_db.Set<TEntity>().Remove(entity);
		return true;
	}

	// UPDATE
	public async Task<bool> Update<TEntity>(Expression<Func<TEntity, bool>> predicate)
	where TEntity : class, IEntity
	{
		// Get the existing entity because we only change the properties that are in the DTO, not the whole entity
		var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(predicate);

		if (entity == null)
			return false;

		_db.Set<TEntity>().Update(entity);
		return true;
	}


	// Save changes
	public async Task<bool> SaveChangesAsync()
	{
		return await _db.SaveChangesAsync() > 0;
	}
}
