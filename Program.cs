var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(); //Se agrego esta linea para agregara el servicio de dependencias Cors

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(p =>
{
    // Se gregar estas lineas para permitir el acceso a cualquier origen que desee consumir nuestr api 
    p.AllowAnyHeader(); // Permitir cualquiert tipo de encabezado dentro del request
    p.AllowAnyOrigin(); // Permitir a quien esta tratando de acceder al request (en este caso es cualquiera)
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
