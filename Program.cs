using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Data;

namespace proj_csharp_kiminoyume
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var angularConnection = "_allowAngularConnection";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var cors = builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(
                    name: angularConnection,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200/")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            builder.Services.AddDbContext<AppDBContext>(ctx =>
            {
                ctx.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabase"));
            });

            builder.Services.AddStackExchangeRedisCache(opt => {
                opt.Configuration = builder.Configuration.GetConnectionString("RedisCacheUrl");
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(x =>
                {
                    x.SerializeAsV2 = true;
                });
                app.UseSwaggerUI();
            }

            app.UseCors(angularConnection);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}