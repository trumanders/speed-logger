namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AcademicYearController(IRepository _repository, IAcademicYearService _academicYearService, IMapper _mapper) : BaseController
{
	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] AcademicYearCreateDto createDto)
	{
		if (createDto == null)
		{
			return BadRequest("Dto cannot be null.");
		}

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


	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();

		var academicYear = await _repository.GetByIdAsync<AcademicYear>(ay => ay.Id == id && ay.ApplicationUserId == applicationUserId);

		if (academicYear == null)
			return NotFound($"Academic year with ID {id} not found.");

		var academicYearDto = _mapper.Map<AcademicYearDto>(academicYear);

		return Ok(academicYearDto);
	}
}
