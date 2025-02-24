using System.Threading.RateLimiting;
using GameAPI.Filters;
using GameAPI.GenericCrud;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace GameAPI;
public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        ConfigureServices(builder);

        WebApplication app = builder.Build();
        
        ConfigureMiddleware(app);
        
        app.MapControllers();
        app.Run();
    }
    
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<GameDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );
        
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress?.ToString() ?? "unknown", 
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 40,
                        Window = TimeSpan.FromMinutes(1)
                    }));
            options.AddFixedWindowLimiter("StrictPolicy", opt =>
            {
                opt.PermitLimit = 1; 
                opt.Window = TimeSpan.FromSeconds(5);
            });
            
            options.RejectionStatusCode = 429;
        });
        
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddScoped<IdFilter>();
    }
    
    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseRateLimiter();
        app.UseHttpsRedirection();
        app.UseAuthorization();
    }
}