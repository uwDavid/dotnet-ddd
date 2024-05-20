using BuildingBlocks.Behaviors;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add Services to container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    // add required services to MediatR
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior((typeof(ValidationBehavior<,>)));
});

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

// Configure HTTP middleware pipeline
app.MapCarter();  // auto maps all routes

// Global Exception Handling - lambda approach
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null) return;

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace  // remove in production
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});


app.Run();
