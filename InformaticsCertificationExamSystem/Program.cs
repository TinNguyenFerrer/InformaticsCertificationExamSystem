using InformaticsCertificationExamSystem.Data;
using Microsoft.EntityFrameworkCore;
using InformaticsCertificationExamSystem.Controllers;
using InformaticsCertificationExamSystem.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCORS",
                          policy =>
                          {
                              policy.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to DB
builder.Services.AddDbContext<InformaticsCertificationExamSystem_DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
//Add Repository
builder.Services.AddRepository();
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//tránh vòng lặp
//builder.Services.AddControllers()
//            .AddJsonOptions(o => o.JsonSerializerOptions
//                .ReferenceHandler = ReferenceHandler.Preserve);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("MyCORS");

app.MapControllers();


app.Run();
