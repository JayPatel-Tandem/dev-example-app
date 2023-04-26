using dev_example_app.DBContext;
using dev_example_app.Extensions;
using dev_example_app.Middleware;
using dev_example_app.PipelineBehaviours;
using dev_example_app.Repository;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IUserInterface, UserImpl>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add Validator for MediatR
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add Okta Authentication
builder.Services.AddOktaAuthnetication(builder.Configuration);

builder.Services.AddCors(options =>
{
    // The CORS policy is open for testing purposes. In a production application, you should restrict it to known origins.
    options.AddPolicy(
       "AllowAll",
       builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
