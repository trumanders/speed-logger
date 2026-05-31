namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AcademicYearController(IAcademicYearService _academicYearService) : BaseController
{
	[HttpGet("/all-user-data/{id}")]
	public async Task<IActionResult> GetAcademicYearAllUserData(int academicYearId)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		var allUserData = await _academicYearService.GetAcademicYearAllUserDataAsync(academicYearId);
		if (allUserData == null)
			return NotFound();
		return Ok(allUserData);
	}

	// GET ALL (names only)
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		var academicYears = await _academicYearService.GetAllNamesAsync(applicationUserId);
		return Ok(academicYears);
	}

	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		var academicYearDto = await _academicYearService.GetByIdAsync(id, applicationUserId);
		if (academicYearDto == null)
			return NotFound($"Academic year with ID {id} not found.");
		return Ok(academicYearDto);
	}

	// GET by Studnet Id
	[HttpGet("/student/{studentId}")]
	public async Task<IActionResult> GetByStudentId(int studentId)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		var academicYearDtos = await _academicYearService.GetAllNamesByStudentIdAsync(studentId, applicationUserId);
		if (academicYearDtos == null || !academicYearDtos.Any())
			return NotFound($"Academic years for student with ID {studentId} not found.");
		return Ok(academicYearDtos);
	}

	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] AcademicYearCreateDto createDto)
	{
		if (createDto == null)
			return BadRequest("Dto cannot be null.");
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		try
		{
			var academicYearDto = await _academicYearService.CreateAcademicYearAsync(createDto, applicationUserId);
			return CreatedAtAction(nameof(GetById), new { id = academicYearDto.Id }, academicYearDto);
		}
		catch(Exception ex)
		{
			return BadRequest($"An error occurred while saving the academic year.\n{ex.InnerException?.Message ?? ex.Message}");
		}
	}

	// DELETE
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		try
		{
			if (!await _academicYearService.DeleteAsync(id, applicationUserId))
				return NotFound($"Academic year with ID {id} not found.");
			return NoContent();
		}
		catch (Exception)
		{
			return BadRequest($"An error occurred while deleting student.");
		}
	}
}
