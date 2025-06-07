using Microsoft.EntityFrameworkCore;
using StormSafe.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Swagger e Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Banco de dados Oracle
builder.Services.AddDbContext<StormSafeDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// CORS antes da autorização
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();