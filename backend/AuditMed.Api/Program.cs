
using AuditMed.Api.Data.Context;
using AuditMed.Api.Repositories.AtencionRepository;
using AuditMed.Api.Services.AtencionService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuditMedDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuditMedConnection")));

builder.Services.AddScoped<IAtencionRepository, AtencionRepository>();

builder.Services.AddScoped<IAtencionService, AtencionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "AuditMed API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

app.Run();