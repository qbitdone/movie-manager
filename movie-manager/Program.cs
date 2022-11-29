using Microsoft.EntityFrameworkCore;
using movie_manager.Data;
using movie_manager.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddControllers();
builder.Services.AddDbContext<MovieManagerDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<DirectorService>();
builder.Services.AddTransient<GenreService>();
builder.Services.AddTransient<MovieService>();
builder.Services.AddTransient<ActorService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
