var builder = WebApplication.CreateBuilder(args);
// Add Services to container

var app = builder.Build();
// Configure HTTP middleware pipeline

app.Run();
