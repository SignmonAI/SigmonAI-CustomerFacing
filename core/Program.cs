using AutoMapper;
using core.Contexts;
using core.Middlewares;
using core.Repositories;
using core.Services;
using core.Services.Mappings;
using Microsoft.EntityFrameworkCore;

namespace core
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseExceptionHandler();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<SigmonDbContext>(
                options => options.UseSqlServer(connectionString)
            );

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<UserRepository>();

            services.AddScoped<UserService>();

            services.AddExceptionHandler<ErrorHandlingMiddleware>();
            services.AddProblemDetails();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}