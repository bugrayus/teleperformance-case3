using System.Reflection;
using System.Text;
using CicekSepetiCase.Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using teleperformance_case3.Application;
using teleperformance_case3.Application.Common.Helpers;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Application.Common.Middlewares;
using teleperformance_case3.Infrastructure;
using teleperformance_case3.Infrastructure.Persistence;
using teleperformance_case3.Services;

const string corsAll = "all";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var appSettingsSection = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<Token>();

builder.Services.AddScoped<IApplicationDbContext>(
    provider => provider.GetRequiredService<ApplicationDbContext>()
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsAll,
        policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddHttpContextAccessor();

//builder.Services.AddDataProtection();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TeleperformanceCase3",
        Description = "",
        Contact = new OpenApiContact
        {
            Name = "Bugra Durukan",
            Email = "bugray34@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then user token in the text input below.\r\n\r\n " +
            "Example: \"Bearer 12345abcdef\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ErrorHandler));
app.UseMiddleware<Jwt>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(corsAll);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
context?.Database.EnsureCreated();

app.Run();