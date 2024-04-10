using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using TestTasks__API_;
using TestTasks__API_.Domain.Interfaces;
using Serilog;
using Serilog.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Установка строки подключения

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Добавление сервиса в коллекцию сервисов приложения
builder.Services.AddTransient<IPizzaRepository, PizzaRepository>(); //система на место объектов интерфейса IPizzaRepository будет передавать экземпляры класса PizzaRepository

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MankovaJV_TaskContext>(options => options.UseSqlServer(connection));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();



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