namespace Api;

public class Program
{
	public static void Main(string[] args)
	{
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // To prevent default claim mapping
		JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear(); // To prevent default claim mapping

		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllers()
			.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
		builder.Services.AddDbContext<Context>(options =>
			options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseSessionPoolerConnection")));
			//options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));
		
		builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
		builder.Services.AddScoped<IBaseLogDayService, BaseLogDayService>();
		builder.Services.AddScoped<IContext, Context>();
		builder.Services.AddScoped<ILogDayService, LogDayService>();
		builder.Services.AddScoped<ILogService, LogService>();
		builder.Services.AddScoped<IRepository, Repository>();
		builder.Services.AddScoped<IStudentService, StudentService>();

		builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
		{
			options.User.RequireUniqueEmail = true;
		})
			.AddEntityFrameworkStores<Context>()
			.AddDefaultTokenProviders();
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
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

		// Configure CORS
		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins("http://localhost:5173", "https://speedlogger.netlify.app") // React app URL
					  .AllowAnyHeader()
					  .AllowAnyMethod();
			});
		});

		var app = builder.Build();
		app.UseCors();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseHttpsRedirection();
		app.MapControllers();
		app.Run();
	}
}
