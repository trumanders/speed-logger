namespace Persistence.Repositories;

public class Repository(Context _db) : IRepository
{
	public async Task<TEntity?> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
	{
		return await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);
	}

	public async Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity
	{
		var entities = await _db.Set<TEntity>()
			.Where(predicate)
			.ToListAsync();
		return entities;
	}

	public async Task<TEntity?> GetSingleWithIncludesAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] includes)		
	where TEntity : class, IEntity
	{
		IQueryable<TEntity> query = _db.Set<TEntity>();
		foreach (var include in includes)
		{
			query = query.Include(include);
		}		
		return await query.FirstOrDefaultAsync(predicate);
	}

	public async Task<List<TEntity>> GetWithIncludesAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] includes)
	where TEntity : class, IEntity
	{
		IQueryable<TEntity> query = _db.Set<TEntity>();
		foreach (var include in includes)
		{
			query = query.Include(include);
		}
		return await query.Where(predicate).ToListAsync();
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

	public async Task<bool> IsCountGreaterThan<TEntity>(Expression<Func<TEntity, bool>> predicate, int count)
		where TEntity : class, IEntity
	{
		return await _db.Set<TEntity>().CountAsync(predicate) > count;
	}


	// Save changes
	public async Task<bool> SaveChangesAsync()
	{
		return await _db.SaveChangesAsync() > 0;
	}
}
