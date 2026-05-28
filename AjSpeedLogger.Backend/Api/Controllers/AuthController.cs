using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IConfiguration _configuration;
	private readonly string frontendUrl = "http://localhost:5173/reset-password";

	public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto dto)
	{
		var user = new ApplicationUser
		{
			UserName = dto.Email,
			Email = dto.Email
		};

		var result = await _userManager.CreateAsync(user, dto.Password);
		if (!result.Succeeded)
			return BadRequest(result.Errors);
		return Ok("User registered successfully.");
	}


	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LogInDto dto)
	{
		try
		{
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Fel epost eller lösenord.");
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Fel epost eller lösenord.");
            var token = GenerateJwtToken(user);
			return Ok(new
			{
				token = token,
				username = user.UserName
			});
		}
		catch(Exception ex)
		{
			return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
        }
    }

	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		return Ok();
	}

	[HttpPost("forgot-password")]
	public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
	{
		var resetLinkSentMessage = $"En återställningslänk är skickad till {dto.Email}, om epostadressen existerar";
		var user = await _userManager.FindByEmailAsync(dto.Email);
		if (user == null)
			return Ok(resetLinkSentMessage);
		var token = await _userManager.GeneratePasswordResetTokenAsync(user);
		var resetLink = $"{frontendUrl}?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email!)}";
		Console.WriteLine(resetLink);
		return Ok(resetLinkSentMessage);
	}

	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
	{
		var user = await _userManager.FindByEmailAsync(dto.Email);
		if (user == null)
			return BadRequest("Återställningen av lösenord misslyckades");

		var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
		if (!result.Succeeded)
			return BadRequest(result.Errors);
		return Ok("Lösenordet har återställts.");
	}


	private string GenerateJwtToken(ApplicationUser user)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Email, user.Email!)
		};

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(8),
			signingCredentials: credentials
		);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
