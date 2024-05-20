using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add Services to container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    // add required services to MediatR
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior((typeof(ValidationBehavior<,>)));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(assembly);

// add CustomExceptionHandler - for global error handling
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure HTTP middleware pipeline
app.MapCarter();  // auto maps all routes

// Global Exception Handling - Custome Exception Handler approach
app.UseExceptionHandler(options => { });

// Global Exception Handling - lambda approach
// app.UseExceptionHandler(exceptionHandlerApp =>
// {
//     exceptionHandlerApp.Run(async context =>
//     {
//         var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//         if (exception is null) return;

//         var problemDetails = new ProblemDetails
//         {
//             Title = exception.Message,
//             Status = StatusCodes.Status500InternalServerError,
//             Detail = exception.StackTrace  // remove in production
//         };

//         var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//         logger.LogError(exception, exception.Message);

//         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//         context.Response.ContentType = "application/problem+json";

//         await context.Response.WriteAsJsonAsync(problemDetails);
//     });
// });

app.Run();
