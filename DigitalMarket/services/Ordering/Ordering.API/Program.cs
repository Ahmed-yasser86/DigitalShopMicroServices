using Ordering.API;
using Ordering.Infrastructure.Data.Extensions;
using OrderingInfra.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppliactionServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseApiServices();

if (app.Environment.IsDevelopment()) {

    await app.InitializeDatabaseAsync();
}

app.Run();