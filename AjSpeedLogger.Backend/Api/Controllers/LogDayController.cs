namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LogDayController(ILogDayService _logDayService) : BaseController
{
	[HttpPut]
	public async Task<IActionResult> UpdateLogDayContent(int logId, int baseLogDayId, [FromBody] string logDayContent)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		if (!await _logDayService.SaveLogDayContentAsync(logId, baseLogDayId, logDayContent, applicationUserId))
			return BadRequest();
		return NoContent();
	}
}