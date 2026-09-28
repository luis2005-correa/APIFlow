using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IActividadService, ActividadService>();


builder.Services.AddDbContext<GestionesAcademicasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("con")));

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
