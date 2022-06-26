using MongoDB.Bson;
using MongoDB.Driver;
using OpenFoodFacts.Models;
using OpenFoodFacts.Services;
using Cronos;
using OpenFoodFacts.CRON;
using OpenFoodFacts.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OpenFoodFactsDatabaseSettings>(
    builder.Configuration.GetSection("OpenFoodsDatabase"));

builder.Services.AddSingleton<FoodsService>();
builder.Services.AddSingleton<CronService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCronJob<CronTask>(c =>
//{
//    c.TimeZoneInfo = TimeZoneInfo.Local;
//    c.CronExpression = @"* * * * *";
//});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

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
