using BibliotecaMangas.Abstractions.Interfaces;
using BibliotecaMangas.Data.EF;
using BibliotecaMangas.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("CONNECTIONSTRING");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<IAutoresRepository, AutoresRepository>();
builder.Services.AddScoped<IEditorialesRepository, EditorialesRepository>();
builder.Services.AddScoped<IObrasRepository, ObrasRepository>();
builder.Services.AddScoped<ITomosRepository, TomosRepository>();

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
