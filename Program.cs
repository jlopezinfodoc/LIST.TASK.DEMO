using Microsoft.EntityFrameworkCore;
using LIST.TASK.DEMO.Infraestructura.Persistencia;
using LIST.TASK.DEMO.Infraestructura.Repository;
using LIST.TASK.DEMO.Infraestructura.Repository.Interfaces;
using LIST.TASK.DEMO.Servicios;
using LIST.TASK.DEMO.Servicios.Interfaces;
using LIST.TASK.DEMO.Servicios.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Entity Framework
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configure Repository layer
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Configure Service layer
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
