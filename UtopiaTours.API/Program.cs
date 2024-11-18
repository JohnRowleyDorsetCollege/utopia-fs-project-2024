using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UtopiaTours.API;
using static UtopiaTours.API.DTOs.BookingDTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = "Server=localhost;Database=UtopiaTours;User=root;Password=root";

//builder.Services.AddDbContext<UtopiaToursContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));
builder.Services.AddDbContext<UtopiaToursContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
