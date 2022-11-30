using Microsoft.Extensions.DependencyInjection;
using Users.Domain;
using Microsoft.Extensions.Configuration;
using Users.Application;
using Users.Persistance;
using Users.Application.Common.Mapping;
using System.Reflection;
using Users.Application.Interfaces;
using Users.Persistance;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using Users.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
//public IConfiguration Configuration { get; }
// Add services to the container.

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IUsersDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
//builder.Services.AddDbContext<Sql8580971Context>(options =>
//{
//    options.UseMySQL(builder.Configuration.GetConnectionString("DbConnection"));
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddApplication();
var app = builder.Build();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<Sql8580971Context>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        
    }
}

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
