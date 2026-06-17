namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseLogDayController(IBaseLogDayService _baseLogDayService) : BaseController
{
	[HttpGet("academicYearId/{academicYearId}")]
	public async Task<IActionResult> GetByAcademicYearId(int academicYearId)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		var baseLogDaysDtos = await _baseLogDayService.GetByAcademicYearIdAsync(academicYearId, applicationUserId);
		if (baseLogDaysDtos == null || !baseLogDaysDtos.Any())
			return NotFound($"Base log for academic year {academicYearId} not found.");
		return Ok(baseLogDaysDtos);
	}


	[HttpPut("{baseLogDayId}/content")]
	public async Task<IActionResult> UpdateLogDayContent(int baseLogDayId, [FromBody] string newContent)
	{
		var applicationUserId = ApplicationUserId;
		if (applicationUserId == null)
			return Unauthorized();
		if (!await _baseLogDayService.UpdateBaseLogDayContentAsync
			(baseLogDayId, newContent, applicationUserId))
			return BadRequest();
		return NoContent();
	}
}