namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AcademicYearController(IRepository _repository, IAcademicYearService _academicYearService) : BaseController
{
	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] AcademicYearCreateDto dto)
	{
		if (dto == null)
		{
			return BadRequest("Dto cannot be null.");
		}

		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();
		
		try
		{
			var entity = await _academicYearService.CreateAcademicYearAsync(dto, applicationUserId);

			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
		}
		catch(Exception ex)
		{
			return BadRequest($"An error occurred while saving the academic year.\n{ex.InnerException?.Message ?? ex.Message}");
		}
	}


	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();

		var academicYear = await _repository.GetByIdAsync<AcademicYear, AcademicYearDto>(id, applicationUserId);

		if (academicYear == null)
		{
			return NotFound($"Academic year with ID {id} not found.");
		}
		return Ok(academicYear);
	}
}
