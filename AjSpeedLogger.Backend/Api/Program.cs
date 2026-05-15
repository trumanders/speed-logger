using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;
using Persistence.Repositories;

using Persistence.Entities;
using Common;
namespace Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<Context>(options =>
				options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

			builder.Services.AddScoped<IRepository, Repository>();

			ConfigureAutoMapper(builder.Services);

			builder.Services.AddOpenApi();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}

		private static void ConfigureAutoMapper(IServiceCollection services)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AcademicYear, AcademicYearDto>();
			}, null);

			var mapper = config.CreateMapper();
			services.AddSingleton(mapper);
		}
	}
}
