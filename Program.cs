using System.Text;
using Aerolineas.Config;
using Aerolineas.Interfaces;
using Aerolineas.Mapper;
using Aerolineas.Models;
using Aerolineas.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddDbContext<AeroContext>();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("Jwt"));


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt__Key") ?? throw new InvalidOperationException("No hay jwtkey")))
        };
    });

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UsuarioService>();
builder.Services.AddScoped<IAeronaveService, AeronaveService>();
builder.Services.AddScoped<IVuelosService, VueloService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddControllers();

builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
