using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeAreDevs.Context;
using WeAreDevs.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registro de servi�os
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//conex�o com banco de dados
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433;Database=wearedevs0;User ID=sa;Password=WeAreDevs0$;TrustServerCertificate=True");
});

//documenta��o da aplica��o
builder.Services.AddSwaggerGen(x =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();