using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

using var connection = new MySqlConnection("Server=localhost; Port=3306; Database=lawyer_schema; Uid=root; Pwd=password;");

var users = connection.Query<User>("Select id,name,email,password from Users;");

Console.WriteLine(string.Join(Environment.NewLine, users.Select(u => $"{u.name}, {u.email}, {u.password}")));
// Add services to the container.

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherForecast}/{action=GetWeatherForecast}/{id?}");

app.MapControllers();

app.Run();

public record User(int id, string name, string email, string password);