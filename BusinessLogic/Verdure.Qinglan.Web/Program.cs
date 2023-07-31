using Verdure.Qinglan;
using Verdure.Qinglan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<QinglanAccountOptions>(
    builder.Configuration.GetSection("QinglanAccount"));


builder.Services.AddHttpApi<IQinglanTokenApi>();
builder.Services.AddHttpApi<IQinglanApi>();
builder.Services.AddTokenProvider<IQinglanApi, CustomTokenProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

