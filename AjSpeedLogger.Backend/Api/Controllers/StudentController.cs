namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController(IRepository _repository, IMapper _mapper) : BaseController
{
	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] StudentCreateDto dto)
	{
		if (dto == null)
			return BadRequest("StudentCreateDto cannot be null.");
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		var entity = await _repository.AddAsync<Student>(_mapper.Map<Student>(dto));
		entity.ApplicationUserId = applicationUserId;
		try
		{
			if (await _repository.SaveChangesAsync())
			{
				var studentDto = _mapper.Map<StudentDto>(entity);
				return CreatedAtAction(nameof(GetById), new { id = entity.Id }, studentDto);
			}
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
		var studentEntities = await _repository.GetAllAsync<Student>(s => s.ApplicationUserId == applicationUserId);
		if (studentEntities == null)
			return NotFound($"Students not found.");
		var studentDtos = _mapper.Map<List<StudentDto>>(studentEntities);
		return Ok(studentDtos);
	}


	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
        var applicationUserId = ApplicationUserId;
        if (applicationUserId == null)
			return Unauthorized();
		var studentEntities = await _repository.GetByIdAsync<Student>(s => s.Id == id && s.ApplicationUserId == applicationUserId);
		if (studentEntities == null)
			return NotFound($"Student year with ID {id} not found.");
		var studentDtos = _mapper.Map<StudentDto>(studentEntities);
		return Ok(studentDtos);
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
			if (!await _repository.DeleteAsync<Student>(s => s.Id == id && s.ApplicationUserId == applicationUserId))
				return NotFound($"Student with ID {id} not found.");
			if (await _repository.SaveChangesAsync())
				return NoContent();
		}
		catch (Exception ex)
		{
			return BadRequest($"An error occurred while deleting student: {ex.Message}");
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
			if (await _repository.Update<Student>(s => s.Id == id && s.ApplicationUserId == applicationUserId))
				if (await _repository.SaveChangesAsync())
					return NoContent();
		}
		catch (Exception ex)
		{
			return BadRequest($"An error occurred while updating student with ID {id}.\n{ex.InnerException?.Message ?? ex.Message}");
		}
		return BadRequest($"An error occurred while updating student with ID {id}.");
	}
}
