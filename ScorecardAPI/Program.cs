
using Microsoft.EntityFrameworkCore;

namespace ScorecardAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ScorecardContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ScorecardDatabase")));
            builder.Services.AddCors(options => 
                options.AddPolicy("MyCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin() // Replace with your Blazor app's origin
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                })
            );

            var app = builder.Build();

            // Apply database migrations
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ScorecardContext>();
                dbContext.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scorecard API v1.0.0");
                });
            }
            else
            {
                //app.UseHsts();
            }

            app.UseCors("MyCorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

      
    }
}