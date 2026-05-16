using Common.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using Persistence.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		private readonly IRepository _repository;

		public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
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
			var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

			if (!result.Succeeded)
				return Unauthorized("Invalid username or password.");
			
			return Ok("User logged in successfully.");
		}
	}
}
