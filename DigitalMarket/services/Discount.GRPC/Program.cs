using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to use ONLY HTTP/2 (unencrypted gRPC) on port 8080
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
    
    // Ignore EF Core pending model changes warning on automatic migration
    options.ConfigureWarnings(w => 
        w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

builder.Services.AddGrpc();

var app = builder.Build();

// Run DB migrations on container start
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    dbContext.Database.Migrate();
}

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "gRPC endpoint active.");

app.Run();