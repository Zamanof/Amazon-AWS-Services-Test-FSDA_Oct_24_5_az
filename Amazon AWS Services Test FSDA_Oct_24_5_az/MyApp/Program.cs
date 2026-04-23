using MyApp.Data;
using MyApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddScoped<IStorageService, S3Service>();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowReact", policy =>
        {
            policy.WithOrigins("http://productoria.s3-website.eu-north-1.amazonaws.com", "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    }
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");

app.UseAuthorization();

app.MapControllers();


app.Run();
