using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure.Data.Extensions;
using OrderingInfra.Application;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddAppliactionServices()
    .AddAppliactionServices(builder.Configuration);


var app = builder.Build();
app.UseApiServices();

if (app.Environment.IsDevelopment()) {

    await app.InitializeDatabaseAsync();
}

app.Run();