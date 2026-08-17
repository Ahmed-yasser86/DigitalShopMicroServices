using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(
    config =>config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
});
var app = builder.Build();

app.MapCarter();

app.Run();
