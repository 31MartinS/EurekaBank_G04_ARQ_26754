using Microsoft.EntityFrameworkCore;
using Eurabank_Restfull_SOAP_G04.Data; // Importa el DbContext
using Eurabank_Restfull_SOAP_G04.ec.edu.monster.service; // Importa el servicio

var builder = WebApplication.CreateBuilder(args);

// 1. Añadir la conexión a la Base de Datos (DbContext)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CalculatorDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Añadir los servicios de API (Swagger, Controladores)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. REGISTRAR NUESTRAS CLASES (Inyección de Dependencias)
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();
builder.Services.AddScoped<ISucursalService, SucursalService>();
builder.Services.AddScoped<IContadorService, ContadorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Esto le dice a la app que busque nuestros Controladores
app.MapControllers();

app.Run();