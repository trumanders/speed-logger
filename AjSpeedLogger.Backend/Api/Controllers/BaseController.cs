namespace Api.Controllers;

public class BaseController : ControllerBase
{
    public string ApplicationUserId =>
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
}
