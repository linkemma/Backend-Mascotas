using BackendMascotas.Models;
using BackendMascotas.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//activar los Cors
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                                        builder => builder.AllowAnyOrigin()
                                                                           .AllowAnyHeader()
                                                                           .AllowAnyMethod()));

//Add Context
builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Services
builder.Services.AddScoped<IMascotasRepository,MascotaRepository>();

// Configuracion Automaper
builder.Services.AddAutoMapper(typeof(Program));
app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
