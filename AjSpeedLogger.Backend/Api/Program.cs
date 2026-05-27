namespace Api;

public class Program
{
	public static void Main(string[] args)
	{
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // To prevent default claim mapping
		JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear(); // To prevent default claim mapping

		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllers();
		builder.Services.AddDbContext<Context>(options =>
			options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"),
			npgsqlOptions => npgsqlOptions.EnableRetryOnFailure())
			);
		builder.Services.AddScoped<IRepository, Repository>();
		builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
		builder.Services.AddScoped<ILogService, LogService>();
		builder.Services.AddScoped<ILogDayService, LogDayService>();
		builder.Services.AddScoped<IBaseLogDayService, BaseLogDayService>();
		builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<Context>()
			.AddDefaultTokenProviders();
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
				};
			});
		builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
		builder.Services.AddSwaggerGen(c =>
		{
			c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = Microsoft.OpenApi.Models.ParameterLocation.Header
			});
			c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
			{
				{
					new Microsoft.OpenApi.Models.OpenApiSecurityScheme
					{
						Reference = new Microsoft.OpenApi.Models.OpenApiReference
						{
							Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});
		});

		// Configure CORS
		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins("http://localhost:5173") // React app URL
					  .AllowAnyHeader()
					  .AllowAnyMethod();
			});
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseCors();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseHttpsRedirection();
		app.MapControllers();
		app.Run();
	}
}
