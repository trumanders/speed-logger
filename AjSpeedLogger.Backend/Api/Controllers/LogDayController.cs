namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LogDayController(ILogDayService _logDayService) : BaseController
{
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateLogDayContent(int id, [FromBody] string logDayContent)
	{
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();

		if (!await _logDayService.UpdateLogDayContentAsync
			(id, logDayContent, applicationUserId))
		{
			return BadRequest();
		}

		return NoContent();
	}
}