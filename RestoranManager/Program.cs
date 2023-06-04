using Aplication;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Serilog;
using System.Text;
using TelegramSink;

namespace RestoranManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Telegram 
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(builder.Configuration)
                   .Enrich.FromLogContext()
                   .WriteTo.TeleSink(
                   telegramApiKey: "Telegram api key ",
                   telegramChatId: "Telegram chat id ",
                   minimumLevel: LogEventLevel.Error)
                   .CreateLogger();

            try
            {
                Log.Information("Aplication started");
                builder.Host.UseSerilog();
                //builder.Services.AddControllers().AddNewtonsoftJson();

                IConfiguration configuration = builder.Configuration;
                builder.Services.AddInfrastructure(configuration);
                builder.Services.AddApplicationServices();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();


                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "Bearer Authentication with JWT Token",
                        Type = SecuritySchemeType.Http
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme()
                    {
                       Reference=new OpenApiReference()
                       {
                           Id="Bearer",
                           Type=ReferenceType.SecurityScheme
                       }
                    },
                    new List<string>()
                } });
                });

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = configuration["JWT:Audience"],
                        ValidIssuer = configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseFileServer();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}