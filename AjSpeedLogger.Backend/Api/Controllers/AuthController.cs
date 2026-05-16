using Common.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Persistence.Entities;
using Persistence.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IConfiguration _configuration;

	private readonly IRepository _repository;

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
		{
			return BadRequest(result.Errors);
		}

		return Ok("User registered successfully.");
	}


	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LogInDto dto)
	{
		var user = await _userManager.FindByEmailAsync(dto.Email);
		
		if (user == null)
			return Unauthorized("Invalid email or password");

		var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

		if (!result.Succeeded)
			return Unauthorized("Invalid username or password.");

		var token = GenrateJwtToken(user);
		
		return Ok("User logged in successfully.");
	}


	private string GenrateJwtToken(ApplicationUser user)
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
