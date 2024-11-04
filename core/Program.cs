using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using core.Contexts;
using core.Data;
using core.Data.Contexts;
using core.Middlewares;
using core.Models;
using core.Repositories;
using core.Services;
using core.Services.Factories;
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

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

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

            services.AddScoped<ITierFactory, DefaultTierFactory>();

            // Repositories configuration
            services.AddScoped<UserRepository>();
            services.AddScoped<CountryRepository>();
            services.AddScoped<LanguageRepository>();
            services.AddScoped<SubscriptionRepository>();
            services.AddScoped<TierRepository>();
            services.AddScoped<BillRepository>();

            // Services layer registration
            services.AddScoped<UserService>();
            services.AddScoped<CountryService>();
            services.AddScoped<LanguageService>();
            services.AddScoped<SubscriptionService>();
            services.AddScoped<TierService>();
            services.AddScoped<BillService>();
            services.AddScoped<JwtService>();
            services.AddScoped<LoginService>();
            services.AddScoped<UserContext>();
            services.AddScoped<TierService>();
            services.AddScoped<ClassificationService>();

            // Http Client
            services.AddHttpClient<ClassificationService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Fixtures configuration
            // services.AddTransient<FreeTierFixture>(async sp =>
            //     {
            //         var f = new FreeTierFixture(
            //             sp.GetRequiredService<TierRepository>(),
            //             generateInDatabase: true);

            //         await f.ApplyDefault(new Tier(
            //             modelDescription: "Free",
            //             modelNumber: ClassificationModel.FREE));

            //         return f;
            //     });

            // Middlewares registration
            services.AddScoped<AuthenticationMiddleware>();

            // Exception handling configuration
            services.AddExceptionHandler<ErrorHandlingMiddleware>();
            services.AddProblemDetails();

            // Endpoint configuration
            services.AddCors();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}