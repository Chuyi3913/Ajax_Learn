using Core_Api_Learn.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DemoContext>(
 options => options.UseSqlServer(
 builder.Configuration.GetConnectionString(@"DemoConnection")
));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Allow"); //µù¥U¨Ï¥ÎªºCORS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
