var builder = WebApplication.CreateBuilder(args);

// Add Services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    // add required services to MediatR
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure HTTP middleware pipeline
app.MapCarter();  // auto maps all routes


app.Run();
