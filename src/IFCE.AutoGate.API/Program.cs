using IFCE.AutoGate.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure services

builder.Services.AddApiConfiguration();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.ConfigureDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiConfiguration();
app.UseAuthorization();

app.Run();
