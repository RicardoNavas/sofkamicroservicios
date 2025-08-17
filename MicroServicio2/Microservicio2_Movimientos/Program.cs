using Microsoft.EntityFrameworkCore;
using Microservicio2_Movimientos.Models;
using Microservicio2_Movimientos.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<ReporteRepository>();

builder.Services.AddDbContext<BaseDatosContextM2>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ca" +
        "" +
        "denaSQL"));

});


builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();

