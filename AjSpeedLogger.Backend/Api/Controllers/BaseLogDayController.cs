namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseLogDayController(IBaseLogDayService _baseLogDayService) : BaseController
{
	[HttpPut]
	public async Task<IActionResult> UpdateLogDayContent(int baseLogDayId, [FromBody] string newBaseLogDayContent)
	{
		var applicationUserId = ApplicationUserId;

		if (applicationUserId == null)
			return Unauthorized();

		if (!await _baseLogDayService.UpdateBaseLogDayContentAsync
			(baseLogDayId, newBaseLogDayContent, applicationUserId))
		{
			return BadRequest();
		}

		return NoContent();
	}
}