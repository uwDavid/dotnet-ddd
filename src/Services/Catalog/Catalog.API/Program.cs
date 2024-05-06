var builder = WebApplication.CreateBuilder(args);

// Add Services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    // add required services to MediatR
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure HTTP middleware pipeline
app.MapCarter();  // auto maps all routes


app.Run();
