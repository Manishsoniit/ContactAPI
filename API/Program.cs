var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/error");
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        await context.Response.WriteAsync("Resource not found");
    }
});


app.Run();
