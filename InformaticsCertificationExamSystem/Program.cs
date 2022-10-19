﻿using InformaticsCertificationExamSystem.Data;
using Microsoft.EntityFrameworkCore;
using InformaticsCertificationExamSystem.Controllers;
using InformaticsCertificationExamSystem.Service.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to DB
builder.Services.AddDbContext<InformaticsCertificationExamSystem_DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
//Add Repository
builder.Services.AddRepository();

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
