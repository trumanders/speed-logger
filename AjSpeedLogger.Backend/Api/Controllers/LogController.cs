namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LogController(IRepository _repository, ILogService _logService, IMapper _mapper) : BaseController
{
	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();

		var logEntity = await _repository.GetByIdAsync<Log>(id, applicationUserId);

		if (logEntity == null)
			return NotFound($"Log with ID {id} not found.");
		
		var logDto = _mapper.Map<LogDto>(logEntity);

		return Ok(logDto);
	}

	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] LogCreateDto dto)
	{
		if (dto == null)
			return BadRequest("LogCreateDto cannot be null.");

		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();

		var logDto = await _logService.CreateLogAsync(dto, applicationUserId);

		return CreatedAtAction(nameof(GetById), new { id = logDto.Id }, logDto);
	}
}
