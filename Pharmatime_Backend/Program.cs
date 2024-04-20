using Microsoft.EntityFrameworkCore;
using Pharmatime_Backend.Repositories.Models;
using Rotativa.AspNetCore;
using Wkhtmltopdf.NetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PHARMATIME_DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PHARMATIMEContext"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string wwwroot = app.Environment.WebRootPath;
RotativaConfiguration.Setup(wwwroot, "Rotativa");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
