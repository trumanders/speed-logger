using Common;
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

		// ADD
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AcademicYearDto dto)
		{
			if (dto == null)
			{
				return BadRequest("AcademicYearDto cannot be null.");
			}

			var entity = await _repository.AddAsync<AcademicYear, AcademicYearDto>(dto);

			try
			{
				if (await _repository.SaveChangesAsync())
					return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
			}
			catch
			{
				return BadRequest("An error occurred while saving the academic year.");
			}
			return BadRequest();

		}


		// GET by ID
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
