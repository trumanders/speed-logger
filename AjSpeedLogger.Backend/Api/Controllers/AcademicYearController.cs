using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using Persistence.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AcademicYearController : ControllerBase
	{
		private readonly IRepository _repository;

		public AcademicYearController(IRepository repository)
		{
			_repository = repository;
		}

		// ADD
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AcademicYearDto dto)
		{
			if (dto == null)
			{
				return BadRequest("AcademicYearDto cannot be null.");
			}

			var entity = await _repository.AddAsync<AcademicYear, AcademicYearDto>(dto);

			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			if (userId == null)
				return Unauthorized();

			entity.ApplicationUserId = userId;


			try
			{
				if (await _repository.SaveChangesAsync())
					return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
			}
			catch(Exception ex)
			{
				return BadRequest($"An error occurred while saving the academic year.\n{ex.InnerException?.Message ?? ex.Message}");
			}
			return BadRequest();

		}


		// GET by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var academicYears = await _repository.GetAllAsync<AcademicYear, AcademicYearDto>();
			var academicYear = academicYears.FirstOrDefault(ay => ay.Id == id);
			if (academicYear == null)
			{
				return NotFound($"Academic year with ID {id} not found.");
			}
			return Ok(academicYear);
		}

	}
}
