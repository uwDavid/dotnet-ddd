using Marten;
using BuildingBlocks.Behaviors;
using Basket.API.Data;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Cater scans for all ICarter implementations and add those routes
builder.Services.AddCarter();

// MediatR
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    // register MediatR into current assembly
    // so that we can use MediatR from the BulidingBlocks library
    config.RegisterServicesFromAssembly(assembly);
    // add additional behaviors into request pipeline
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Marten
var conn = builder.Configuration.GetConnectionString("Database");
builder.Services.AddMarten(opts =>
{
    opts.Connection(conn!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
})
    .UseLightweightSessions();

// DI for Repository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Manual Decoration for Cache Repository
// builder.Services.AddScoped<IBasketRepository>(provider =>
// {
//     var basketRepository = provider.GetRequiredService<BasketRepository>();
//     return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
// });

// DI for CachedRepository - via Scrutor
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// Register Redis as Distributed Cache - to use IDistributedCache 
var redisConnection = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnection;
    // options.InstanceName = "Basket";
});

// Exception Handler from Building Blocks
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
// Configure Exception Handler
app.UseExceptionHandler(options => { });

app.Run();
