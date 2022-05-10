
using MedaitorR.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(MedaitorDbContext));
//or
//builder.Services.AddMediatR(typeof(Program));

builder.Services.AddDbContext<MedaitorDbContext>(options => options.UseInMemoryDatabase("memoryDB"));

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

using (var service = app.Services.CreateScope())
{
    var context = service.ServiceProvider.GetService<MedaitorDbContext>();
    if (context!=null)
    {
        context.Database.EnsureCreated();
    }
}

app.Run();
