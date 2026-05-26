namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LogController(ILogService _logService) : BaseController
{
	// GET by ID
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null) return Unauthorized();

		var logDto = await _logService.GetByIdAsync(id, applicationUserId);

		if (logDto == null)
			return NotFound($"Log with ID {id} not found.");

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

		if (logDto == null)
			return NotFound();

		return CreatedAtAction(nameof(GetById), new { id = logDto.Id }, logDto);
	}

	// DELETE
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();

		if (!await _logService.DeleteLogAsync(id, applicationUserId))
			return NotFound($"Log with ID {id} not found.");
		
		return NoContent();
	}
}
