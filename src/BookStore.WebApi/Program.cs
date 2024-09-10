using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "BookStore API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.RegisterInfrastureDependencies(builder.Configuration);

var fixedWindowPolicy = "fixedWindow";
builder.Services.AddRateLimiter(configureOptions => {
    configureOptions.AddFixedWindowLimiter(policyName: fixedWindowPolicy, options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(30);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
    configureOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var concurrencyPolicy = "Concurrency";
builder.Services.AddRateLimiter(configureOptions =>
{
    configureOptions.AddConcurrencyLimiter(policyName: concurrencyPolicy, options =>
    {
        options.PermitLimit = 2;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();
app.Run();