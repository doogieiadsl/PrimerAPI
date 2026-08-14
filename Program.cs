using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
//Se agrego esta linea para agregara el servicio de dependencias Cors
var MyAllowOrigins = "MyAllowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name : MyAllowOrigins,
      policy    =>
      {
          
          //policy.WithOrigins(allowedOrigins);
          policy.AllowAnyHeader();// Permitir cualquiert tipo de encabezado dentro del request
          policy.AllowAnyOrigin();// Permitir a quien esta tratando de acceder al request (en este caso es cualquiera)
          policy.AllowAnyMethod();
      });
}); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowOrigins);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
