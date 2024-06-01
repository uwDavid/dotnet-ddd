using Marten;
using BuildingBlocks.Behaviors;
using Basket.API.Data;
using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using StackExchange.Redis;
using Discount.Grpc;

var builder = WebApplication.CreateBuilder(args);

// APPLICATION SERVICES
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

// DATA SERVICES
// Marten
var psqlConnection = builder.Configuration.GetConnectionString("Database");
builder.Services.AddMarten(opts =>
{
    opts.Connection(psqlConnection!);
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
// var redisConnection = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    // options.Configuration = redisConnection;
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    // to work in container
    // options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
    // {
    //     EndPoints = { { "cache", 6379 } },
    //     DefaultDatabase = 0
    // };
    // options.InstanceName = "Basket";
});
// Console.WriteLine("Connecting to Redis: " + builder.Configuration.GetConnectionString("Redis"));
// Need to add additional configuration to connect with Redis on Docker
// var redisOptions = new ConfigurationOptions
// {
//     EndPoints = { { "cache", 6379 } }
// };
// builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions));

// GRPC SERVICES - Grpc.AspNetCore
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

// CROSS-CUTTING SERVICES
// Exception Handler from Building Blocks
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Register Healthcheck
// install the healthcheck extendsions for PostgreSQL and Redis
builder.Services.AddHealthChecks()
    .AddNpgSql(psqlConnection!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.MapCarter();
// Configure Exception Handler
app.UseExceptionHandler(options => { });
// app.UseHealthChecks("/health"); 
// install healthcheck.ui
// json healthcheck response
app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.Run();
