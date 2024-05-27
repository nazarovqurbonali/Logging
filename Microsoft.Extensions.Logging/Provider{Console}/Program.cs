using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Web.Data;
using Web.Services.CateGoryService;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging( logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});


builder.Services.AddDbContext<DataContext>(x 
    => x.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();