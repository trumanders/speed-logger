namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController(IRepository _repository, IStudentRepository _studentRepository) : BaseController
{
	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] StudentCreateDto dto)
	{
		if (dto == null)
		{
			return BadRequest("StudentCreateDto cannot be null.");
		}

		if (ApplicationUserId == null)
			return Unauthorized();

		var entity = await _repository.AddAsync<Student, StudentCreateDto>(dto);
		entity.ApplicationUserId = ApplicationUserId;


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
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();

		var students = await _repository.GetAllAsync<Student, StudentDto>(applicationUserId);
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
        var applicationUserId = ApplicationUserId;

        if (applicationUserId == null)
			return Unauthorized();

		var student = await _repository.GetByIdAsync<Student, StudentDto>(id, applicationUserId);
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
        var applicationUserId = ApplicationUserId;

        if (applicationUserId == null)
			return Unauthorized();

		var students = await _studentRepository.GetByAcademicYearIdAsync(academicYearId, applicationUserId);
		
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
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();
								
		try
		{
			if (!await _repository.DeleteAsync<Student>(id, applicationUserId))
				return NotFound($"Student with ID {id} not found.");

			if (await _repository.SaveChangesAsync())
				return NoContent();
		}
		catch (Exception)
		{
			return BadRequest($"An error occurred while deleting student.");
		}
		return BadRequest();
	}

	// UPDATE
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto)
	{
		if (dto == null)
			return BadRequest("DTO is null");

		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();

		try
		{
			if (!await _repository.AnyAsync<Student>(e => e.Id.Equals(id) && e.ApplicationUserId == applicationUserId))
				return NotFound();

			if (await _repository.Update<Student, StudentUpdateDto>(id, dto, applicationUserId))
			{
				if (await _repository.SaveChangesAsync())
					return NoContent();
			}
		}
		catch (Exception ex)
		{
			return BadRequest($"An error occurred while updating student with ID {id}.\n{ex.InnerException?.Message ?? ex.Message}");
		}

		return BadRequest($"An error occurred while updating student with ID {id}.");
	}
}
