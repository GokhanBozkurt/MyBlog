using CacheUse.Infrastructure;
using CacheUse.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

/*
docker pull redis
docker run -p 6379:6379 redis
 */
builder.Services.AddStackExchangeRedisCache(o =>
{
    o.Configuration = builder.Configuration.GetSection("Redis")["ConnectionString"]; //"localhost";
    o.InstanceName = "SampleInstance";
});

builder.Services.AddScoped<ICacheProvider, CacheProvider>();
builder.Services.AddScoped<IDistrubutedCacheService, DistrubutedCacheService>();

//builder.Services.AddDistributedRedisCache(a =>
//{
//    a.InstanceName = "session";
//    a.Configuration = "localhost:1453";
//});

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
