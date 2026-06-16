using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LogDayController(ILogDayService _logDayService) : BaseController
{
	[HttpGet]
	[Route("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();

		var logDayDto = await _logDayService.GetByIdAsync(id, applicationUserId);
		if (logDayDto == null)
			return NotFound($"Log entry with ID {id} not found.");
		return Ok(logDayDto);
	}

	[HttpGet]
	[Route("logId/{logId}")]
	public async Task<IActionResult> GetByLogId(int logId)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();

		var logDayDtos = await _logDayService.GetByLogIdAsync(logId, applicationUserId);
		if (logDayDtos == null)
			return NotFound($"Log entries for log ID {logId} not found.");
		return Ok(logDayDtos);
	}

	[HttpGet]
	[Route("baseLogDayId/{baseLogDayId}")]
	public async Task<IActionResult> GetByBaseLogDayId(int baseLogDayId)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();

		var logDayDto = await _logDayService.GetByBaseLogDayIdAsync(baseLogDayId, applicationUserId);
		
		return Ok(logDayDto);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] LogDayCreateDto logDayCreateDto)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		try
		{
			var newLogDayDto = await _logDayService.CreateLogDay(logDayCreateDto);
			return CreatedAtAction(nameof(GetById), new { id = newLogDayDto.Id }, newLogDayDto);
		}
		catch (Exception ex)
		{
			return BadRequest($"An error occurred while saving the log entry.\n{ex.InnerException?.Message ?? ex.ToString()}");
		}
	}

	[HttpPut("{logId}/content")]
	public async Task<IActionResult> UpdateLogDayContent(int logId, int baseLogDayId, [FromBody] string logDayContent)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		if (!await _logDayService.SaveLogDayContentAsync(logId, baseLogDayId, logDayContent, applicationUserId))
			return BadRequest();
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteLogDay(int id)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		if (!await _logDayService.DeleteAsync(id, applicationUserId))
			return BadRequest();
		return NoContent();
	}
}