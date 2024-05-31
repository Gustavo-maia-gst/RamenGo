using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RamenGo.Api.Middlewares;
using RamenGo.Api.Profiles;
using RamenGo.Application.Services;
using RamenGo.Domain.Repositories;
using RamenGo.Infrastructure;
using RamenGo.Infrastructure.Repositories;
using System.Reflection;

namespace RamenGo.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<RamenGoDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("Default")!;
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ramen Go", Version = "v1" });

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Api key",
                    Name = "x-api-key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddScoped<IBrothRepository, BrothRepository>();
            builder.Services.AddScoped<IProteinRepository, ProteinRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<RamenGoDbContext>();

            builder.Services.AddTransient<OrderService>();
            builder.Services.AddTransient<BrothService>();
            builder.Services.AddTransient<ProteinService>();

            builder.Services.AddAutoMapper(typeof(RamenGoProfile));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors();

            app.MapControllers();
            app.MapGet("/", async context => await Task.Run(() => context.Response.Redirect("/swagger/")));

            app.Run();
        }
    }
}
