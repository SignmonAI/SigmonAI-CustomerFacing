using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using core.Contexts;
using core.Data;
using core.Data.Contexts;
using core.Middlewares;
using core.Models;
using core.Repositories;
using core.Services;
using core.Services.Fixtures;
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

            // app.UseHttpsRedirection();

            app.UseExceptionHandler();

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(
                IServiceCollection services,
                ConfigurationManager configuration)
        {
            // Database context configuration
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<SigmonDbContext>(
                options => options.UseSqlServer(connectionString)
            );

            // JWT authentication configuration
            var jwtSettings = new JwtSettings()
            {
                SecretKey = configuration.GetSection("JwtSettings")
                        .GetValue<string>("SecretKey")!
            };
            services.AddSingleton(jwtSettings);
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddScoped<JwtService>();

            // AutoMapper configuration
            services.AddAutoMapper(typeof(MappingProfile));

            // Repositories configuration
            services.AddScoped<UserRepository>();
            services.AddScoped<CountryRepository>();

            // Services layer registration
            services.AddScoped<UserService>();
            services.AddScoped<CountryService>();
            services.AddScoped<JwtService>();
            services.AddScoped<LoginService>();
            services.AddScoped<UserContext>();

            // Fixtures configuration
            services.AddTransient<IFixture<Tier>>(sp =>
                    new BaseFixture<Tier>(
                        sp.GetRequiredService<TierRepository>(),
                        generateInDatabase: true));

            // Middlewares registration
            services.AddTransient<AuthenticationMiddleware>();

            // Exception handling configuration
            services.AddExceptionHandler<ErrorHandlingMiddleware>();
            services.AddProblemDetails();

            // Endpoint configuration
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}