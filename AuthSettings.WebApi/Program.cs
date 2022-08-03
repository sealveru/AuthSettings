using AuthSettings;
using AuthSettings.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileReader, FileReader>();
builder.Services.AddScoped<IValidationRunner, ValidationRunner>();
builder.Services.AddScoped<ISettingsDeployer, SettingsDeployer>();

builder.Services.Configure<Options>(builder.Configuration.GetSection("Auth0"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();