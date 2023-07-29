using Verdure.Qinglan;
using Verdure.Qinglan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApiTokenFilter>();

builder.Services.Configure<QinglanAccountOptions>(
    builder.Configuration.GetSection("QinglanAccount"));

builder.Services.AddHttpApi<IQinglanApi>(o =>
{
    o.GlobalFilters.Add(builder.Services.BuildServiceProvider().GetRequiredService<ApiTokenFilter>());
});

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

