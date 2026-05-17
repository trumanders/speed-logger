using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
	public class Repository : IRepository
	{
		private readonly Context _db;
		private readonly IMapper _mapper;

		public Repository(Context db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		
		// Get
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


		// Add
		public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
			where TEntity : class, IEntity
			where TDto : class
		{
			var entity = _mapper.Map<TEntity>(dto);
			await _db.Set<TEntity>().AddAsync(entity);
			return entity;		
		}

		// Update

		// Delete


		// Save changes
		public async Task<bool> SaveChangesAsync()
		{
			return await _db.SaveChangesAsync() > 0;
		}
	}
}
