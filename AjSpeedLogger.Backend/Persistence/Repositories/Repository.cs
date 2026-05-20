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
			.Where(e => e.ApplicationUserId == applicationUserId)
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

	// ADD RANGE
	public async Task AddRangeAsync<TEntity>(ICollection<TEntity> entities)
	where TEntity : class, IEntity
	{
		await _db.Set<TEntity>().AddRangeAsync(entities);
	}

	// DELETE
	public async Task<bool> DeleteAsync<TEntity>(int id, string applicationUserId)
		where TEntity : class, IEntity, IUserOwned
	{
		var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(x => x.ApplicationUserId == applicationUserId && x.Id == id);
		
		if (entity == null)
			return false;
		
		_db.Set<TEntity>().Remove(entity);
		return true;
	}

	// UPDATE
	public async Task<bool> Update<TEntity, TDto>(int id, TDto dto, string applicationUserId)
	where TEntity : class, IEntity, IUserOwned
	where TDto : class
	{
		// Get the existing entity because we only change the properties that are in the DTO, not the whole entity
		var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id && e.ApplicationUserId == applicationUserId);

		if (entity == null)
			return false;

		_mapper.Map(dto, entity); // Copies the properties from the DTO to the entity
		_db.Set<TEntity>().Update(entity);
		return true;
	}


	// Save changes
	public async Task<bool> SaveChangesAsync()
	{
		return await _db.SaveChangesAsync() > 0;
	}
}
