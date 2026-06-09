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

	[HttpGet("{studentId}/has-multiple-logs")]
	public async Task<IActionResult> HasMultipleLogs(int studentId)
	{
		// Antingen räknar du (Count > 1) eller kollar om det finns NÅGON mer logg utöver den man står i
		var hasMultiple = await _logService.HasMultipleLogsAsync(studentId);
		return Ok(hasMultiple); // Returnerar true eller false
	}

	// ADD
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] LogCreateDto dto)
	{
		if (dto == null)
			return BadRequest("LogCreateDto cannot be null.");
		try
		{
			var applicationUserId = ApplicationUserId;
			if (applicationUserId == null)
				return Unauthorized();
			var result = await _logService.CreateLogAsync(dto, applicationUserId);
			if (!result.IsSuccess)
				return BadRequest(result.ErrorCode);
			return CreatedAtAction(nameof(GetById), new { id = result.Value?.Id }, result.Value);
		}
		catch(InvalidOperationException ex)
		{
			return BadRequest($"{ex.Message}");
		}
		catch
		{
			return BadRequest();
		}
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
