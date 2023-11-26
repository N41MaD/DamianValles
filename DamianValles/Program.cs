using AutoMapper;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using WeightLiftingLibrary.Business;
using WeightLiftingLibrary.Business.Interfaces;
using WeightLifting.Library.Logger;
using WeightLifting.Library.MapperProfiles;
using WeightLifting.Persistance.Repository.Interfaces;
using WeightLifting.Persistance;
using WeightLiftingPersistance.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeightLifting.Library.Business.Interfaces;
using WeightLifting.Library.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!));
    var singingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey
    };
    options.RequireHttpsMetadata = false;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//log4net
var log4netConfig = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "log4net.config"));
XmlConfigurator.ConfigureAndWatch(log4netConfig);
builder.Services.AddSingleton<ILoggerService, LoggerService>();

builder.Services.AddDbContext<WeightliftingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeightLiftingConnection")));

var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
}); 

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IWeightLiftingService, WeightLiftingService>();
builder.Services.AddTransient<IWeightLiftingRepository, WeightLiftingRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
