using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using MonitorMarkets.DesktopDatabase;
using MonitorMarkets.StategyService.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDesktopDataBase(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
    options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        ReferenceHandler = ReferenceHandler.Preserve,
    }));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseRouting();

app.UseSwagger();

app.UseSwaggerUI();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StrategyService");
    c.RoutePrefix = string.Empty;
});


app.Run();