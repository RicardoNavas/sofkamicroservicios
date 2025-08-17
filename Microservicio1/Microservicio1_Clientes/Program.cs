using Microsoft.EntityFrameworkCore;
using Microservicio1_Clientes.Models;
using Microservicio1_Clientes.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddDbContext<BaseDatosContext>(option =>
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

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();

