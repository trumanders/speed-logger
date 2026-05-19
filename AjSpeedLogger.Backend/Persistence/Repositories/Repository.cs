namespace Persistence.Repositories;

public class Repository(Context _db, IMapper _mapper) : IRepository
{
	
	// GET
	public async Task<List<TDto>> GetAllAsync<TEntity, TDto>()
		where TEntity : class, IEntity
		where TDto : class
	{
		var entities = await _db.Set<TEntity>().ToListAsync();
		return _mapper.Map<List<TDto>>(entities);
	}

	public async Task<List<TDto>> GetAllAsync<TEntity, TDto>(string applicationUserId)
		where TEntity : class, IEntity, IUserOwned
		where TDto : class
	{
		var entities = await _db.Set<TEntity>()
			.Where(x => x.ApplicationUserId == applicationUserId)
			.ToListAsync();
		return _mapper.Map<List<TDto>>(entities);
	}

	public async Task<TDto> GetByIdAsync<TEntity, TDto>(int id, string applicationUserId)
		where TEntity : class, IEntity, IUserOwned
		where TDto : class
	{
		var entity = await _db.Set<TEntity>()
			.FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId && x.Id == id);
		return _mapper.Map<TDto>(entity);
	}


	public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
		where TEntity : class, IEntity
	{
		return await _db.Set<TEntity>().AnyAsync(predicate);
	}


	// ADD
	public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
		where TEntity : class, IEntity
		where TDto : class
	{
		var entity = _mapper.Map<TEntity>(dto);
		await _db.Set<TEntity>().AddAsync(entity);
		return entity;		
	}

	// DELETE
	public async Task<bool> DeleteAsync<TEntity>(int id, string applicationUserId)
		where TEntity : class, IEntity, IUserOwned
	{
		try
		{
			var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(x => x.ApplicationUserId == applicationUserId && x.Id == id);
			
			if (entity == null)
				return false;

			_db.Set<TEntity>().Remove(entity);
			return true;
		}
		catch
		{
			return false;
		}					
	}


	// Save changes
	public async Task<bool> SaveChangesAsync()
	{
		return await _db.SaveChangesAsync() > 0;
	}
}
