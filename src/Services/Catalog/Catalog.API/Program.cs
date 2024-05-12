using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add Services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    // add required services to MediatR
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior((typeof(ValidationBehavior<,>)));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Configure HTTP middleware pipeline
app.MapCarter();  // auto maps all routes


app.Run();
