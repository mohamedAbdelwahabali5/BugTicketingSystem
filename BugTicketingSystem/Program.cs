using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using Microsoft.Extensions.Configuration;
using DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Get configuration from builder
var configuration = builder.Configuration;

// Configure DbContext with proper migrations assembly
//builder.Services.AddDataAccessServices(builder.Configuration);

builder.Services.AddDbContext<BTSDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("DataAccessLayer")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();