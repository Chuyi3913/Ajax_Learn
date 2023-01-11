using Core_Api_Learn.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//建立Database Demo的dbContext
builder.Services.AddDbContext<DemoContext>(
 options => options.UseSqlServer(
 builder.Configuration.GetConnectionString(@"DemoConnection")
));

//建立資料可以序列化成xml的方法
builder.Services.AddControllers().AddXmlSerializerFormatters();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow",
    builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Allow"); //註冊使用的CORS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
