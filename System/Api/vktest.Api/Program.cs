using Api.Configuration;
using vktest.Api;
using vktest.Api.Configuration;
using vktest.Context;
using vktest.Context.Setup;
using vktest.Settings;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddAppLogger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppHealthChecks();
services.AddAppCors();
services.AddAppDbContext(builder.Configuration);
services.RegisterAppServices();
services.AddAppAutoMappers();
services.AddAppController();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();
app.UseAppController();
app.UseAppMiddlewares();
app.UseAppHealthChecks();

app.UseAppCors();



DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);
app.Run();

