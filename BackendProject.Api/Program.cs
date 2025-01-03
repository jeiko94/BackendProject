using BackendProject.Aplicacion.Usuarios.Interfaces;
using BackendProject.Aplicacion.Usuarios.Servicios;
using BackendProject.Infraestructura.Usuarios.Data;
using BackendProject.Infraestructura.Usuarios.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Registrar DbContext
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseSqlServer(connectionString));

//Registrar Repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IRolRepositorio, RolRepositorio>();

//Registrar servicios
builder.Services.AddScoped<AuthServices>();


//Servicios que vienen por defecto en el template
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
