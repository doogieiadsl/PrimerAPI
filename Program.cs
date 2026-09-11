using CursoApis.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PrimerAPI.Data;
using PrimerAPI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Este DBcontext sera para BD SQLExpress
builder.Services.AddDbContext<PrimerApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    options.UseSqlServer(connectionString);
});
//Este dbcontext se utilizo para bd in memory
//builder.Services.AddDbContext<PrimerApiDbContext>(options =>
//{
//    options.UseInMemoryDatabase("PrimerAPiDB");
//}
//    );
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authentication"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("basic", document),
                new List<string>() // <-- List<string>, no array
            }
        }
    );
});
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
// Llamado de los servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
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
app.UseBasicAuth();

app.UseAuthorization();

// Aqui van los custom middlewares
app.UseRequestLogging();

app.MapControllers();

app.Run();
