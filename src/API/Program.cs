using System.Text.Json.Serialization;
using API.common.Extensions;
using API.middleware;
using Application;
using Infrastructure;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(isDevelopment? "http://localhost:5173":"https://gentle-mushroom-0a6104400.7.azurestaticapps.net")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.Configure<CookieOptions>(options =>
{
    options.HttpOnly = true;
    options.Secure = !isDevelopment;
    options.SameSite = isDevelopment? SameSiteMode.Lax : SameSiteMode.None;
    options.Expires  = DateTime.UtcNow.AddDays(1);
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<NormalizeStringFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, isDevelopment);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
});

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
//     await seeder.SeedAsync();
// }

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();