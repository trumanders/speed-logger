
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController : ControllerBase
{
	private readonly IRepository _repository;
	private readonly IStudentRepository _studentRepository;

	public StudentController(IRepository repository)
	{
		_repository = repository;
	}

	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] StudentCreateDto dto)
	{
		if (dto == null)
		{
			return BadRequest("StudentCreateDto cannot be null.");
		}

		var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

		if (userId == null)
			return Unauthorized();

		var entity = await _repository.AddAsync<Student, StudentCreateDto>(dto);
		entity.ApplicationUserId = userId;


		try
		{
			if (await _repository.SaveChangesAsync())
				return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.Id }, dto);
		}
		catch (Exception ex)
		{
			return BadRequest($"An error occurred while saving student.\n{ex.InnerException?.Message ?? ex.Message}");
		}
		return BadRequest();

	}

	// GET ALL (by User)
	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

		if (userId == null)
			return Unauthorized();

		var students = await _repository.GetAllAsync<Student, StudentDto>(userId);
		if (students == null)
		{
			return NotFound($"Students not found.");
		}
		return Ok(students);
	}


	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

		if (userId == null)
			return Unauthorized();

		var student = await _repository.GetByIdAsync<Student, StudentDto>(id, userId);
		if (student == null)
		{
			return NotFound($"Student year with ID {id} not found.");
		}
		return Ok(student);
	}

	// GET by AcademicYearId
	[HttpGet("byAcademicYear/{academicYearId}")]
	public async Task<IActionResult> GetByAcademicYearIdAsync(int academicYearId)
	{
		var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

		if (userId == null)
			return Unauthorized();

		var students = await _studentRepository.GetByAcademicYearIdAsync(academicYearId, userId);
		
		if (students == null || !students.Any())
		{
			return NotFound($"Students for Academic Year ID {academicYearId} not found.");
		}
		
		return Ok(students);
	}




	// DELETE
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

		if (userId == null)
			return Unauthorized();
								
		try
		{
			if (!await _repository.DeleteAsync<Student>(id, userId))
				return NotFound($"Student with ID {id} not found.");

			if (await _repository.SaveChangesAsync())
				return NoContent();
		}
		catch (Exception ex)
		{
			return BadRequest($"An error occurred while deleting student.");
		}
		return BadRequest();
	}
}
