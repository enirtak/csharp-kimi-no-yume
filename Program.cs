using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using proj_csharp_kiminoyume.BusinessLogics;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.Middlewares;
using proj_csharp_kiminoyume.Services.Auth;
using proj_csharp_kiminoyume.Services.DreamCategory;
using proj_csharp_kiminoyume.Services.DreamDictionary;
using proj_csharp_kiminoyume.Services.Profile;
using proj_csharp_kiminoyume.Services.Redis;
using System.Text;
using System.Reflection;
using System.IO;

namespace proj_csharp_kiminoyume
{
    public class Program
    {
        // https://www.youtube.com/watch?v=H3EbflpXVmo
        public static void Main(string[] args)
        {
            var angularConnection = "_allowAngularConnection";
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add/Register services to the (DI) container.
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

            builder.Services.AddDbContext<AuthDBContext>(ctx =>
            {
                ctx.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabase"));
            });

            builder.Services.AddStackExchangeRedisCache(opt => {
                opt.Configuration = builder.Configuration.GetConnectionString("RedisCacheUrl");
            });

            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
            builder.Services.AddScoped<IDreamDictionaryBusinessLogic, DreamDictionaryBusinessLogic>();
            builder.Services.AddScoped<IDreamCategoryBusinessLogic, DreamCategoryBusinessLogic>();
            builder.Services.AddScoped<IProfileBusinessLogic, ProfileBusinessLogic>();
            builder.Services.AddTransient<ExceptionHandling>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(opt =>
            {
                // https://stackoverflow.com/a/62864495
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            // https://medium.com/c-sharp-progarmming/asp-net-core-5-jwt-authentication-tutorial-with-example-api-aa59e80d02da
            builder.Services
                .AddIdentityCore<IdentityUser>(opt =>
                {
                    opt.SignIn.RequireConfirmedAccount = false;
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<AuthDBContext>();

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "apiWithAuthBackend",
                        ValidAudience = "apiWithAuthBackend",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!SomethingSecret!"))
                    };

                    // https://stackoverflow.com/a/70173244
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Headers["Authorization"];
                            return Task.CompletedTask;
                        },
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Enable middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(x =>
                {
                    x.SerializeAsV2 = true;
                });
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandling>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(angularConnection);

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}