using IFCE.AutoGate.API.Configurations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.SetDefaultConfiguration(builder.Environment);

// Configure services
builder.Services.AddApiConfiguration();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoGate API", Version = "v1" });
    options.UseDateOnlyTimeOnlyStringConverters();
});
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddSettingsConfiguration(configuration);
builder.Services.ConfigureDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiConfiguration();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
