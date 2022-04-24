using System.Text;
using teleperformance_case3.Application;
using teleperformance_case3.Application.Common.Interfaces;
using teleperformance_case3.Infrastructure;
using teleperformance_case3.Infrastructure.Persistence;
using teleperformance_case3.Services;

var builder = WebApplication.CreateBuilder(args);
//const string corsAll = "all";
var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]);

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration();

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(corsAll,
//        policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
//});
builder.Services.AddHttpContextAccessor();
//builder.Services.AddDataProtection();

//builder.Services.AddAuthentication(x =>
//    {
//        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(x =>
//    {
//        x.RequireHttpsMetadata = false;
//        x.SaveToken = true;
//        x.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware(typeof(ErrorHandler));

app.UseHttpsRedirection();

app.UseRouting();

//app.UseCors(corsAll);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();