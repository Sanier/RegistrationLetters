using Microsoft.EntityFrameworkCore;
using RegistrationLetters.DAL;
using RegistrationLetters.DAL.Interfaces;
using RegistrationLetters.DAL.Repositories;
using RegistrationLetters.DAL.Seed;
using RegistrationLetters.Domain.Entities;
using RegistrationLetters.Services.Implementations;
using RegistrationLetters.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IBaseRepositories<MailEntity>, MailRepository>()
    .AddScoped<IBaseRepositories<EmployeeEntity>, EmployeeRepository>()
    .AddScoped<IEmployeeService, EmployeeService>()
    .AddScoped<IMailService, MailService>()
    .AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Made for testing and filling the database with data
    using (var scope = app.Services.CreateScope())
    {
        var salesContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        salesContext.Database.EnsureCreated();
        salesContext.Seed();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
