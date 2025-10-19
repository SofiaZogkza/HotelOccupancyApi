using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelOccupancy.Application;
using HotelOccupancy.Application.Services;
using HotelOccupancy.Application.Validators;
using HotelOccupancy.Persistence.Data;
using HotelOccupancy.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var envFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, ".env");
Env.Load(envFilePath);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RoomSearchRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RoomsByTravelGroupRequestValidator>();

var connectionString =
    $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
    $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
    $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}";

builder.Services.AddDbContext<HotelOccupancyDbContext>(options =>
     options.UseNpgsql(connectionString));

// Register your services
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ITravelGroupService, TravelGroupService>();
builder.Services.AddScoped<ITravelGroupRepository, TravelGroupRepository>();
builder.Services.AddScoped<ITravellerService, TravellerService>();
builder.Services.AddScoped<ITravellerRepository, TravellerRepository>();

// OpenAPI/Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();