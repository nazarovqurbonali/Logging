using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Services.CateGoryService;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddDebug();


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